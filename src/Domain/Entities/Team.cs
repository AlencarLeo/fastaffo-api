namespace fastaffo_api.src.Domain.Entities;

public class Team
{
    public Guid Id { get; set; }
    public required Guid JobId { get; set; }
    public Job? Job { get; set; }
    public required string Name { get; set; }
    public Guid? SupervisorStaffId { get; set; }
    public Staff? SupervisorStaff { get; set; }
    public Guid? SupervisorAdminId { get; set; }
    public Admin? SupervisorAdmin { get; set; }
}