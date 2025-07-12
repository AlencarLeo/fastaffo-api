
using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.DTOs;

public class RequestDtoUpdateReq
{
    public Guid Id { get; set; }
    public Guid? AdminId { get; set; }
    public required Guid ResponsedById { get; set; }
    public required RequestStatus Status { get; set; }
}

