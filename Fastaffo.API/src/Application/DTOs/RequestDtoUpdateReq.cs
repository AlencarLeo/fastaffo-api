
using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.DTOs;

public class RequestDtoUpdateReq
{
    public required Guid AdminId { get; set; }
    public required Guid ResponsedById { get; set; }
    public required RequestStatus Status { get; set; }
    public required DateTime ResponseDate { get; set; }
}

