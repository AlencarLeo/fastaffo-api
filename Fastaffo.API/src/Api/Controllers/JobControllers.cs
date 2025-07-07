using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;


[Route("api/")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    [Route("job/{jobId}")]
    public async Task<ActionResult<JobDtoRes>> GetJobById(Guid jobId)
    {
        try
        {
            var result = await _jobService.GetJobByIdAsync(jobId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("job")]
    public async Task<ActionResult<List<JobDtoRes>>> GetJobs(int page = 1, int pageSize = 10)
    {
        try
        {
            var result = await _jobService.GetJobsAsync(page, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("job")]
    public async Task<ActionResult> CreateJob(JobDtoReq request)
    {
        try
        {
            await _jobService.CreateJobAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
