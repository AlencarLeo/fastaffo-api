using fastaffo_api.src.Domain.Enums;
namespace fastaffo_api.src.Domain.Entities;
public class Job
{
    public Guid Id { get; set; }
    public required string JobRef { get; set; }
    public required string EventName { get; set; }
    public required int ChargedAmount { get; set; }
    public required string ClientName { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public JobStatus Status { get; set; } = JobStatus.Planning;
    public required Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public required Guid CreatedByAdminId { get; set; }
    public Admin? CreatedBy { get; set; }
}

//     public string StartDateTimeZoneLocation { get; set; } = string.Empty;
//     public DateTimeOffset UtcStartDateTime { get; set; }
//     public DateTimeOffset LocalStartDateTime { get; set; }
//     public int MaxStaffNumber { get; set; }
//     public int CurrentStaffCount { get; set; } = 0; 
//     public bool AcceptingReqs { get; set; }
//     public List<Guid>? AllowedForJobStaffIds { get; set; }
