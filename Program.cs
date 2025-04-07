using FinBeatTestTask.Data;
using FinBeatTestTask.Middlewares;
using FinBeatTestTask.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddMemoryCache();

    builder.Services.AddControllers();
    builder.Services.AddTransient<ICodeService, CodeService>();
    builder.Services.AddTransient<ICodeRepository, CodeRepository>();

    //миграция добавлена, потому что предпочитаю Code first, и чтобы логику приложения не размазывать на разные уровни
    builder.Services.AddDbContext<CodeDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    var app = builder.Build();

    app.UseMiddleware<RequestResponseLoggingMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}