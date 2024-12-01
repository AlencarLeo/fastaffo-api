using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class JobsController : ControllerBase
{

    private readonly DataContext _context;
    public JobsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("job/{id}")]
    public async Task<ActionResult<Job>> GetJobById(Guid id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if(job is null)
        {
            return NotFound("Job not found.");
        }

        return Ok(job);
    }

    [HttpGet("nextjob")]
    public async Task<ActionResult<List<Job>>> GetNextJob()
    {
        var jobs = await _context.Jobs.ToListAsync();

        return Ok(jobs);
    }

    [HttpPost]
    [Route("job")]
    public async Task<ActionResult> CreateJob(JobDto request)
    {
        Job job = new Job();
        
        job.Title = request.Title;
        job.Company = request.Company;
        job.BaseRate = request.BaseRate;
        job.DateTime = request.DateTime;
        job.Location = request.Location;

        // request.Staffs

        job.Staffs = request.Staffs;
        
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("job/{id}")]
    public async Task<ActionResult> DeleteJob(Guid id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if(job is null)
        {
            return NotFound("Job not found.");
        }

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();

        return Ok();
    }

}