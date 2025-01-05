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

    public bool IsClosed { get; set; } = false;
    // public string WhyClosed { get; set; } =  string.Empty; // FUTURAMENTE

    public int MaxStaffNumber { get; set; }
    public int CurrentStaffCount { get; set; } = 0;

    public List<Guid>? JobRequestsId { get; set; } = null;
    public List<JobRequest>? JobRequests { get; set; } = null;
    
    public List<Guid>? JobStaffsId { get; set; } = null;
    public List<JobStaff>? JobStaffs { get; set; } = null;
}