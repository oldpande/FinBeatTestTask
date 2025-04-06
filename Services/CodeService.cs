using FinBeatTestTask.Dto;
using FinBeatTestTask.Models;

namespace FinBeatTestTask.Services;

public class CodeService : ICodeService
{
    public CodeService()
    {

    }

    private readonly ICodeRepository _codeRepository;

    public async Task<List<ExternalCodes>> GetCodesAsync(CancellationToken cancellationToken)
    {
        var codes = await _codeRepository.GetAsync(cancellationToken);

        return codes.Select(c => new ExternalCodes { Code = c.Code, Value = c.Value })
            .OrderBy(c => c.Code)
            .ToList();
    }

    public async Task<bool> ReloadCodesAsync(CancellationToken cancellationToken, IList<ExternalCodes> codes)
    {
        var codesEntity = codes.Select(c => new CodeEntity { Code = c.Code, Value = c.Value} )
            .OrderBy(c => c.Code)
            .ToList();

        await _codeRepository.ClearAsync(cancellationToken);

        await _codeRepository.LoadAsync(cancellationToken, codesEntity);

        return true;
    }
}
