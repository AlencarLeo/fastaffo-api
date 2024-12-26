
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
                Id = c.Id,
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
        var userAdmin = await _context.Admins.FindAsync(id);

        if(userAdmin is null){
            return NotFound("User does not exist.");
        }

        if(!userAdmin.IsOwner && userAdmin.Role != "admin"){
            return BadRequest("To create a company, you need to be a owner admin.");
        }

        var company = new Company{
            Name = request.Name,
            ABN = request.ABN
        };
        
        await _context.AddAsync(company);
        await _context.SaveChangesAsync();
        
        userAdmin.CompanyId = company.Id;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch]
    [Route("company/{companyId}/useradmin/{useradminId}/add/{newUseradminId}")]
    public async Task<ActionResult<CompanyDtoRes>> AddUserAdminOnCompany(Guid companyId, Guid useradminId, Guid newUseradminId)
    {
        var company = await _context.Companies.FindAsync(companyId);
        var userAdmin = await _context.Admins.FindAsync(useradminId);
        var newUserAdmin = await _context.Admins.FindAsync(newUseradminId);

        if(company is null){
            return NotFound("Company does not exist.");
        }
        if(userAdmin is null || newUserAdmin is null){
            return BadRequest("User does not exist.");
        }

        if(userAdmin.Role is not "admin"){
            return BadRequest("You are not allowed to add people on this company.");
        }
        if(newUserAdmin.Role is not "admin"){
            return BadRequest("You can only add peopleo with 'ADMIN' roles in your company.");
        }
        if(newUserAdmin.CompanyId is not null){
            return BadRequest("User is already in a company.");
        }

        newUserAdmin.CompanyId = companyId;

        _context.Admins.Update(newUserAdmin);
        await _context.SaveChangesAsync();

        var checkCompany = await _context.Companies
            .Where(c => c.Id == companyId)
            .Select(c => new CompanyDtoRes{
                Id = c.Id,
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
            }).FirstOrDefaultAsync();
        
        return Ok(checkCompany);
    }

    [HttpPatch]
    [Route("company/{companyId}/useradmin/{useradminId}/remove/{oldUseradminId}")]
    public async Task<ActionResult<CompanyDtoRes>> RemoveUserAdminFromCompany(Guid companyId, Guid useradminId, Guid newUseradminId)
    {
        var company = await _context.Companies.FindAsync(companyId);
        var userAdmin = await _context.Admins.FindAsync(useradminId);
        var oldUserAdmin = await _context.Admins.FindAsync(newUseradminId);

        if(company is null){
            return NotFound("Company does not exist.");
        }
        if(userAdmin is null || oldUserAdmin is null){
            return BadRequest("User does not exist.");
        }
        if(userAdmin.Role is not "admin"){
            return BadRequest("You are not allowed to remove people from this company.");
        }
        if(oldUserAdmin.IsOwner && !userAdmin.IsOwner){
            return BadRequest("Only owner can remove another onwer.");
        }

        oldUserAdmin.CompanyId = null;

        _context.Admins.Update(oldUserAdmin);
        await _context.SaveChangesAsync();

        var checkCompany = await _context.Companies
            .Where(c => c.Id == companyId)
            .Select(c => new CompanyDtoRes{
                Id = c.Id,
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
            }).FirstOrDefaultAsync();

        return Ok(checkCompany);
    }


}
