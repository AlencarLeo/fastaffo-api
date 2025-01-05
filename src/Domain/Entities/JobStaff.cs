namespace fastaffo_api.src.Domain.Entities;
public class JobStaff
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public Guid StaffId { get; set; }
    public string JobRole { get; set; }
    public float AddRate { get; set; }
    public DateTime AddStartDateTime { get; set; }
}