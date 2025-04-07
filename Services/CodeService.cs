using FinBeatTestTask.Dto;
using FinBeatTestTask.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FinBeatTestTask.Services;

public class CodeService : ICodeService
{
    private readonly ICodeRepository _codeRepository;
    //есть несколько вариантов, как улучшить кэширование:
    //1) кжшировать на уровне инфраструктуры
    //2) вынести кэширование в Redis
    private readonly IMemoryCache _cache;
    private readonly string CacheKey = "CodesCacheKey";

    public CodeService(ICodeRepository codeRepository, IMemoryCache cache)
    {
        _codeRepository = codeRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<CodeEntity>> GetCodesAsync(CancellationToken cancellationToken, CodeFilter filter)
    {
        if (!_cache.TryGetValue(CacheKey, out IEnumerable<CodeEntity> codes))
        {
            codes = await _codeRepository.GetAsync(cancellationToken);
            _cache.Set(CacheKey, codes, TimeSpan.FromMinutes(1));
        }

        var filteredCodes = TryFilter(codes, filter);
        return filteredCodes;
    }

    public async Task<bool> ReloadCodesAsync(CancellationToken cancellationToken, List<Dictionary<string, string>> codes)
    {
        var codesEntity = codes.Select(c => new CodeEntity { Code = int.Parse(c.First().Key), Value = c.First().Value} )
            .OrderBy(c => c.Code)
            .ToList();

        await _codeRepository.ClearAsync(cancellationToken);

        await _codeRepository.LoadAsync(cancellationToken, codesEntity);

        _cache.Set(CacheKey, codesEntity, TimeSpan.FromMinutes(1));

        return true;
    }

    private IEnumerable<CodeEntity> TryFilter(IEnumerable<CodeEntity>? codes, CodeFilter filter)
    {
        if (filter.Id is not null)
        {
            return codes.Where(c => c.Id == filter.Id);
        }
        if (filter.Code is not null)
        {
            return codes.Where(c => c.Code == filter.Code);
        }
        if (filter.Value is not null)
        {
            return codes.Where(c => filter.Value.Equals(c.Value, StringComparison.OrdinalIgnoreCase));
        }

        return codes;
    }
}
