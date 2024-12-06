
namespace fastaffo_api.src.Application.DTOs;
public class UserAdminDtoReq
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Password { get; set; }
    public required bool IsOwner { get; set; }
}
