using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class JobController : ControllerBase
{

    private readonly DataContext _context;
    public JobController(DataContext context)
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

    [HttpGet("daysofjobsbymonth")]
    public async Task<ActionResult<List<MonthJobsDtoRes>>> GetDaysOfJobsByMonth(int month, int year)
    {
        var jobs = await _context.Jobs
            .Where(e => e.DateTime.Year == year && e.DateTime.Month == month)
            .ToListAsync();


        var groupedByDay = jobs
            .GroupBy(job => job.DateTime.Day)
            .Select(group => new DayJobsDtoRes
            {
                day = group.Key,
                jobQuantity = group.Count(),
                jobs = group.Select(job => new JobDto
                {
                    Id = job.Id,
                    Title = job.Title,
                    Company = job.Company,
                    BaseRate = job.BaseRate,
                    DateTime = job.DateTime,
                    Location = job.Location,
                    Staffs = job.Staffs
                }).ToList()
            }).ToList();


        MonthJobsDtoRes monthJobs = new MonthJobsDtoRes{
            year = year,
            month = month,
            days = groupedByDay
        };

        return Ok(monthJobs);
    }

    [HttpGet("nextjobs")]
    public async Task<ActionResult<List<Job>>> GetNextJobs()
    {
        var jobs = await _context.Jobs.ToListAsync();

        return Ok(jobs);
    }

    [HttpGet("pastjobs")]
    public async Task<ActionResult<List<Job>>> GetPastJobs()
    {
        var jobs = await _context.Jobs.ToListAsync();

        return Ok(jobs);
    }

    [HttpPost]
    // [Route("job"), Authorize(Roles = "admin,staff")]
    [Route("job")]
    public async Task<ActionResult> CreateJob(JobDto request)
    {
        Job job = new Job();

        var company = await _context.Companies.FindAsync(request.Company);

        if(company is null){
            return BadRequest("Company does not exist");
        }
        
        job.Title = request.Title;
        job.Company = request.Company;
        job.BaseRate = request.BaseRate;
        job.DateTime = request.DateTime;
        job.Location = request.Location;
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