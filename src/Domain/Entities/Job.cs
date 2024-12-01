namespace fastaffo_api.src.Domain.Entities;
public class Job
{
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public float BaseRate { get; set; }
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = string.Empty;
}