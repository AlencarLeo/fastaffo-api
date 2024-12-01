
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public static User user = new User();

    private readonly TokenService _tokenService;
    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public ActionResult<User> Register(UserDto request)
    {
        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        user.Username = request.Username;
        user.PasswordHash = passwordHash;

        return Ok(user);
    }

    [HttpPost("login")]
    public ActionResult<User> Login(UserDto request)
    {
        if(user.Username != request.Username)
        {
            return BadRequest("User not found.");
        }

        if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return BadRequest("Wrong password");
        }

        string token = _tokenService.CreateToken(user);

        return Ok(token);
    }
}