using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Interfaces;
public interface IAuthService
{
    (string? Id, List<string>? Roles) GetCurrentUser();
    Task RegisterUserAdminAsync(Admin request);
    Task RegisterUserStaffAsync(Staff request);
    Task<TokenUserDto<Admin>> AuthenticateAdminAsync(AuthDtoReq request);
    Task<TokenUserDto<Staff>> AuthenticateStaffAsync(AuthDtoReq request);
}
