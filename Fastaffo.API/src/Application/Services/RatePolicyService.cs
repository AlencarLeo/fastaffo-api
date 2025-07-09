using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
using fastaffo_api.src.Infrastructure.Data;

namespace fastaffo_api.src.Application.Services;

public class RatePolicyService : IRatePolicyService
{
    private readonly DataContext _context;
    public RatePolicyService(DataContext context)
    {
        _context = context;
    }

    public async Task CreateRatePolicyAsync(RatePolicyDtoReq request)
    {
        var newRatePolicy = RatePolicyMapper.ToEntity(request);

        await _context.AddAsync(newRatePolicy);
        await _context.SaveChangesAsync();
    }

}
