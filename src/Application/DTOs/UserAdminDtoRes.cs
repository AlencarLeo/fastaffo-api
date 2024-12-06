
namespace fastaffo_api.src.Application.DTOs;
public class UserAdminDtoRes
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required bool IsOwner { get; set; }
    public required string Role { get; set; }
}
