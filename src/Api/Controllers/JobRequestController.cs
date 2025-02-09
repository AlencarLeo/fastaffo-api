using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class JobRequestController : ControllerBase
{
    private readonly IJobRequestService _jobRequestService;
    public JobRequestController(IJobRequestService jobRequestService)
    {
        _jobRequestService = jobRequestService;
    }

    [HttpPost]
    [Route("job-request")]
    public async Task<ActionResult> CreateJobRequest(JobRequestDtoReq request)
    {
        var response = await _jobRequestService.CreateJobRequestAsync(request.Type, request.JobId, request.AdminId, request.StaffId);
        return StatusCode(response.StatusCode, new { message = response.Message });
    }

    [HttpPatch]
    [Route("job-request/accept")]
    public async Task<ActionResult> AcceptJobRequest(Guid jobRequestId, Guid jobId, Guid adminId, Guid staffId)
    {
        var response = await _jobRequestService.ApproveJobRequest(jobRequestId, jobId, adminId, staffId);
        return StatusCode(response.StatusCode, new { message = response.Message });
    }

    [HttpPatch]
    [Route("job-request/decline")]
    public async Task<ActionResult> DeclineJobRequest(Guid jobRequestId, Guid adminId, Guid staffId)
    {
        var response = await _jobRequestService.DeclineJobRequest(jobRequestId, adminId, staffId);
        return StatusCode(response.StatusCode, new { message = response.Message });
    }

}