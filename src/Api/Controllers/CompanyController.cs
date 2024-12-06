
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Services;
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
