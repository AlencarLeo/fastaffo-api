using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;
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
    [Route("job/{id}"), Authorize]
    public async Task<ActionResult<JobDtoRes>> GetJobById(Guid id)
    {
        var response = await _jobService.GetJobById(id);
        return StatusCode(response.StatusCode, response.Data);
    }

    [HttpGet]
    [Route("openedjobs"), Authorize]
    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetOpenedJobs(int page = 1, int pageSize = 10)
    {
        var response = await _jobService.GetOpenedJobs(page, pageSize);
        return StatusCode(response.StatusCode, response.Data);
    }

    [HttpGet]
    [Route("CompanyJobsByDateRange"), Authorize(Roles = "admin")]
    public async Task<ActionResult<List<JobDtoRes>>> GetCompanyJobsByDateRange(DateTime jobLocalStartDate, DateTime jobLocalEndDate)
    {
        var response = await _jobService.GetCompanyJobsByDateRange(jobLocalStartDate, jobLocalEndDate);
        return StatusCode(response.StatusCode, response.Data);
    }

    [HttpGet]
    [Route("daysofjobsbymonth"), Authorize]
    public async Task<ActionResult<List<MonthJobsDtoRes>>> GetDaysOfJobsByMonth(int month, int year)
    {
        var response = await _jobService.GetDaysOfJobsByMonth(month, year);
        return StatusCode(response.StatusCode, response.Data);
    }

    [HttpGet("pastjobs"), Authorize]
    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetPastJobs(int page = 1, int pageSize = 5)
    {
        var response = await _jobService.GetPastJobs(page, pageSize);
        return StatusCode(response.StatusCode, response.Data);
    }

    [HttpGet]
    [Route("nextjobs"), Authorize]
    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetNextJobs(int page = 1, int pageSize = 5)
    {
        var response = await _jobService.GetNextJobs(page, pageSize);
        return StatusCode(response.StatusCode, response.Data);
    }

    [HttpPost]
    [Route("job"), Authorize(Roles = "admin")]
    public async Task<ActionResult> CreateJob(JobDtoReq request)
    {
        var response = await _jobService.CreateJob(request);
        return StatusCode(response.StatusCode, new { message = response.Message });
    }

    [HttpPut]
    [Route("job"), Authorize(Roles = "admin")]
    public async Task<ActionResult> UpdateJob(Guid jobId, JobDtoReq request)
    {
        var response = await _jobService.UpdateJob(jobId, request);
        return StatusCode(response.StatusCode, new { message = response.Message });
    }

    // [HttpPost]
    // [Route("job/myself"), Authorize(Roles = "staff")]
    // public async Task<ActionResult> CreateJobToMyself(JobDtoReq request)
    // {
    //     Job job = new Job();

    //     var company = await _context.Companies.FindAsync(request.CompanyId);

    //     if(company is null){
    //         return BadRequest("Company does not exist");
    //     }

    //     job.Title = request.Title;
    //     job.CompanyId = request.CompanyId;
    //     job.BaseRate = request.BaseRate;
    //     job.StartDateTime = request.StartDateTime;
    //     job.Location = request.Location;
    //     job.StaffsId = request.StaffsId;

    //     _context.Jobs.Add(job);
    //     await _context.SaveChangesAsync();

    //     return Ok();
    // }

    // [HttpDelete]
    // [Route("job/{id}")]
    // public async Task<ActionResult> DeleteJob(Guid id)
    // {
    //     var job = await _context.Jobs.FindAsync(id);

    //     if(job is null)
    //     {
    //         return NotFound("Job not found.");
    //     }

    //     _context.Jobs.Remove(job);
    //     await _context.SaveChangesAsync();

    //     return Ok();
    // }

}