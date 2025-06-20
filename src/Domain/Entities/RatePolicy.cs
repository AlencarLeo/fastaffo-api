namespace fastaffo_api.src.Domain.Entities;

public class RatePolicy
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int overtime_start_minutes { get; set; }
    public decimal overtime_multiplier { get; set; }
    public decimal weekend_multiplier { get; set; }
    public bool allow_travel_time { get; set; }
    public bool allow_kilometers { get; set; }
    public bool allow_allowances { get; set; }
}
