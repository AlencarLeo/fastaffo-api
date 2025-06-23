namespace fastaffo_api.src.Application.DTOs;
public class StaffDtoReq
{
    public required string Name { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public ContactInfoDto? ContactInfo { get; set; }
}
