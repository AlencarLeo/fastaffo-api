using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
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
    public async Task<ActionResult<List<UserAdmin>>> GetUserAdmins()
    {
        var userAdmins = await _context.Admins.ToListAsync();

        return Ok(userAdmins);
    }

    [HttpPost]
    [Route("admin")]
    public async Task<ActionResult> CreateUserAdmin(UserAdminDtoReq request)
    {
        var userAdmin = new UserAdmin();

        userAdmin.FirstName = request.FirstName;
        userAdmin.LastName = request.LastName;
        userAdmin.Email = request.Email;
        userAdmin.Phone = request.Phone;
        userAdmin.Password = request.Password;
        userAdmin.IsOwner = request.IsOwner;
        userAdmin.CompanyId = null;

        await _context.AddAsync(userAdmin);
        await _context.SaveChangesAsync();

        return Ok();
    }
}