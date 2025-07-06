namespace fastaffo_api.src.Application.DTOs;
public class CompanyDtoRes
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ABN { get; set; }
    public string? WebsiteUrl { get; set; }
    public ContactInfoDtoRes? ContactInfo { get; set; }
}
