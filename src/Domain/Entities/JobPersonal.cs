namespace fastaffo_api.src.Domain.Entities;

public class JobPersonal
{
    public Guid Id { get; set; }
    public Guid CreatedByLabourId { get; set; }

    public string CompanyName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset? UtcStartDateTime { get; set; }
    public DateTimeOffset? LocalStartDateTime { get; set; }
    public float BaseRate { get; set; }

    public string? Notes { get; set; } // opcional para organização pessoal
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    




    public bool IsClosed { get; set; } = false;

}
