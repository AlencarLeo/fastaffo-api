using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;

[Route("api/")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;

    }

    [HttpGet]
    [Route("staff")]
    public async Task<ActionResult<StaffDtoRes>> GetStaffById(Guid staffId)
    {
        try
        {
            var result = await _staffService.GetStaffByIdAsync(staffId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
