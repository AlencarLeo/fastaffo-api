using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Domain.Entities;

public class Request
{
    public Guid Id { get; set; }
    public required RequestType Type { get; set; }
    public required Guid JobId { get; set; }
    public Job? Job { get; set; }
    public required Guid StaffId { get; set; }
    public Staff? Staff { get; set; }
    public Guid? AdminId { get; set; }
    public Admin? Admin { get; set; }
    public required Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public required Guid SentById { get; set; }
    public Guid? ResponsedById { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // CHECK TIMEZONE LATER ON
    public DateTime? ResponseDate { get; set; }
}
