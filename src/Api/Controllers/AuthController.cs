using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [Route("me"), Authorize]
    public ActionResult<string> GetMe()
    {
        var (id, roles) = _authService.GetAuthenticatedUser();

        if (id == null)
        {
            return Unauthorized("User not found.");
        }

        return Ok(new { id, roles });
    }

    [HttpPost]
    [Route("register/admin")]
    public async Task<ActionResult> RegisterAdmin(AdminDtoReq request)
    {
        try
        {
            await _authService.RegisterAdminAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("singin/admin")]
    public async Task<ActionResult<TokenUserDto<AdminDtoRes>>> SinginAdmin(AuthDtoReq request)
    {
        try
        {
            var result = await _authService.AuthenticateAdminAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register/staff")]
    public async Task<ActionResult> RegisterUserStaff(Staff request)
    {
        try
        {
            await _authService.RegisterStaffAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("singin/staff")]
    public async Task<ActionResult<TokenUserDto<Staff>>> SinginStaff(AuthDtoReq request)
    {
        try
        {
            var result = await _authService.AuthenticateStaffAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}