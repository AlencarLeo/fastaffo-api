namespace fastaffo_api.src.Application.DTOs;
public class StaffDtoRes
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public ContactInfoDtoRes? ContactInfo { get; set; }
}
