namespace fastaffo_api.src.Domain.Entities;

public class StaffJob
{
    public Guid Id { get; set; }
    public required Guid StaffId { get; set; }
    public Staff? Staff { get; set; }
    public Guid? JobId { get; set; }
    public Job? Job { get; set; }
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
    public string? Role { get; set; }
    public required DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }
    public required int BaseRate { get; set; }
    public int? TravelTimeMinutes { get; set; }
    public decimal? Kilometers { get; set; }
    public string? Notes { get; set; }
    public int? TotalAmount { get; set; }
    public string? Title { get; set; }
    public string? Location { get; set; }
    public required bool IsPersonalJob { get; set; }
}
