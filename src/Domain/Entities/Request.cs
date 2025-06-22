using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Domain.Entities;

public class Request
{
    public Guid Id { get; set; }

    public required RequestType Type { get; set; }
    public required RequestTarget Target { get; set; }

    public required Guid JobId { get; set; }
    public Job Job { get; set; } = null!;

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }

    public Guid? SupervisorStaffId { get; set; }
    public Staff? SupervisorStaff { get; set; }

    public Guid? SupervisorAdminId { get; set; }
    public Admin? SupervisorAdmin { get; set; }

    public required Guid SentById { get; set; }
    public required SystemRole SentByType { get; set; }

    public required RequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // REVER ISSO TIMEZONE
}
