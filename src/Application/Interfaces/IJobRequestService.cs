using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.Interfaces;
public interface IJobRequestService
{
    Task<ServiceResponseDto> CreateJobRequestAsync(RequestType requestType, Guid jobId, Guid staffId);
    Task<bool> TryAutoAcceptRequestAsync(RequestType requestType, Job job, Guid staffId, RequestStatus jobRequestStatus);
} 