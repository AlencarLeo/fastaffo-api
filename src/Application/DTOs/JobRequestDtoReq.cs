using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.DTOs;
public class JobRequestDtoReq
{
    public required Guid JobId { get; set; }
    public required Guid StaffId { get; set; }
    public Guid? AdminId { get; set; }
    public required RequestType Type { get; set; }
}