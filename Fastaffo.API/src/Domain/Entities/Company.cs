namespace fastaffo_api.src.Domain.Entities;
public class Company
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ABN { get; set; }
    public string? WebsiteUrl { get; set; }
    public Guid? ContactInfoId { get; set; }
    public ContactInfo? ContactInfo { get; set; }
}
