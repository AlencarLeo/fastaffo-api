namespace fastaffo_api.src.Domain.Entities;
public class ExtraRateAmountEntry
{
    public Guid Id { get; set; }
    public Guid? CompanyId { get; set; }
    public required string Label { get; set; }
    public string? description { get; set; }
}