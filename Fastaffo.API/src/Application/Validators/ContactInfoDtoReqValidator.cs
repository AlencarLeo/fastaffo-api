using fastaffo_api.src.Application.DTOs;

using FluentValidation;

namespace fastaffo_api.src.Application.Validators;
public class ContactInfoDtoReqValidator : AbstractValidator<ContactInfoDtoReq>
{
    public ContactInfoDtoReqValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s\-()]{7,20}$").WithMessage("Phone number format is invalid.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.")
            .Matches(@"^\d{4,10}$").WithMessage("Postal code format is invalid.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .MaximumLength(50).WithMessage("State must be at most 50 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must be at most 100 characters.");

        RuleFor(x => x.AddressLine1)
            .NotEmpty().WithMessage("Address Line 1 is required.")
            .MaximumLength(150).WithMessage("Address Line 1 must be at most 150 characters.");

        RuleFor(x => x.AddressLine2)
            .MaximumLength(150).WithMessage("Address Line 2 must be at most 150 characters.")
            .When(x => !string.IsNullOrEmpty(x.AddressLine2));

        RuleFor(x => x.PhotoLogoUrl)
            .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Photo Logo URL must be a valid URL.");
    }
}
