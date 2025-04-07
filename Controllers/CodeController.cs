using FinBeatTestTask.Models;
using FinBeatTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinBeatTestTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CodeController : ControllerBase
{
    private readonly ICodeService _codeService;

    public CodeController(ICodeService codeService)
    {
        _codeService = codeService;
    }

    [HttpPost]
    public async Task<IActionResult> LoadCodesAsync(CancellationToken cancellationToken, [FromBody] List<Dictionary<string, string>> codes)
    {
        return Ok(await _codeService.ReloadCodesAsync(cancellationToken, codes));
    }

    [HttpGet]
    public async Task<IActionResult> GetCodesAsync(CancellationToken cancellationToken, [FromQuery] CodeFilter filter)
    {
        return Ok(await _codeService.GetCodesAsync(cancellationToken, filter));
    }
}

