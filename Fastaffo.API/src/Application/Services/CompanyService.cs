using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

namespace fastaffo_api.src.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly DataContext _context;
    private readonly IValidatorService _validatorService;
    private readonly IValidator<CompanyDtoReq> _companyDtoReqValidator;
    public CompanyService(DataContext context, IValidatorService validatorService, IValidator<CompanyDtoReq> companyDtoReqValidator)
    {
        _context = context;
        _validatorService = validatorService;
        _companyDtoReqValidator = companyDtoReqValidator;
    }

    public async Task CreateCompanyAsync(CompanyDtoReq request)
    {
        await _validatorService.ValidateAsync(_companyDtoReqValidator, request);

        var newCompany = new Company
        {
            Name = request.Name,
            ABN = request.ABN,
            WebsiteUrl = request.WebsiteUrl,
            ContactInfo = ContactInfoMapper.ToEntity(request.ContactInfo)
        };

        await _context.AddAsync(newCompany);
        await _context.SaveChangesAsync();
    }

}
