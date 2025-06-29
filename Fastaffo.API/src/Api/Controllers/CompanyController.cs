using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

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
    [Route("company")]
    public async Task CreateCompany(CompanyDtoReq request)
    {
        var newCompany = new Company
        {
            Name = request.Name,
            ABN = request.ABN,
            WebsiteUrl = request.WebsiteUrl,
            ContactInfo  = new ContactInfo
            {
                PhoneNumber = request.ContactInfo.PhoneNumber,
                PhotoLogoUrl = request.ContactInfo.PhotoLogoUrl,
                PostalCode = request.ContactInfo.PostalCode,
                State = request.ContactInfo.State,
                City = request.ContactInfo.City,
                AddressLine1 = request.ContactInfo.AddressLine1,
                AddressLine2 = request.ContactInfo.AddressLine2
            }
        };
        

        await _context.AddAsync(newCompany);
        await _context.SaveChangesAsync();
    }
    
}