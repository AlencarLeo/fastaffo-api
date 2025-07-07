using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;

public interface IJobService
{
    public Task<ServiceResponseDto<JobDtoRes>> GetJobByIdAsync(Guid jobsId);
    public Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetJobsAsync(int page = 1, int pageSize = 10);
    public Task CreateJobAsync(JobDtoReq request);
    public Task<string> GenerateJobRefAsync(Guid companyId);
}
