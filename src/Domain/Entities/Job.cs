namespace fastaffo_api.src.Domain.Entities;
public class Job
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid Company { get; set; }
    public float BaseRate { get; set; }
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public List<Guid>? Staffs { get; set; }
}