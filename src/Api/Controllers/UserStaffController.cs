using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class UserStaffController : ControllerBase
{
    private readonly DataContext _context;
    public UserStaffController(DataContext context)
    {
        _context = context;
    }


    [HttpGet]
    [Route("staff")]
    public async Task<ActionResult<List<UserStaff>>> GetUserStaffs()
    {
        var userStaffs = await _context.Staffs.ToListAsync();

        return Ok(userStaffs);
    }
}