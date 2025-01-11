namespace fastaffo_api.src.Domain.Entities;
public class Job
{
    public Guid Id { get; set; }
    public int JobNumber { get; set; }
    public string Client { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public float? TotalChargedValue { get; set; }
    public float? JobDuration { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public float BaseRate { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? FinishDateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsClosed { get; set; } = false;
    // public string WhyClosed { get; set; } =  string.Empty; // FUTURAMENTE
    public int MaxStaffNumber { get; set; }
    public int CurrentStaffCount { get; set; } = 0;    
    public bool AcceptingReqs { get; set; }
    public List<Guid>? AllowedForJobStaffIds { get; set; } // SE FOR NULL TODOS PODEM APLICAR
    public List<JobRequest>? JobRequests { get; set; } = null;
    public List<JobStaff>? JobStaffs { get; set; } = null;
}