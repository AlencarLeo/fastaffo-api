using fastaffo_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
class JobsConflictController : ControllerBase
{
    private readonly DataContext _context;
    public JobsConflictController(DataContext context)
    {
        _context = context;
    }

}