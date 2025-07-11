using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;

public interface IRequestService
{
    public Task CreateRequestAsync(RequestDtoCreateReq dto, CancellationToken ct = default);
    // public Task UpdateStatus(RequestDtoCreateReq dto, CancellationToken ct = default);
}
