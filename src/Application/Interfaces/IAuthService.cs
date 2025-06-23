using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Interfaces;
public interface IAuthService
{
    (string? Id, List<string>? Roles) GetAuthenticatedUser();
    Task RegisterAdminAsync(AdminDtoReq request);
    Task<TokenUserDto<AdminDtoRes>> AuthenticateAdminAsync(AuthDtoReq request);
    Task RegisterStaffAsync(StaffDtoReq request);
    Task<TokenUserDto<StaffDtoRes>> AuthenticateStaffAsync(AuthDtoReq request);
}
