using Microsoft.AspNetCore.Mvc;
using WorkTracker.Clock.UseCase.InputViewModels;
using WorkTracker.Clock.UseCase.Ports;
using WorkTracker.Domain.Core;

namespace WorkTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeUseCases _employeeUseCases;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeUseCases employeeUseCases)
    {
        _logger = logger;
        _employeeUseCases = employeeUseCases;
    }


    /// <summary>
    /// Add a new employee to the system
    /// </summary>
    /// <returns>Returns the respective employee</returns>
    /// <response code="200">Successfully retrieved employees.</response>
    /// <response code="400">Invalid rm.</response>
    /// <response code="404">No employeees found for the specified rm.</response>
    /// <response code="500">An error occurred while processing your request.</response>
    [HttpGet(Name = "Add a new employee")]
    [Route("")]
    public async Task<ActionResult<EmployeeViewModel>> AddEmployee(EmployeeViewModel employeeViewModel)
    {
        try 
        {
            await _employeeUseCases.AddEmployee(employeeViewModel);
            
            return Ok(employeeViewModel);
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
