using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.DTOs;
public class JobRequestDtoRes
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public Guid StaffId { get; set; }
    public UserStaffDtoRes Staff { get; set; }
    public Guid? AdminId { get; set; }
    public UserAdminDtoRes? Admin { get; set; }
    public RequestStatus Status { get; set; }
    public RequestType Type { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? ResponsedAt { get; set; }

}