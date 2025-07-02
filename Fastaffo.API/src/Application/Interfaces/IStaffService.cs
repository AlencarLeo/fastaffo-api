using fastaffo_api.src.Application.DTOs;
namespace fastaffo_api.src.Application.Interfaces;
public interface IStaffService
{
    Task<ServiceResponseDto<StaffDtoRes>> GetStaffByIdAsync(Guid staffId);
}
