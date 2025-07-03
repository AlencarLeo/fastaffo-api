using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.DTOs;

public class JobDtoRes
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
    public CompanyDtoRes? Company { get; set; }
    public required Guid CreatedByAdminId { get; set; }
    public AdminDtoRes? CreatedBy { get; set; }
}
