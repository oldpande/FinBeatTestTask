using FinBeatTestTask.Services;
using FinBeatTestTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinBeatTestTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CodeController
{
    public CodeController()
    {

    }

    private readonly ICodeService _codeService;

    [HttpPost]
    public async Task<ActionResult<bool>> LoadCodesAsync(CancellationToken cancellationToken, [FromBody] List<ExternalCodes> codes)
    {
        return await _codeService.ReloadCodesAsync(cancellationToken, codes);
    }

}

