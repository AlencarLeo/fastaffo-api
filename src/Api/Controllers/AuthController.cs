
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Services;
using Microsoft.AspNetCore.Mvc;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    [HttpPost("register/staff")]
    public async Task<ActionResult> Register(UserStaffDto request)
    {
        UserStaff? userStaff = await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is not null){
            return Conflict("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        userStaff.FirstName = request.FirstName;
        userStaff.LastName = request.LastName;
        userStaff.Phone = request.Phone;
        userStaff.Email = request.Email;
        userStaff.Password = passwordHash;

        _context.Staffs.Add(userStaff);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(AuthDtoReq request)
    {
        // UserStaff? userStaff =  await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);
        UserStaff? userStaff =  await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is null || !BCrypt.Net.BCrypt.Verify(request.Password, userStaff.Password)){
            return BadRequest("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(userStaff, userStaff.Role);

        return Ok(token);
    }
}