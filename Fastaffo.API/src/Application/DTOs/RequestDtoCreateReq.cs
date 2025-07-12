
using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.DTOs;

public class RequestDtoCreateReq
{
    public required RequestType Type { get; set; }
    public required Guid JobId { get; set; }
    public required Guid StaffId { get; set; }
    public Guid? AdminId { get; set; }
    public required Guid CompanyId { get; set; }
    public required Guid SentById { get; set; }
}

