using FinBeatTestTask.Dto;

namespace FinBeatTestTask.Services;

public interface ICodeRepository
{
    public Task ClearAsync(CancellationToken cancellationToken);
    public Task LoadAsync(CancellationToken cancellationToken, IList<CodeEntity> codes);
    public Task<IEnumerable<CodeEntity>> GetAsync(CancellationToken cancellationToken);
}
