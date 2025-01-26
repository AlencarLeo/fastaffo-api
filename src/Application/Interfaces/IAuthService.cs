using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;
public interface IAuthservice
{
    (string? Id, List<string>? Roles) GetCurrentUser();
    Task RegisterUserAdminAsync(UserAdminDtoReq request);
    Task RegisterUserStaffAsync(UserStaffDtoReq request);
    Task<TokenUserDto<UserAdminDtoRes>> AuthenticateAdminAsync(AuthDtoReq request);
    Task<TokenUserDto<UserStaffDtoRes>> AuthenticateStaffAsync(AuthDtoReq request);
}
