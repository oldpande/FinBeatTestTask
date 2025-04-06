using FinBeatTestTask.Models;

namespace FinBeatTestTask.Services;

public interface ICodeService
{
    public Task<List<ExternalCodes>> GetCodesAsync(CancellationToken cancellationToken);

    public Task<bool> ReloadCodesAsync(CancellationToken cancellationToken, IList<ExternalCodes> codes);
}
