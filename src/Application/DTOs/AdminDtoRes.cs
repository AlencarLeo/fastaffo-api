using fastaffo_api.src.Domain.Enums;
namespace fastaffo_api.src.Application.DTOs;

public class AdminDtoRes
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public SystemRole Role { get; set; }
    public required Guid CompanyId { get; set; }
    public ContactInfoDto? ContactInfo { get; set; }
}
