
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly DataContext _context;
    public CompanyController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("companies")]
    public async Task<ActionResult> GetAllCompanies()
    {
        var companies = await _context.Companies
            .AsNoTracking()
            .Select(c => new CompanyDtoRes
            {
                Name = c.Name,
                ABN = c.ABN,
                OwnersAndAdmins = c.OwnersAndAdmins != null ? 
                c.OwnersAndAdmins.Select(oa => new UserAdminDtoRes
                {
                    Id = oa.Id,
                    FirstName = oa.FirstName,
                    LastName = oa.LastName,
                    Email = oa.Email,
                    Phone = oa.Phone,
                    IsOwner = oa.IsOwner,
                    Role = oa.Role
                }).ToList() : new List<UserAdminDtoRes>()
            })
            .ToListAsync();

        return Ok(companies);
    }

    [HttpPost]
    [Route("company/useradmin/{id}")]
    public async Task<ActionResult> CreateCompany(Guid id, CompanyDtoReq request)
    {
        var company = new Company();
        var userAdmin = await _context.Admins.FindAsync(id);

        if(userAdmin is null){
            return NotFound("User does not exist.");
        }

        company.Name = request.Name;
        company.ABN = request.ABN;
        
        await _context.AddAsync(company);
        await _context.SaveChangesAsync();
        
        userAdmin.CompanyId = company.Id;
        await _context.SaveChangesAsync();

        return Ok();
    }

}
