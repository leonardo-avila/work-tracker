using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.Clock.UseCase.OutputViewModels;
using WorkTracker.Clock.UseCase.Ports;
using WorkTracker.Domain.Core;

namespace WorkTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("Bearer")]
public class ReportsController : ControllerBase
{
    private readonly ILogger<ReportsController> _logger;
    private readonly IPunchUseCases _punchUseCases;

    public ReportsController(ILogger<ReportsController> logger, IPunchUseCases punchUseCases)
    {
        _logger = logger;
        _punchUseCases = punchUseCases;
    }


    /// <summary>
    /// Get current day work punches for the specified employee
    /// </summary>
    /// <returns>Returns the respective punches</returns>
    /// <response code="200">Successfully retrieved punches.</response>
    /// <response code="400">Invalid rm.</response>
    /// <response code="404">No punches found for the specified rm.</response>
    /// <response code="500">An error occurred while processing your request.</response>
    [HttpGet(Name = "Get punches of the month")]
    public async Task<ActionResult<MonthlyPunchesViewModel>> GetMonthlyReport()
    {
        try 
        {
            var rm = User.FindFirst("nickname")?.Value;
            var punches = await _punchUseCases.GetMonthlyPunches(rm!);
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
}
