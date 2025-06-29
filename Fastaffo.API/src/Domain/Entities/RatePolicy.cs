namespace fastaffo_api.src.Domain.Entities;

public class RatePolicy
{
    public Guid Id { get; set; }
    public required Guid CompanyId { get; set; }
    public int? OvertimeStartMinutes { get; set; }
    public decimal? OvertimeMultiplier { get; set; }
    public decimal? DayMultiplier { get; set; }
    public int? TravelTimeRate { get; set; }
    public int? KilometersRate { get; set; }
}
