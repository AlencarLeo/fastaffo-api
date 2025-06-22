namespace fastaffo_api.src.Domain.Entities;
public class StaffJobAllowance
{
    public Guid Id { get; set; }
    public Guid StaffJobId { get; set; }
    public required Guid ExtraRateAmountEntryId { get; set; }
    public string Label { get; set; } = null!;
    public required int Amount { get; set; }
}