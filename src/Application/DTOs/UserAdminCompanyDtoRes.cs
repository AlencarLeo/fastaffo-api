
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.DTOs;
public class UserAdminCompanyDtoRes
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required bool IsOwner { get; set; }
    public required string Role { get; set; }
    public Guid? CompanyId { get; set; }
    public CompanyDtoRes? Company { get; set; }
}
