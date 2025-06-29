namespace fastaffo_api.src.Domain.Entities;
public class Staff
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Guid? ContactInfoId { get; set; }
    public ContactInfo? ContactInfo { get; set; }
}
