using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Services;

public class JobService : IJobService
{
    private readonly DataContext _context;
    private readonly IValidatorService _validatorService;
    private readonly IValidator<JobDtoReq> _jobDtoReqValidator;
    public JobService(DataContext context, IValidatorService validatorService, IValidator<JobDtoReq> jobDtoReqValidator)
    {
        _context = context;
        _validatorService = validatorService;
        _jobDtoReqValidator = jobDtoReqValidator;
    }

    public async Task<ServiceResponseDto<JobDtoRes>> GetJobByIdAsync(Guid jobsId)
    {
        var job = await _context.Jobs
                                    .Include(j => j.Company!.ContactInfo)
                                    .Include(j => j.CreatedBy!.ContactInfo)
                                    .FirstOrDefaultAsync(j => j.Id == jobsId);

        if (job == null)
        {
            return new ServiceResponseDto<JobDtoRes>(null, "Staff member not found.", 404);
        }

        var jobDtoRes = JobMapper.ToDto(job);

        return new ServiceResponseDto<JobDtoRes>(jobDtoRes, "Job retrieved successfully.", 200);
    }

    public async Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetJobsAsync(int page = 1, int pageSize = 10)
    {
        var query = _context.Jobs
                            .Include(j => j.Company!.ContactInfo)
                            .Include(j => j.CreatedBy!.ContactInfo)
                            .AsQueryable();

        var totalCount = await query.CountAsync();

        var jobs = await query
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        if (jobs.Count == 0)
        {
            return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(null, "No jobs found.", 404);
        }

        var jobsDtoRes = jobs.Select(JobMapper.ToDto);

        var paginatedJobsDtoRes = new PaginatedDto<JobDtoRes>(jobsDtoRes, totalCount, page, pageSize);

        return new ServiceResponseDto<PaginatedDto<JobDtoRes>>(paginatedJobsDtoRes, "Job retrieved successfully.", 200);
    }

    public async Task CreateJobAsync(JobDtoReq request)
    {
        await _validatorService.ValidateAsync(_jobDtoReqValidator, request);

        var newJob = JobMapper.ToEntity(request);

        newJob.JobRef = await GenerateJobRefAsync(request.CompanyId);

        await _context.AddAsync(newJob);
        await _context.SaveChangesAsync();
    }

    public async Task<string> GenerateJobRefAsync(Guid companyId)
    {
        var companyName = await _context.Companies
            .Where(c => c.Id == companyId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        if (string.IsNullOrWhiteSpace(companyName))
            throw new Exception("Company not found.");

        var prefix = new string(companyName
            .ToUpper()
            .Where(char.IsLetter)
            .Take(3)
            .ToArray())
            .PadRight(3, 'X');

        var jobsCount = await _context.Jobs
            .CountAsync(j => j.CompanyId == companyId);

        var guidPart = Guid.NewGuid().ToString("N")[..4].ToUpper();

        return $"J-{prefix}-{guidPart}-{jobsCount + 1:D3}";
    }

}
