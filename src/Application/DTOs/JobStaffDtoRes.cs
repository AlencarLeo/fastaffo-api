namespace fastaffo_api.src.Application.DTOs;
public class JobStaffDtoRes
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public Guid StaffId { get; set; }
    public UserStaffDtoRes Staff { get; set; }
    public string JobRole { get; set; }
    public float AddRate { get; set; }
    public DateTime AddStartDateTime { get; set; }
}