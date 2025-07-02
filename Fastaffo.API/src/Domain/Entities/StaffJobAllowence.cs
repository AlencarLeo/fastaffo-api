namespace fastaffo_api.src.Domain.Entities;
public class StaffJobAllowance
{
    public Guid Id { get; set; }
    public required Guid StaffJobId { get; set; }
    public Guid? ExtraRateAmountEntryId { get; set; }
    public string? Label { get; set; }
    public required int Amount { get; set; }
}
