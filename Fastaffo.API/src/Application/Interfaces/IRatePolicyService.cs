using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;

public interface IRatePolicyService
{
    public Task CreateRatePolicyAsync(RatePolicyDtoReq request);
}
