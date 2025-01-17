
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Services;
using Microsoft.AspNetCore.Mvc;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly DataContext _context;
    private readonly TokenService _tokenService;
     
    public AuthController(TokenService tokenService, DataContext context)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Route("me"), Authorize]
    public ActionResult<string> GetMe()
    {
        var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roleClaims = User?.FindAll(ClaimTypes.Role);
        var roles = roleClaims?.Select(c => c.Value).ToList();
        return Ok(new { id, roles });
    }


    [HttpPost]
    [Route("register/admin")]
    public async Task<ActionResult> RegisterUserAdmin(UserAdminDtoReq request)
    {
        UserAdmin? userAdmin = await _context.Admins.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userAdmin is not null){
            return Conflict("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        userAdmin = new UserAdmin
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Password = passwordHash,
            IsOwner = request.IsOwner,
            CompanyId = null
        };

        await _context.AddAsync(userAdmin);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("register/staff")]
    public async Task<ActionResult> RegisterUserStaff(UserStaffDtoReq request)
    {
        UserStaff? userStaff = await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is not null){
            return Conflict("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        userStaff = new UserStaff
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            Email = request.Email,
            Password = passwordHash
        };

        await _context.Staffs.AddAsync(userStaff);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("singin/staff")]
    public async Task<ActionResult<TokenUserDto<UserStaffDtoRes>>> SinginStaff(AuthDtoReq request)
    {
        UserStaff? userStaff =  await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is null || !BCrypt.Net.BCrypt.Verify(request.Password, userStaff.Password)){
            return BadRequest("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(userStaff.Id, userStaff.Role);

        UserStaffDtoRes userStaffDtoRes = new UserStaffDtoRes{
            Id = userStaff.Id,
            Role = userStaff.Role,    
            FirstName = userStaff.FirstName,
            LastName = userStaff.LastName,
            Phone = userStaff.Phone,
            Email = userStaff.Email
        };

        var result = new TokenUserDto<UserStaffDtoRes>(userStaffDtoRes, token);

        return Ok(result);
    }

    [HttpPost("singin/admin")]
    public async Task<ActionResult<TokenUserDto<UserAdminDtoRes>>> SinginAdmin(AuthDtoReq request)
    {
        UserAdmin? userAdmin =  await _context.Admins.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userAdmin is null || !BCrypt.Net.BCrypt.Verify(request.Password, userAdmin.Password)){
            return BadRequest("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(userAdmin.Id, userAdmin.Role);
        

        UserAdminDtoRes userAdminDtoRes = new UserAdminDtoRes{
            Id = userAdmin.Id,
            FirstName = userAdmin.FirstName,
            LastName = userAdmin.LastName,
            Email = userAdmin.Email,
            Phone = userAdmin.Phone,
            IsOwner = userAdmin.IsOwner,
            Role = userAdmin.Role
        };

        var result = new TokenUserDto<UserAdminDtoRes>(userAdminDtoRes, token);

        return Ok(result);
    }
}