using fastaffo_api.src.Application.DTOs;

using FluentValidation;

namespace fastaffo_api.src.Application.Validators;

public class CompanyDtoReqValidator : AbstractValidator<CompanyDtoReq>
{
    public CompanyDtoReqValidator()
    {
        RuleFor(company => company.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be at most 50 characters long.");

        RuleFor(company => company.ABN)
            .NotEmpty().WithMessage("ABN is required.")
            .Length(11).WithMessage("ABN must be exactly 11 digits.")
            .Matches(@"^\d{11}$").WithMessage("ABN must contain only digits.");
        // https://abr.business.gov.au/

        RuleFor(company => company.WebsiteUrl)
            .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Website URL must be a valid URL.");

        When(admin => admin.ContactInfo != null, () =>
        {
            RuleFor(admin => admin.ContactInfo!)
                .SetValidator(new ContactInfoDtoReqValidator());
        });
    }
}
