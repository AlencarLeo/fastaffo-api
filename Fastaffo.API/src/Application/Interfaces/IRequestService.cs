using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.Interfaces;

public interface IRequestService
{
    public Task CreateRequestAsync(RequestDtoCreateReq dto, CancellationToken ct = default);
    public Task ApproveRequestAsync(RequestDtoUpdateReq request, CancellationToken ct = default);
    public Task RejectRequestAsync(RequestDtoUpdateReq request, CancellationToken ct = default);
}
