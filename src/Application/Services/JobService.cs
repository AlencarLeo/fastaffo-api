using System.Security.Claims;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Interfaces;
public class JobService : IJobService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    public JobService(IHttpContextAccessor httpContextAccessor, DataContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    private List<JobDtoRes> ConvertJobsToDto(IEnumerable<Job> jobs)
    {
        return jobs.Select(ConvertJobToDto).ToList();
    }

    private JobDtoRes ConvertJobToDto(Job job)
    {
        // DateTime jobLocalStartDate => HORARIO EM Q STAFF TEM Q TAR NO LOCAL DO JOB (INDEPENDENTE DO FUSO)
        // DateTime jobLocalEndDate => HORARIO EM Q STAFF TEM Q TAR NO LOCAL DO JOB (INDEPENDENTE DO FUSO)

        // FUTURAMENTE MOSTRA NA TELA DO ADMIN HORA Q O JOB VAI ESTAR ACONTECENDO PARA ELE

        // req -> jobLocalStartDate = 2030-02-02T00:00:00; jobLocalEndDate = 2030-02-08T00:00:00 (melb)

        // j.LocalStartDateTime => se comeca 00am de la (seja qual for o fuso) -> vai mostrar no dash bord q dia 2/2/25 por exemplo vai ter esse job la 00am 
        // MESMO Q PRA MIM SEJA OUTRO HORARIO -> TAMBEM VAI PARECE DPS



        // string StartDateTimeZoneLocation -> Time zone da location do job
        // DateTimeOffset UtcStartDateTime -> horario do job em utc
        // DateTimeOffset OriginalLocalStartDateTime ->  horario do job na location do job
        // DateTime ReconstructedLocalStartDateTime ->  horario do job na location do dashboard do admin

        return new JobDtoRes
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
            ReconstructedLocalStartDateTime = GetReconstructedLocalStartDateTime(job),
            StartDateTimeZoneLocation = job.StartDateTimeZoneLocation,

            Location = job.Location,
            IsClosed = job.IsClosed,
            MaxStaffNumber = job.MaxStaffNumber,
            CurrentStaffCount = job.CurrentStaffCount,
            AcceptingReqs = job.AcceptingReqs,
            AllowedForJobStaffIds = job.AllowedForJobStaffIds,

            JobRequests = job.JobRequests?.Select(ConvertJobRequestToDto).ToList(),
            JobStaffs = job.JobStaffs?.Select(ConvertJobStaffToDto).ToList()
        };
    }

    private DateTime GetReconstructedLocalStartDateTime(Job job)
    {
        return job.UtcStartDateTime.UtcDateTime.Add(
            TimeZoneInfo.FindSystemTimeZoneById(job.StartDateTimeZoneLocation)
                        .GetUtcOffset(job.UtcStartDateTime.UtcDateTime)
        );
    }

    private JobRequestDtoRes ConvertJobRequestToDto(JobRequest jr)
    {
        return new JobRequestDtoRes
        {
            Id = jr.Id,
            JobId = jr.JobId,
            StaffId = jr.StaffId,
            Staff = ConvertUserStaffToDto(jr.Staff),
            AdminId = jr.AdminId,
            Admin = jr.Admin is not null ? ConvertUserAdminToDto(jr.Admin) : null,
            Status = jr.Status,
            Type = jr.Type,
            RequestedAt = jr.RequestedAt,
            ResponsedAt = jr.ResponsedAt
        };
    }

    private JobStaffDtoRes ConvertJobStaffToDto(JobStaff js)
    {
        return new JobStaffDtoRes
        {
            Id = js.Id,
            JobId = js.JobId,
            StaffId = js.StaffId,
            Staff = ConvertUserStaffToDto(js.Staff),
            JobRole = js.JobRole,
            AddRate = js.AddRate,
            AddStartDateTime = js.AddStartDateTime
        };
    }

    private UserAdminDtoRes ConvertUserAdminToDto(UserAdmin admin)
    {
        return new UserAdminDtoRes
        {
            Id = admin.Id,
            FirstName = admin.FirstName,
            LastName = admin.LastName,
            Email = admin.Email,
            Phone = admin.Phone,
            IsOwner = admin.IsOwner,
            Role = admin.Role
        };
    }

    private UserStaffDtoRes ConvertUserStaffToDto(UserStaff staff)
    {
        return new UserStaffDtoRes
        {
            Id = staff.Id,
            Role = staff.Role,
            FirstName = staff.FirstName,
            LastName = staff.LastName,
            Phone = staff.Phone,
            Email = staff.Email
        };
    }



    public async Task<ServiceResponseDto<JobDtoRes>> GetJobById(Guid id)
    {
        var job = await _context.Jobs
        .Include(j => j.JobRequests)
            .ThenInclude(jr => jr.Staff)
        .Include(j => j.JobRequests)
            .ThenInclude(jr => jr.Admin)
        .Include(j => j.JobStaffs)
            .ThenInclude(jr => jr.Staff)
        .AsNoTracking()
        .FirstOrDefaultAsync(j => j.Id == id);

        if (job is null)
        {
            return new ServiceResponseDto<JobDtoRes>(null, "Job not found.", 404);
        }

        JobDtoRes jobRes = ConvertJobToDto(job);

        return new ServiceResponseDto<JobDtoRes>(jobRes, "Success.", 200); ;
    }

    public async Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetOpenedJobs(int page = 1, int pageSize = 10)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(null, "User not found.", 404);
        }
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;


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
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Admin)
            .Include(j => j.JobStaffs)
                .ThenInclude(jr => jr.Staff)
            .AsNoTracking()
            .ToListAsync();

        // if(jobs is null || jobs.Count == 0)
        // {
        //     return NotFound("No opened jobs found.");
        // }

        var jobRes = ConvertJobsToDto(jobs);

        int totalCount = jobs.Count;
        var result = new PaginatedDto<JobDtoRes>(jobRes, totalCount, page, pageSize);

        return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(result, "User not found.", 200);
    }

    public async Task<ServiceResponseDto<List<JobDtoRes>>> GetCompanyJobsByDateRange(DateTime jobLocalStartDate, DateTime jobLocalEndDate)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            return new ServiceResponseDto<List<JobDtoRes>>(null, "User not found.", 404);
        }

        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userAdmin = await _context.Admins.FindAsync(Guid.Parse(id!));

        if (userAdmin is null)
        {
            return new ServiceResponseDto<List<JobDtoRes>>(null, "Admin user not found.", 404);
        }

        var jobs = await _context.Jobs
            .Where(j => j.CompanyId == userAdmin.CompanyId)
            .Where(j => j.LocalStartDateTime >= jobLocalStartDate.Date && j.LocalStartDateTime <= jobLocalEndDate.Date.AddDays(1).AddTicks(-1))
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Staff)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Admin)
            .Include(j => j.JobStaffs)
                .ThenInclude(jr => jr.Staff)
            .AsNoTracking()
            .ToListAsync();

        if (jobs is null)
        {
            return new ServiceResponseDto<List<JobDtoRes>>(null, "No jobs found.", 404);
        }

        var jobDtoRes = ConvertJobsToDto(jobs);

        return new ServiceResponseDto<List<JobDtoRes>>(jobDtoRes, "Success.", 200);
    }

    public async Task<ServiceResponseDto<MonthJobsDtoRes>> GetDaysOfJobsByMonth(int month, int year)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            return new ServiceResponseDto<MonthJobsDtoRes>(null, "User not found.", 404);
        }
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var jobs = await _context.Jobs
            .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId == Guid.Parse(id!)))
            .Where(e => e.LocalStartDateTime.Year == year && e.LocalStartDateTime.Month == month)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Staff)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Admin)
            .Include(j => j.JobStaffs)
                .ThenInclude(jr => jr.Staff)
            .AsNoTracking()
            .ToListAsync();

        var groupedByDay = jobs
            .GroupBy(job => job.LocalStartDateTime.Day)
            .Select(group => new DayJobsDtoRes
            {
                day = group.Key,
                jobQuantity = group.Count(),
                jobs = ConvertJobsToDto(group)
            }).ToList();


        MonthJobsDtoRes monthJobs = new MonthJobsDtoRes
        {
            year = year,
            month = month,
            days = groupedByDay
        };

        return new ServiceResponseDto<MonthJobsDtoRes>(monthJobs, "User not found.", 200);
    }

    public async Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetPastJobs(int page = 1, int pageSize = 5)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(null, "User not found.", 404);
        }
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var totalCount = await _context.Jobs.Where(job => job.LocalStartDateTime < DateTime.Now).CountAsync();

        var jobs = await _context.Jobs
            .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId == Guid.Parse(id!)))
            .Where(job => job.LocalStartDateTime < DateTime.Now)
            .OrderByDescending(i => i.LocalStartDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Staff)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Admin)
            .Include(j => j.JobStaffs)
                .ThenInclude(jr => jr.Staff)
            .AsNoTracking()
            .ToListAsync();

        var jobDtos = ConvertJobsToDto(jobs);

        var result = new PaginatedDto<JobDtoRes>(jobDtos, totalCount, page, pageSize);
        return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(result, "Success.", 200);
    }

    public async Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetNextJobs(int page = 1, int pageSize = 5)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(null, "User not found.", 404);
        }
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var totalCount = await _context.Jobs.Where(job => job.LocalStartDateTime < DateTime.Now).CountAsync();
        var jobs = await _context.Jobs
            .Where(job => job.JobStaffs != null && job.JobStaffs.Any(s => s.StaffId == Guid.Parse(id!)))
            .Where(job => job.LocalStartDateTime > DateTime.Now)
            .OrderBy(i => i.LocalStartDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Staff)
            .Include(j => j.JobRequests)
                .ThenInclude(jr => jr.Admin)
            .Include(j => j.JobStaffs)
                .ThenInclude(jr => jr.Staff)
            .AsNoTracking()
            .ToListAsync();

        var jobDtos = ConvertJobsToDto(jobs);

        var result = new PaginatedDto<JobDtoRes>(jobDtos, totalCount, page, pageSize);
        return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(result, "Success.", 200);
    }

    public async Task<ServiceResponseDto> CreateJob(JobDtoReq request)
    {
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(request.StartDateTimeZoneLocation);
        var originalLocalDate = request.LocalStartDateTime;
        var originalLocalDateByTimeZoneInfo = new DateTimeOffset(originalLocalDate, timeZoneInfo.GetUtcOffset(originalLocalDate)); ;
        var utcDate = TimeZoneInfo.ConvertTimeToUtc(originalLocalDate, timeZoneInfo);

        var id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userAdmin = await _context.Admins.FindAsync(Guid.Parse(id!));

        if (userAdmin is null)
        {
            return new ServiceResponseDto("Admin not found.", 404);
        }

        if (userAdmin.CompanyId == null)
        {
            return new ServiceResponseDto("Admin does not have an associated company.", 404);
        }

        var companyId = userAdmin.CompanyId.Value;
        var company = await _context.Companies.FindAsync(companyId);

        if (company is null)
        {
            return new ServiceResponseDto("Company does not exist.", 404);
        }

        var companyJobsCount = await _context.Jobs.Where(j => j.CompanyId == companyId).CountAsync();

        var job = new Job
        {
            Title = request.Title,
            JobNumber = companyJobsCount + 1,
            Client = request.Client,
            Event = request.Event,
            TotalChargedValue = request.TotalChargedValue,
            CompanyId = companyId,
            CompanyName = company.Name,
            BaseRate = request.BaseRate,
            StartDateTimeZoneLocation = request.StartDateTimeZoneLocation,
            UtcStartDateTime = new DateTimeOffset(utcDate),
            LocalStartDateTime = originalLocalDateByTimeZoneInfo,
            Location = request.Location,
            MaxStaffNumber = request.MaxStaffNumber,
            AcceptingReqs = request.AcceptingReqs,
            AllowedForJobStaffIds = request.AllowedForJobStaffIds ?? null
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return new ServiceResponseDto("Job created", 200);
    }

    public async Task<ServiceResponseDto> UpdateJob(Guid jobId, JobDtoReq request)
    {
        var job = await _context.Jobs.FindAsync(jobId);
        if (job == null)
        {
            return new ServiceResponseDto("Job not found.", 404);
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
        return new ServiceResponseDto("Success.", 200);
    }
}