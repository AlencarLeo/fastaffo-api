using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.DTOs;
public class JobDtoRes
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required Guid CompanyId { get; set; }
    public required string CompanyName { get; set; }
    public required float BaseRate { get; set; }
    public required DateTime StartDateTime { get; set; }
    public required string Location { get; set; }
    public required bool IsClosed { get; set; }
    public required int MaxStaffNumber { get; set; }
    public required int CurrentStaffCount { get; set; }
    public required bool AcceptingReqs { get; set; }
    public List<Guid>? AllowedForJobStaffIds { get; set; } 
    public List<JobRequest>? JobRequests { get; set; } 
    public List<JobStaff>? JobStaffs { get; set; }
}
