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
        var response = await _jobRequestService.CreateJobRequestAsync(request.Type, request.JobId, request.StaffId);
        return StatusCode(response.StatusCode, new { message = response.Message });
    }

}