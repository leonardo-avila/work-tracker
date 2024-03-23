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
public class ReportsController : ControllerBase
{
    private readonly ILogger<ReportsController> _logger;
    private readonly IPunchUseCases _punchUseCases;

    private readonly IUtilsService _employeeService;

    public ReportsController(ILogger<ReportsController> logger, IPunchUseCases punchUseCases, IUtilsService employeeService)
    {
        _logger = logger;
        _punchUseCases = punchUseCases;
        _employeeService = employeeService;
    }


    /// <summary>
    /// Get monthly punches report
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
            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            var punches = await _punchUseCases.GetMonthlyPunches(rm!, email!);
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
