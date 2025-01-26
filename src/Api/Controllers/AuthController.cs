using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthservice _authService;

    public AuthController(IAuthservice authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [Route("me"), Authorize]
    public ActionResult<string> GetMe()
    {
        var (id, roles) = _authService.GetCurrentUser();

        if (id == null)
        {
            return Unauthorized("User not found.");
        }

        return Ok(new { id, roles });
    }


    [HttpPost]
    [Route("register/admin")]
    public async Task<ActionResult> RegisterUserAdmin(UserAdminDtoReq request)
    {
        try
        {
            await _authService.RegisterUserAdminAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("register/staff")]
    public async Task<ActionResult> RegisterUserStaff(UserStaffDtoReq request)
    {
        try
        {
            await _authService.RegisterUserStaffAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("singin/staff")]
    public async Task<ActionResult<TokenUserDto<UserStaffDtoRes>>> SinginStaff(AuthDtoReq request)
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

    [HttpPost("singin/admin")]
    public async Task<ActionResult<TokenUserDto<UserAdminDtoRes>>> SinginAdmin(AuthDtoReq request)
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
}