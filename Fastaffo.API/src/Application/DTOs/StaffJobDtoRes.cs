namespace fastaffo_api.src.Application.DTOs;

public class StaffJobDtoRes
{
    public Guid Id { get; set; }
    public required Guid StaffId { get; set; }
    public StaffDtoRes? Staff { get; set; }
    public Guid? JobId { get; set; }
    public JobDtoRes? Job { get; set; }
    public Guid? TeamId { get; set; }
    public TeamDtoRes? Team { get; set; }
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