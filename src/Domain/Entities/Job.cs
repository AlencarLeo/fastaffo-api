namespace fastaffo_api.src.Domain.Entities;
public class Job
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public float BaseRate { get; set; }
    public DateTime StartDateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public List<Guid>? StaffsId { get; set; }
}