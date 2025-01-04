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
    public async Task<ActionResult<JobDtoRes>> GetJobById(Guid id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if(job is null)
        {
            return NotFound("Job not found.");
        }

        JobDtoRes jobRes = new JobDtoRes{
            Title = job.Title,
            CompanyId = job.CompanyId ,
            CompanyName = job.CompanyName ,
            BaseRate = job.BaseRate ,
            StartDateTime = job.StartDateTime ,
            Location = job.Location ,
            StaffsId = job.StaffsId ,
        };

        return Ok(jobRes);
    }

    [HttpGet("daysofjobsbymonth")]
    public async Task<ActionResult<List<MonthJobsDtoRes>>> GetDaysOfJobsByMonth(int month, int year)
    {
        // FIX: FILTRAR O USUARIO -> suggest url = jobs/{userId}/{month}/{year} or daysofjobsbymonth/{userId} e mantem month e year como param

        var jobs = await _context.Jobs
            .Where(e => e.StartDateTime.Year == year && e.StartDateTime.Month == month)
            .ToListAsync();


        var groupedByDay = jobs
            .GroupBy(job => job.StartDateTime.Day)
            .Select(group => new DayJobsDtoRes
            {
                day = group.Key,
                jobQuantity = group.Count(),
                jobs = group.Select(job => new JobDtoRes
                {
                    Id = job.Id,
                    Title = job.Title,
                    CompanyId = job.CompanyId,
                    CompanyName = job.CompanyName,
                    BaseRate = job.BaseRate,
                    StartDateTime = job.StartDateTime,
                    Location = job.Location,
                    StaffsId = job.StaffsId
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
    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetNextJobs(int page = 1, int pageSize = 5)
    {
        var totalCount = await _context.Jobs.Where(job => job.StartDateTime < DateTime.Now).CountAsync();
        var jobs = await _context.Jobs
            .Where(job => job.StartDateTime > DateTime.Now)
            .OrderBy(i => i.StartDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        List<JobDtoRes> jobDtos = jobs
        .Select(job => new JobDtoRes
        {
            Title = job.Title,
            CompanyId = job.CompanyId,
            CompanyName = job.CompanyName,
            BaseRate = job.BaseRate,
            StartDateTime = job.StartDateTime,
            Location = job.Location,
            StaffsId = job.StaffsId
        }).ToList();

        var result = new PaginatedDto<JobDtoRes>(jobDtos, totalCount, page, pageSize);
                
        return Ok(result);
    }

    [HttpGet("pastjobs")]
    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetPastJobs(int page = 1, int pageSize = 5)
    {
        var totalCount = await _context.Jobs.Where(job => job.StartDateTime < DateTime.Now).CountAsync();

        var jobs = await _context.Jobs
            .Where(job => job.StartDateTime < DateTime.Now)
            .OrderByDescending(i => i.StartDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        List<JobDtoRes> jobDtos = jobs
        .Select(job => new JobDtoRes
        {
            Title = job.Title,
            CompanyId = job.CompanyId,
            CompanyName = job.CompanyName,
            BaseRate = job.BaseRate,
            StartDateTime = job.StartDateTime,
            Location = job.Location,
            StaffsId = job.StaffsId
        }).ToList();

        var result = new PaginatedDto<JobDtoRes>(jobDtos, totalCount, page, pageSize);
        
        return Ok(result);
    }

    [HttpPost]
    // [Route("job"), Authorize(Roles = "admin,staff")]
    [Route("job")]
    public async Task<ActionResult> CreateJob(JobDtoReq request)
    {
        Job job = new Job();

        var company = await _context.Companies.FindAsync(request.CompanyId);

        if(company is null){
            return BadRequest("Company does not exist");
        }
        
        job.Title = request.Title;
        job.CompanyId = request.CompanyId;
        job.CompanyName = company.Name;
        job.BaseRate = request.BaseRate;
        job.StartDateTime = request.StartDateTime;
        job.Location = request.Location;
        job.StaffsId = request.StaffsId?.Count > 0 ? request.StaffsId : null;

        
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // [HttpPost]
    // // [Route("job"), Authorize(Roles = "admin,staff")]
    // [Route("job/myself")]
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