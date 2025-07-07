using fastaffo_api.src.Domain.Enums;
namespace fastaffo_api.src.Application.DTOs;
public class JobDtoReq
{
    public required string EventName { get; set; }
    public required int ChargedAmount { get; set; }
    public required string ClientName { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public JobStatus Status { get; set; } = JobStatus.Planning;
    public required Guid CompanyId { get; set; }
    public required Guid CreatedByAdminId { get; set; }
}
