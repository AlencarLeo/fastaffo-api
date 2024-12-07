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
    public async Task<ActionResult<List<UserAdminCompanyDtoRes>>> GetUserAdmins()
    {
        var userAdmins = await _context.Admins
                                        .Select(ua => new UserAdminCompanyDtoRes{
                                            Id = ua.Id,
                                            FirstName = ua.FirstName,
                                            LastName = ua.LastName,
                                            Email = ua.Email,
                                            Phone = ua.Phone,
                                            IsOwner = ua.IsOwner,
                                            Role = ua.Role,
                                            CompanyId = ua.CompanyId,
                                            Company = ua.Company != null ? new CompanyDtoRes
                                            {
                                                Id = ua.Company.Id,
                                                Name = ua.Company.Name,
                                                ABN = ua.Company.ABN
                                            } : null
                                        }).ToListAsync();

        return Ok(userAdmins);
    }
}