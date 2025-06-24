using fastaffo_api.src.Domain.Enums;
namespace fastaffo_api.src.Domain.Entities;

public class Admin
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required SystemRole Role { get; set; }
    public required Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public Guid? ContactInfoId { get; set; }
    public ContactInfo? ContactInfo { get; set; }
}
