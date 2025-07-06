using fastaffo_api.src.Domain.Enums;
namespace fastaffo_api.src.Application.DTOs;

public class AdminDtoReq
{
    public required string Name { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required SystemRole Role { get; set; }
    public required Guid CompanyId { get; set; }
    public ContactInfoDtoReq? ContactInfo { get; set; }
}
