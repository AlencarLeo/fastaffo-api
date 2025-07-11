namespace fastaffo_api.src.Domain.Entities;

public class ContactInfo
{
    public Guid Id { get; set; }
    public string? PhotoLogoUrl { get; set; }
    public required string PhoneNumber { get; set; }
    public required string PostalCode { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
}
