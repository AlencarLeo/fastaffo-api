namespace fastaffo_api.src.Domain.Entities;

public class RatePolicy
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int overtime_start_minutes { get; set; }
    public decimal overtime_multiplier { get; set; }
    public decimal day_multiplier { get; set; }
    public int travel_time_rate { get; set; }
    public int kilometers_rate { get; set; }
    public int allowances_rate { get; set; }
    public int extras_rate { get; set; }
}
