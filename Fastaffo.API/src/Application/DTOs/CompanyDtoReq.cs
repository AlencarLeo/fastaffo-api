namespace fastaffo_api.src.Application.DTOs;

public class CompanyDtoReq
{
    public required string Name { get; set; }
    public required string ABN { get; set; }
    public string? WebsiteUrl { get; set; }
    public ContactInfoDto? ContactInfo { get; set; }
}
