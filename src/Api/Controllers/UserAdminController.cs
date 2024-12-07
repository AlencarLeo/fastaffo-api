using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class UserAdminController : ControllerBase
{
    private readonly DataContext _context;
    public UserAdminController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("admin")]
    public async Task<ActionResult<List<UserAdminDtoRes>>> GetUserAdmins()
    {
        var userAdmins = await _context.Admins
                                        .Include(ua => ua.Company)
                                        .ToListAsync();

        return Ok(userAdmins);
    }
}