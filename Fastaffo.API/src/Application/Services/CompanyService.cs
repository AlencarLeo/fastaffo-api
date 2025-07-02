using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using FluentValidation;

namespace fastaffo_api.src.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly DataContext _context;
    private readonly IValidator<CompanyDtoReq> _companyDtoReqValidator;
    public CompanyService(DataContext context, IValidator<CompanyDtoReq> companyDtoReqValidator)
    {
        _context = context;
        _companyDtoReqValidator = companyDtoReqValidator;
    }

    public async Task CreateCompany(CompanyDtoReq request)
    {
        var validationResult = await _companyDtoReqValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new Exception($"Validation failed: {errors}");
        }

        var newCompany = new Company
        {
            Name = request.Name,
            ABN = request.ABN,
            WebsiteUrl = request.WebsiteUrl,
            ContactInfo  = request.ContactInfo == null ? null : new ContactInfo
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