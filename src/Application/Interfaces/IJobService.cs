

using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;
public interface IJobService
{
    // Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetOpenedJobs(int page = 1, int pageSize = 10);
    Task<ServiceResponseDto> CreateJob(JobDtoReq request);
}