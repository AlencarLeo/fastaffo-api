using fastaffo_api.src.Application.DTOs;
namespace fastaffo_api.src.Application.Interfaces;
public interface IStaffJobService
{
    Task CreateStaffJobAsync(StaffJobDtoReq request);
}
