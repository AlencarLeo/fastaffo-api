namespace fastaffo_api.src.Application.DTOs;
public class JobDtoReq
{
    public required string Title { get; set; }
    public string Client { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public float? TotalChargedValue { get; set; }
    public required float BaseRate { get; set; }
    public required string StartDateTimeZoneLocation { get; set; }
    public required DateTime LocalStartDateTime { get; set; }
    public required string Location { get; set; }
    public required int MaxStaffNumber { get; set; }
    public required bool AcceptingReqs { get; set; }
    public List<Guid>? AllowedForJobStaffIds { get; set; } 
}