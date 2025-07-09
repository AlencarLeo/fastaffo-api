
namespace fastaffo_api.src.Application.DTOs;

public class RatePolicyDtoReq
{
    public required Guid CompanyId { get; set; }
    public int? OvertimeStartMinutes { get; set; }
    public decimal? OvertimeMultiplier { get; set; }
    public decimal? DayMultiplier { get; set; }
    public int? TravelTimeRate { get; set; }
    public int? KilometersRate { get; set; }
}

