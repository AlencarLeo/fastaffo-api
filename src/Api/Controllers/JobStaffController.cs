using fastaffo_api.src.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class JobStaffController : ControllerBase
{
    private readonly IJobStaffService _jobStaffService;

    public JobStaffController(IJobStaffService jobStaffService)
    {
        _jobStaffService = jobStaffService;
    }


    [HttpDelete]
    [Route("job-staff")]
    public async Task<ActionResult> RemoveJobStaff(Guid jobStaffId)
    {
        var response = await _jobStaffService.RemoveJobStaff(jobStaffId);
        return StatusCode(response.StatusCode, new { message = response.Message });
    }

}