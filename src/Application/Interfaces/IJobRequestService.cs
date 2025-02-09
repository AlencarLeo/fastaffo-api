using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.Interfaces;
public interface IJobRequestService
{
    Task<ServiceResponseDto> CreateJobRequestAsync(RequestType requestType, Guid jobId, Guid? adminId, Guid staffId);
    Task<ServiceResponseDto> DeclineJobRequest(Guid jobRequestId, Guid adminId, Guid staffId);
    Task<ServiceResponseDto> ApproveJobRequest(Guid jobRequestId, Guid jobId, Guid adminId, Guid staffId);
} 