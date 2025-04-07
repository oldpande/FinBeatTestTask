using FinBeatTestTask.Data;
using FinBeatTestTask.Dto;
using Microsoft.EntityFrameworkCore;

namespace FinBeatTestTask.Services;

public class CodeRepository : ICodeRepository
{
    private readonly CodeDbContext _db;

    public CodeRepository(CodeDbContext db)
    {
        _db = db;
    }

    public async Task ClearAsync(CancellationToken cancellationToken)
    {
        var oldCodes = await _db.Codes.Select(x => x).ToListAsync(cancellationToken);
        _db.Codes.RemoveRange(oldCodes);
        _db.SaveChanges();
    }

    public async Task<IEnumerable<CodeEntity>> GetAsync(CancellationToken cancellationToken)
    {
        return await _db.Codes.Select(x => x).ToListAsync(cancellationToken);
    }

    public async Task LoadAsync(CancellationToken cancellationToken, IList<CodeEntity> codes)
    {
        await _db.Codes.AddRangeAsync(codes);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
