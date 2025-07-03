namespace fastaffo_api.src.Application.DTOs;

public class TeamDtoRes
{
    public Guid Id { get; set; }
    public required Guid JobId { get; set; }
    public JobDtoRes? Job { get; set; }
    public required string Name { get; set; }
    public Guid? SupervisorStaffId { get; set; }
    public StaffDtoRes? SupervisorStaff { get; set; }
    public Guid? SupervisorAdminId { get; set; }
    public AdminDtoRes? SupervisorAdmin { get; set; }
}
