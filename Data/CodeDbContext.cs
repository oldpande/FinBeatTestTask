using FinBeatTestTask.Dto;
using Microsoft.EntityFrameworkCore;

namespace FinBeatTestTask.Data;

public class CodeDbContext : DbContext
{
    public CodeDbContext(DbContextOptions<CodeDbContext> options) : base(options) { }
    public DbSet<CodeEntity> Codes { get; set; }
}
