namespace fastaffo_api.src.Application.DTOs;
public class JobDtoReq
{
    public required string Title { get; set; }
    public required Guid CompanyId { get; set; }
    public required float BaseRate { get; set; }
    public required DateTime StartDateTime { get; set; }
    public required string Location { get; set; }
    public required int MaxStaffNumber { get; set; }
    public required bool AcceptingReqs { get; set; }
    public List<Guid>? AllowedForJobStaffIds { get; set; } 
}