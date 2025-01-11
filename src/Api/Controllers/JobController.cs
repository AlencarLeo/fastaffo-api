using System.Security.Claims;
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
    [Route("job/{id}"), Authorize]
    public async Task<ActionResult<JobDtoRes>> GetJobById(Guid id)
    {
        var job = await _context.Jobs
        .Include(j => j.JobRequests)
        .Include(j => j.JobStaffs)
        .FirstOrDefaultAsync(j => j.Id == id);

        if(job is null)
        {
            return NotFound("Job not found.");
        }

        JobDtoRes jobRes = new JobDtoRes{
            Id = job.Id,
            JobNumber = job.JobNumber,
            Client = job.Client,
            Event = job.Event,
            TotalChargedValue = job.TotalChargedValue,
            JobDuration = job.JobDuration,
            Title = job.Title,
            CompanyId = job.CompanyId,
            CompanyName = job.CompanyName,
            BaseRate = job.BaseRate,
            StartDateTime = job.StartDateTime,
            FinishDateTime = job.FinishDateTime,
            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
            JobRequests  = job.JobRequests ,
            JobStaffs = job.JobStaffs
        };

        return Ok(jobRes);
    }

    [HttpGet]
    [Route("openedjobs"), Authorize]
    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetOpenedJobs(int page = 1, int pageSize = 10)
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var jobs = await _context.Jobs
            .Where(j => j.AcceptingReqs)
            .Where(j => j.AllowedForJobStaffIds == null || !j.AllowedForJobStaffIds.Any() || j.AllowedForJobStaffIds.Contains(Guid.Parse(id!)))
            .Where(j => !j.IsClosed)
            .Where(j => j.StartDateTime > DateTime.Now)
            .Where(j => j.CurrentStaffCount < j.MaxStaffNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(job => job.JobRequests)
            .Include(job => job.JobStaffs)
            .ToListAsync();

        if(jobs is null || jobs.Count == 0)
        {
            return NotFound("No opened jobs found.");
        }

        var jobRes = jobs.Select(job => new JobDtoRes{
            Id = job.Id,
            JobNumber = job.JobNumber,
            Client = job.Client,
            Event = job.Event,
            TotalChargedValue = job.TotalChargedValue,
            JobDuration = job.JobDuration,
            Title = job.Title,
            CompanyId = job.CompanyId,
            CompanyName = job.CompanyName,
            BaseRate = job.BaseRate,
            StartDateTime = job.StartDateTime,
            FinishDateTime = job.FinishDateTime,
            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
            JobRequests  = job.JobRequests ,
            JobStaffs = job.JobStaffs
        }).ToList();

        int totalCount = jobs.Count;
        var result = new PaginatedDto<JobDtoRes>(jobRes, totalCount, page, pageSize);

        return Ok(result);
    }

    [HttpGet]
    [Route("companyjobsbydate"), Authorize(Roles = "admin")]
    public async Task<ActionResult<List<MonthJobsDtoRes>>> GetCompanyJobs(int month, int year)
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var userAdmin = await _context.Admins.FindAsync(Guid.Parse(id!));

        if (userAdmin is null){
            return NotFound("Admin user not found.");
        }

        var jobs = await _context.Jobs
            .Where(j => j.CompanyId == userAdmin.CompanyId)
            .Where(e => e.StartDateTime.Year == year && e.StartDateTime.Month == month)
            .Include(j => j.JobRequests)
            .Include(j => j.JobStaffs)
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
                    JobNumber = job.JobNumber,
                    Client = job.Client,
                    Event = job.Event,
                    TotalChargedValue = job.TotalChargedValue,
                    JobDuration = job.JobDuration,
                    Title = job.Title,
                    CompanyId = job.CompanyId,
                    CompanyName = job.CompanyName,
                    BaseRate = job.BaseRate,
                    StartDateTime = job.StartDateTime,
                    FinishDateTime = job.FinishDateTime,
                    Location = job.Location,
                    IsClosed = job.IsClosed,
                    MaxStaffNumber = job.MaxStaffNumber,
                    CurrentStaffCount = job.CurrentStaffCount,
                    AcceptingReqs = job.AcceptingReqs,
                    AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
                    JobRequests  = job.JobRequests ,
                    JobStaffs = job.JobStaffs
                }).ToList()
            }).ToList();


        MonthJobsDtoRes monthJobs = new MonthJobsDtoRes{
            year = year,
            month = month,
            days = groupedByDay
        };

        return Ok(monthJobs);
    }

    [HttpGet]
    [Route("daysofjobsbymonth"), Authorize]
    public async Task<ActionResult<List<MonthJobsDtoRes>>> GetDaysOfJobsByMonth(int month, int year)
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var jobs = await _context.Jobs
            .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId ==  Guid.Parse(id!)))
            .Where(e => e.StartDateTime.Year == year && e.StartDateTime.Month == month)
            .Include(job => job.JobRequests)
            .Include(job => job.JobStaffs)
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
                    JobNumber = job.JobNumber,
                    Client = job.Client,
                    Event = job.Event,
                    TotalChargedValue = job.TotalChargedValue,
                    JobDuration = job.JobDuration,
                    Title = job.Title,
                    CompanyId = job.CompanyId,
                    CompanyName = job.CompanyName,
                    BaseRate = job.BaseRate,
                    StartDateTime = job.StartDateTime,
                    FinishDateTime = job.FinishDateTime,
                    Location = job.Location,
                    IsClosed = job.IsClosed,
                    MaxStaffNumber = job.MaxStaffNumber,
                    CurrentStaffCount = job.CurrentStaffCount,
                    AcceptingReqs = job.AcceptingReqs,
                    AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
                    JobRequests  = job.JobRequests ,
                    JobStaffs = job.JobStaffs
                }).ToList()
            }).ToList();


        MonthJobsDtoRes monthJobs = new MonthJobsDtoRes{
            year = year,
            month = month,
            days = groupedByDay
        };

        return Ok(monthJobs);
    }

    [HttpGet]
    [Route("nextjobs"), Authorize]

    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetNextJobs(int page = 1, int pageSize = 5)
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var totalCount = await _context.Jobs.Where(job => job.StartDateTime < DateTime.Now).CountAsync();
        var jobs = await _context.Jobs
            .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId ==  Guid.Parse(id!)))
            .Where(job => job.StartDateTime > DateTime.Now)
            .OrderBy(i => i.StartDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(job => job.JobRequests)
            .Include(job => job.JobStaffs)
            .ToListAsync();

        List<JobDtoRes> jobDtos = jobs
        .Select(job => new JobDtoRes
        {
            Id = job.Id,
            JobNumber = job.JobNumber,
            Client = job.Client,
            Event = job.Event,
            TotalChargedValue = job.TotalChargedValue,
            JobDuration = job.JobDuration,
            Title = job.Title,
            CompanyId = job.CompanyId,
            CompanyName = job.CompanyName,
            BaseRate = job.BaseRate,
            StartDateTime = job.StartDateTime,
            FinishDateTime = job.FinishDateTime,
            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
            JobRequests  = job.JobRequests ,
            JobStaffs = job.JobStaffs
        }).ToList();

        var result = new PaginatedDto<JobDtoRes>(jobDtos, totalCount, page, pageSize);
                
        return Ok(result);
    }

    [HttpGet("pastjobs"), Authorize]
    public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetPastJobs(int page = 1, int pageSize = 5)
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var totalCount = await _context.Jobs.Where(job => job.StartDateTime < DateTime.Now).CountAsync();

        var jobs = await _context.Jobs
            .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId ==  Guid.Parse(id!)))
            .Where(job => job.StartDateTime < DateTime.Now)
            .OrderByDescending(i => i.StartDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(job => job.JobRequests)
            .Include(job => job.JobStaffs)
            .ToListAsync();

        List<JobDtoRes> jobDtos = jobs
        .Select(job => new JobDtoRes
        {
            Id = job.Id,
            JobNumber = job.JobNumber,
            Client = job.Client,
            Event = job.Event,
            TotalChargedValue = job.TotalChargedValue,
            JobDuration = job.JobDuration,
            Title = job.Title,
            CompanyId = job.CompanyId,
            CompanyName = job.CompanyName,
            BaseRate = job.BaseRate,
            StartDateTime = job.StartDateTime,
            FinishDateTime = job.FinishDateTime,
            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
            JobRequests  = job.JobRequests ,
            JobStaffs = job.JobStaffs
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
        job.MaxStaffNumber = request.MaxStaffNumber;

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return Ok();
    }


//     // [HttpPost]
//     // // [Route("job"), Authorize(Roles = "admin,staff")]
//     // [Route("job/myself")]
//     // public async Task<ActionResult> CreateJobToMyself(JobDtoReq request)
//     // {
//     //     Job job = new Job();

//     //     var company = await _context.Companies.FindAsync(request.CompanyId);

//     //     if(company is null){
//     //         return BadRequest("Company does not exist");
//     //     }
        
//     //     job.Title = request.Title;
//     //     job.CompanyId = request.CompanyId;
//     //     job.BaseRate = request.BaseRate;
//     //     job.StartDateTime = request.StartDateTime;
//     //     job.Location = request.Location;
//     //     job.StaffsId = request.StaffsId;
        
//     //     _context.Jobs.Add(job);
//     //     await _context.SaveChangesAsync();

//     //     return Ok();
//     // }

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