namespace fastaffo_api.src.Domain.Entities;

public class StaffTeam
{
    public Guid Id { get; set; }

    public required Guid StaffId { get; set; }
    public Staff? Staff { get; set; }

    public required Guid TeamId { get; set; }
    public Team? Team { get; set; }

    // // Dados para histórico
    // public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    // public DateTime? LeftAt { get; set; }
}
