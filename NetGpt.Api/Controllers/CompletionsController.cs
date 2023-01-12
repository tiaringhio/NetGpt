
using Microsoft.AspNetCore.Mvc;
using NetGpt.Api.Models;
using NetGpt.Api.Services.Completion;

namespace NetGpt.Api.Controllers;

[Route("[controller]/[action]")]
public sealed class CompletionsController : ControllerBase
{
    private readonly ICompletionService _completionService;

    public CompletionsController(ICompletionService completionService)
    {
        _completionService = completionService;
    }

    /// <summary>
    ///     Asks ChatGPT for complettions
    /// </summary>
    /// <param name="request">Prompt request</param>
    /// <returns></returns>
    /// <response code="200">Completion</response>
    /// <response code="429">User is rate limited</response>
    /// <response code="500">Server Error</response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> GetCompletion([FromBody] CompletionRequest request)
    {
        var completion = await _completionService.GetCompletion(request.Prompt);
        
        return Ok(completion);
    }
}