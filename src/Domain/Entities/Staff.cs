namespace fastaffo_api.src.Domain.Entities;
public class Staff
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public required Guid ContactInfoId { get; set; }
    public ContactInfo ContactInfo { get; set; } = null!;
}
