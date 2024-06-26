using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Clock.UseCase.OutputViewModels;
using WorkTracker.Clock.UseCase.Ports;
using WorkTracker.Domain.Core;

namespace WorkTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("Bearer")]
public class PunchController : ControllerBase
{
    private readonly ILogger<PunchController> _logger;
    private readonly IPunchUseCases _punchUseCases;

    public PunchController(ILogger<PunchController> logger, IPunchUseCases punchUseCases)
    {
        _logger = logger;
        _punchUseCases = punchUseCases;
    }


    /// <summary>
    /// Get current day work punches
    /// </summary>
    /// <returns>Returns the respective punches</returns>
    /// <response code="200">Successfully retrieved punches.</response>
    /// <response code="400">Invalid rm.</response>
    /// <response code="404">No punches found for the specified rm.</response>
    /// <response code="500">An error occurred while processing your request.</response>
    [HttpGet(Name = "Get punches of the day")]
    public async Task<ActionResult<DailyPunchesViewModel>> GetPunches()
    {
        try 
        {
            var rm = User.FindFirst("nickname")?.Value;
            var punches = await _punchUseCases.GetPunches(rm!);
            if (punches is null) {
                return NoContent();
            }
            return Ok(punches);
        }
        catch (DomainException ex) 
        {
            return BadRequest(ex.Message);
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }

    /// <summary>
    /// Punch in or out
    /// </summary>
    /// <returns>Returns the respective punch</returns>
    /// <response code="200">Successfully punched.</response>
    /// <response code="400">Invalid rm.</response>
    /// <response code="500">An error occurred while processing your request.</response>
    [HttpPost(Name = "Punch in or out")]
    public async Task<ActionResult<OutputPunchViewModel>> Punch()
    {
        try 
        {
            var rm = User.FindFirst("nickname")?.Value;
            var punch = await _punchUseCases.Punch(rm!);
            return Ok(punch);
        }
        catch (DomainException ex) 
        {
            return BadRequest(ex.Message);
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }
}
