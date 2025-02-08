using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;
public interface IJobService
{
    Task<ServiceResponseDto<JobDtoRes>> GetJobById(Guid id);
    Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetOpenedJobs(int page = 1, int pageSize = 10);
    Task<ServiceResponseDto<List<JobDtoRes>>> GetCompanyJobsByDateRange(DateTime jobLocalStartDate, DateTime jobLocalEndDate);
    Task<ServiceResponseDto<MonthJobsDtoRes>> GetDaysOfJobsByMonth(int month, int year);
    Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetPastJobs(int page = 1, int pageSize = 5);
    Task<ServiceResponseDto<PaginatedDto<JobDtoRes>>> GetNextJobs(int page = 1, int pageSize = 5);
    Task<ServiceResponseDto> CreateJob(JobDtoReq request);
    Task<ServiceResponseDto> UpdateJob(Guid jobId, JobDtoReq request);
}