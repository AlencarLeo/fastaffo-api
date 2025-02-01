using fastaffo_api.src.Infrastructure.Data;

namespace fastaffo_api.src.Application.Interfaces;
public class JobService : IJobService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    public JobService(IHttpContextAccessor httpContextAccessor, DataContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    
}