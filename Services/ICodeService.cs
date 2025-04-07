using FinBeatTestTask.Dto;
using FinBeatTestTask.Models;

namespace FinBeatTestTask.Services;

public interface ICodeService
{
    public Task<IEnumerable<CodeEntity>> GetCodesAsync(CancellationToken cancellationToken, CodeFilter filter);

    public Task<bool> ReloadCodesAsync(CancellationToken cancellationToken, List<Dictionary<string, string>> codes);
}
