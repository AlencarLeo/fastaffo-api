using System.Security.Claims;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
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


    private readonly IJobService _jobService;
    private readonly DataContext _context;
    public JobController(IJobService jobService, DataContext context)
    {
        _jobService = jobService;
        _context = context;
    }

    [HttpGet]
    [Route("job/{id}"), Authorize]
    public async Task<ActionResult<JobDtoRes>> GetJobById(Guid id)
    {
        var job = await _context.Jobs
        .Include(j => j.JobRequests)
            .ThenInclude(jr => jr.Staff)
        .Include(j => j.JobStaffs)
            .ThenInclude(js => js.Staff)
        .FirstOrDefaultAsync(j => j.Id == id);

        if (job is null)
        {
            return NotFound("Job not found.");
        }

        JobDtoRes jobRes = new JobDtoRes
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

            UtcStartDateTime = job.UtcStartDateTime.UtcDateTime,
            OriginalLocalStartDateTime = job.LocalStartDateTime,
            ReconstructedLocalStartDateTime = job.UtcStartDateTime.UtcDateTime
                                                .Add(
                                                    TimeZoneInfo
                                                    .FindSystemTimeZoneById(job.StartDateTimeZoneLocation)
                                                    .GetUtcOffset(job.UtcStartDateTime.UtcDateTime)
                                                ),
            StartDateTimeZoneLocation = job.StartDateTimeZoneLocation,

            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds = job.AllowedForJobStaffIds,
            JobRequests = job.JobRequests?.Select(jr => new JobRequestDtoRes
            {
                Id = jr.Id,
                JobId = jr.JobId,
                StaffId = jr.StaffId,
                Staff = new UserStaffDtoRes
                {
                    Id = jr.Staff.Id,
                    Role = jr.Staff.Role,
                    FirstName = jr.Staff.FirstName,
                    LastName = jr.Staff.LastName,
                    Phone = jr.Staff.Phone,
                    Email = jr.Staff.Email
                },
                Status = jr.Status,
                Type = jr.Type,
                RequestedAt = jr.RequestedAt,
            }).ToList(),
            JobStaffs = job.JobStaffs?.Select(js => new JobStaffDtoRes
            {
                Id = js.Id,
                JobId = js.JobId,
                StaffId = js.StaffId,
                Staff = new UserStaffDtoRes
                {
                    Id = js.Staff.Id,
                    Role = js.Staff.Role,
                    FirstName = js.Staff.FirstName,
                    LastName = js.Staff.LastName,
                    Phone = js.Staff.Phone,
                    Email = js.Staff.Email
                },
                JobRole = js.JobRole,
                AddRate = js.AddRate,
                AddStartDateTime = js.AddStartDateTime
            }).ToList()
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
            .Where(j => j.LocalStartDateTime > DateTime.Now)
            .Where(j => j.CurrentStaffCount < j.MaxStaffNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Staff)
            .Include(j => j.JobStaffs)
                .ThenInclude(js => js.Staff)
            .ToListAsync();

        // if(jobs is null || jobs.Count == 0)
        // {
        //     return NotFound("No opened jobs found.");
        // }

        var jobRes = jobs.Select(job => new JobDtoRes
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

            UtcStartDateTime = job.UtcStartDateTime.UtcDateTime,
            OriginalLocalStartDateTime = job.LocalStartDateTime,
            ReconstructedLocalStartDateTime = job.UtcStartDateTime.UtcDateTime
                                                .Add(
                                                    TimeZoneInfo
                                                    .FindSystemTimeZoneById(job.StartDateTimeZoneLocation)
                                                    .GetUtcOffset(job.UtcStartDateTime.UtcDateTime)
                                                ),
            StartDateTimeZoneLocation = job.StartDateTimeZoneLocation,

            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds = job.AllowedForJobStaffIds,
            JobRequests = job.JobRequests?.Select(jr => new JobRequestDtoRes
            {
                Id = jr.Id,
                JobId = jr.JobId,
                StaffId = jr.StaffId,
                Staff = new UserStaffDtoRes
                {
                    Id = jr.Staff.Id,
                    Role = jr.Staff.Role,
                    FirstName = jr.Staff.FirstName,
                    LastName = jr.Staff.LastName,
                    Phone = jr.Staff.Phone,
                    Email = jr.Staff.Email
                },
                Status = jr.Status,
                Type = jr.Type,
                RequestedAt = jr.RequestedAt,
            }).ToList(),
            JobStaffs = job.JobStaffs?.Select(js => new JobStaffDtoRes
            {
                Id = js.Id,
                JobId = js.JobId,
                StaffId = js.StaffId,
                Staff = new UserStaffDtoRes
                {
                    Id = js.Staff.Id,
                    Role = js.Staff.Role,
                    FirstName = js.Staff.FirstName,
                    LastName = js.Staff.LastName,
                    Phone = js.Staff.Phone,
                    Email = js.Staff.Email
                },
                JobRole = js.JobRole,
                AddRate = js.AddRate,
                AddStartDateTime = js.AddStartDateTime
            }).ToList()
        }).ToList();

        int totalCount = jobs.Count;
        var result = new PaginatedDto<JobDtoRes>(jobRes, totalCount, page, pageSize);

        return Ok(result);
    }

    [HttpGet]
    [Route("CompanyJobsByDateRange"), Authorize(Roles = "admin")]
    public async Task<ActionResult<List<JobDtoRes>>> GetCompanyJobsByDateRange(DateTime jobLocalStartDate, DateTime jobLocalEndDate)
    {
        // DateTime jobLocalStartDate => HORARIO EM Q STAFF TEM Q TAR NO LOCAL DO JOB (INDEPENDENTE DO FUSO)
        // DateTime jobLocalEndDate => HORARIO EM Q STAFF TEM Q TAR NO LOCAL DO JOB (INDEPENDENTE DO FUSO)

        // FUTURAMENTE MOSTRA NA TELA DO ADMIN HORA Q O JOB VAI ESTAR ACONTECENDO PARA ELE

        // req -> jobLocalStartDate = 2030-02-02T00:00:00; jobLocalEndDate = 2030-02-08T00:00:00 (melb)

        // j.LocalStartDateTime => se comeca 00am de la (seja qual for o fuso) -> vai mostrar no dash bord q dia 2/2/25 por exemplo vai ter esse job la 00am 
        // MESMO Q PRA MIM SEJA OUTRO HORARIO -> TAMBEM VAI PARECE DPS

        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userAdmin = await _context.Admins.FindAsync(Guid.Parse(id!));

        if (userAdmin is null)
        {
            return NotFound("Admin user not found.");
        }

        var jobs = await _context.Jobs
            .Where(j => j.CompanyId == userAdmin.CompanyId)
            .Where(j => j.LocalStartDateTime >= jobLocalStartDate.Date && j.LocalStartDateTime <= jobLocalEndDate.Date.AddDays(1).AddTicks(-1))
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Staff)
            .Include(j => j.JobStaffs)
                .ThenInclude(js => js.Staff)
            .ToListAsync();

        if (jobs is null)
        {
            return NotFound("No jobs found.");
        }


        // string StartDateTimeZoneLocation -> Time zone da location do job
        // DateTimeOffset UtcStartDateTime -> horario do job em utc
        // DateTimeOffset OriginalLocalStartDateTime ->  horario do job na location do job
        // DateTime ReconstructedLocalStartDateTime ->  horario do job na location do dashboard do admin


        var jobDtoRes = jobs.Select(job => new JobDtoRes
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

            UtcStartDateTime = job.UtcStartDateTime.UtcDateTime,
            OriginalLocalStartDateTime = job.LocalStartDateTime,
            ReconstructedLocalStartDateTime = job.UtcStartDateTime.UtcDateTime
                                                .Add(
                                                    TimeZoneInfo
                                                    .FindSystemTimeZoneById(job.StartDateTimeZoneLocation)
                                                    .GetUtcOffset(job.UtcStartDateTime.UtcDateTime)
                                                ),
            StartDateTimeZoneLocation = job.StartDateTimeZoneLocation,

            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds = job.AllowedForJobStaffIds,
            JobRequests = job.JobRequests?.Select(jr => new JobRequestDtoRes
            {
                Id = jr.Id,
                JobId = jr.JobId,
                StaffId = jr.StaffId,
                Staff = new UserStaffDtoRes
                {
                    Id = jr.Staff.Id,
                    Role = jr.Staff.Role,
                    FirstName = jr.Staff.FirstName,
                    LastName = jr.Staff.LastName,
                    Phone = jr.Staff.Phone,
                    Email = jr.Staff.Email
                },
                Status = jr.Status,
                Type = jr.Type,
                RequestedAt = jr.RequestedAt,
            }).ToList(),
            JobStaffs = job.JobStaffs?.Select(js => new JobStaffDtoRes
            {
                Id = js.Id,
                JobId = js.JobId,
                StaffId = js.StaffId,
                Staff = new UserStaffDtoRes
                {
                    Id = js.Staff.Id,
                    Role = js.Staff.Role,
                    FirstName = js.Staff.FirstName,
                    LastName = js.Staff.LastName,
                    Phone = js.Staff.Phone,
                    Email = js.Staff.Email
                },
                JobRole = js.JobRole,
                AddRate = js.AddRate,
                AddStartDateTime = js.AddStartDateTime
            }).ToList()
        }).ToList();

        return Ok(jobDtoRes);
    }

    // [HttpGet]
    // [Route("daysofjobsbymonth"), Authorize]
    // public async Task<ActionResult<List<MonthJobsDtoRes>>> GetDaysOfJobsByMonth(int month, int year)
    // {
    //     var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //     var jobs = await _context.Jobs
    //         .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId ==  Guid.Parse(id!)))
    //         .Where(e => e.StartDateTime.Year == year && e.StartDateTime.Month == month)
    //         .Include(job => job.JobRequests)
    //         .Include(job => job.JobStaffs)
    //         .ToListAsync();

    //     var groupedByDay = jobs
    //         .GroupBy(job => job.StartDateTime.Day)
    //         .Select(group => new DayJobsDtoRes
    //         {
    //             day = group.Key,
    //             jobQuantity = group.Count(),
    //             jobs = group.Select(job => new JobDtoRes
    //             {
    //                 Id = job.Id,
    //                 JobNumber = job.JobNumber,
    //                 Client = job.Client,
    //                 Event = job.Event,
    //                 TotalChargedValue = job.TotalChargedValue,
    //                 JobDuration = job.JobDuration,
    //                 Title = job.Title,
    //                 CompanyId = job.CompanyId,
    //                 CompanyName = job.CompanyName,
    //                 BaseRate = job.BaseRate,
    //                 StartDateTime = job.StartDateTime,
    //                 FinishDateTime = job.FinishDateTime,
    //                 Location = job.Location,
    //                 IsClosed = job.IsClosed,
    //                 MaxStaffNumber = job.MaxStaffNumber,
    //                 CurrentStaffCount = job.CurrentStaffCount,
    //                 AcceptingReqs = job.AcceptingReqs,
    //                 AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
    //                 JobRequests  = job.JobRequests ,
    //                 JobStaffs = job.JobStaffs
    //             }).ToList()
    //         }).ToList();


    //     MonthJobsDtoRes monthJobs = new MonthJobsDtoRes{
    //         year = year,
    //         month = month,
    //         days = groupedByDay
    //     };

    //     return Ok(monthJobs);
    // }

    // [HttpGet]
    // [Route("nextjobs"), Authorize]
    // public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetNextJobs(int page = 1, int pageSize = 5)
    // {
    //     var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //     var totalCount = await _context.Jobs.Where(job => job.StartDateTime < DateTime.Now).CountAsync();
    //     var jobs = await _context.Jobs
    //         .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId ==  Guid.Parse(id!)))
    //         .Where(job => job.StartDateTime > DateTime.Now)
    //         .OrderBy(i => i.StartDateTime)
    //         .Skip((page - 1) * pageSize)
    //         .Take(pageSize)
    //         .Include(job => job.JobRequests)
    //         .Include(job => job.JobStaffs)
    //         .ToListAsync();

    //     List<JobDtoRes> jobDtos = jobs
    //     .Select(job => new JobDtoRes
    //     {
    //         Id = job.Id,
    //         JobNumber = job.JobNumber,
    //         Client = job.Client,
    //         Event = job.Event,
    //         TotalChargedValue = job.TotalChargedValue,
    //         JobDuration = job.JobDuration,
    //         Title = job.Title,
    //         CompanyId = job.CompanyId,
    //         CompanyName = job.CompanyName,
    //         BaseRate = job.BaseRate,
    //         StartDateTime = job.StartDateTime,
    //         FinishDateTime = job.FinishDateTime,
    //         Location = job.Location,
    //         IsClosed = job.IsClosed,
    //         MaxStaffNumber = job.MaxStaffNumber,
    //         CurrentStaffCount = job.CurrentStaffCount,
    //         AcceptingReqs = job.AcceptingReqs,
    //         AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
    //         JobRequests  = job.JobRequests ,
    //         JobStaffs = job.JobStaffs
    //     }).ToList();

    //     var result = new PaginatedDto<JobDtoRes>(jobDtos, totalCount, page, pageSize);

    //     return Ok(result);
    // }

    // [HttpGet("pastjobs"), Authorize]
    // public async Task<ActionResult<PaginatedDto<JobDtoRes>>> GetPastJobs(int page = 1, int pageSize = 5)
    // {
    //     var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //     var totalCount = await _context.Jobs.Where(job => job.StartDateTime < DateTime.Now).CountAsync();

    //     var jobs = await _context.Jobs
    //         .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId ==  Guid.Parse(id!)))
    //         .Where(job => job.StartDateTime < DateTime.Now)
    //         .OrderByDescending(i => i.StartDateTime)
    //         .Skip((page - 1) * pageSize)
    //         .Take(pageSize)
    //         .Include(job => job.JobRequests)
    //         .Include(job => job.JobStaffs)
    //         .ToListAsync();

    //     List<JobDtoRes> jobDtos = jobs
    //     .Select(job => new JobDtoRes
    //     {
    //         Id = job.Id,
    //         JobNumber = job.JobNumber,
    //         Client = job.Client,
    //         Event = job.Event,
    //         TotalChargedValue = job.TotalChargedValue,
    //         JobDuration = job.JobDuration,
    //         Title = job.Title,
    //         CompanyId = job.CompanyId,
    //         CompanyName = job.CompanyName,
    //         BaseRate = job.BaseRate,
    //         StartDateTime = job.StartDateTime,
    //         FinishDateTime = job.FinishDateTime,
    //         Location = job.Location,
    //         IsClosed = job.IsClosed,
    //         MaxStaffNumber = job.MaxStaffNumber,
    //         CurrentStaffCount = job.CurrentStaffCount,
    //         AcceptingReqs = job.AcceptingReqs,
    //         AllowedForJobStaffIds  = job.AllowedForJobStaffIds ,
    //         JobRequests  = job.JobRequests ,
    //         JobStaffs = job.JobStaffs
    //     }).ToList();

    //     var result = new PaginatedDto<JobDtoRes>(jobDtos, totalCount, page, pageSize);

    //     return Ok(result);
    // }

    [HttpPut]
    [Route("job"), Authorize(Roles = "admin")]
    public async Task<ActionResult> UpdateJob(Guid jobId, JobDtoReq request)
    {
        var job = await _context.Jobs.FindAsync(jobId);
        if (job == null)
        {
            return NotFound("Job not found");
        }

        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(request.StartDateTimeZoneLocation);
        var originalLocalDate = request.LocalStartDateTime;
        var originalLocalDateByTimeZoneInfo = new DateTimeOffset(originalLocalDate, timeZoneInfo.GetUtcOffset(originalLocalDate)); ;
        var utcDate = TimeZoneInfo.ConvertTimeToUtc(originalLocalDate, timeZoneInfo);

        job.Title = request.Title;
        job.Client = request.Client;
        job.Event = request.Event;
        job.TotalChargedValue = request.TotalChargedValue;
        job.BaseRate = request.BaseRate;
        job.StartDateTimeZoneLocation = request.StartDateTimeZoneLocation;
        job.UtcStartDateTime = new DateTimeOffset(utcDate);
        job.LocalStartDateTime = originalLocalDateByTimeZoneInfo;
        job.Location = request.Location;
        job.MaxStaffNumber = request.MaxStaffNumber;
        job.AcceptingReqs = request.AcceptingReqs;
        job.AllowedForJobStaffIds = request.AllowedForJobStaffIds ?? null;

        _context.Update(job);

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    [Route("job"), Authorize(Roles = "admin")]
    public async Task<ActionResult> CreateJob(JobDtoReq request)
    {
        var response = await _jobService.CreateJob(request);
        return StatusCode(response.StatusCode, new { message = response.Message });
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