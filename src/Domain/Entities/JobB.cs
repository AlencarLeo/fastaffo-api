// Se tornaram o Job base -> Tenho que fazer start e Finish time
// Job admin tem o JobCount, como funcionara para o own job??

public abstract class JobB
{
    public string Title { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public float BaseRate { get; set; }
    public DateTimeOffset? UtcStartDateTime { get; set; }
    public DateTimeOffset? LocalStartDateTime { get; set; }
}
