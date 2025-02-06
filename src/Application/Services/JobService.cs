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
}