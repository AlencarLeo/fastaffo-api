using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Domain.Entities;
public class JobRequest
{
    public Guid Id { get; set; }
    
    public Guid JobId { get; set; }
    public Job Job { get; set; }

    public Guid StaffId { get; set; }
    public UserStaff Staff { get; set; }

    public RequestStatus Status { get; set; }
    public RequestType Type { get; set; }
    public DateTime RequestedAt { get; set; }
}