using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;

[Route("api/")]
[ApiController]
public class StaffJobController : ControllerBase
{
    private readonly IStaffJobService _staffJobService;
    public StaffJobController(IStaffJobService staffJobService)
    {
        _staffJobService = staffJobService;
    }

    [HttpPost]
    [Route("staff-job")]
    public async Task<ActionResult> CreateStaffJob(StaffJobDtoReq request)
    {
        try
        {
            await _staffJobService.CreateStaffJobAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    [Route("staff-job")]
    public async Task<ActionResult<StaffJobDtoRes>> GetStaffJobById(Guid staffJobId)
    {
        try
        {
            var result = await _staffJobService.GetStaffJobByIdAsync(staffJobId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

}
