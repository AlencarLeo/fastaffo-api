using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;
public interface IJobStaffService
{
    Task<ServiceResponseDto> RemoveJobStaff(Guid jobStaffId);
}