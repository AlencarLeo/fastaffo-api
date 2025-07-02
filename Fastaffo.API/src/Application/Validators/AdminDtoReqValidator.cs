using fastaffo_api.src.Application.DTOs;

using FluentValidation;

namespace fastaffo_api.src.Application.Validators;

public class AdminDtoReqValidator : AbstractValidator<AdminDtoReq>
{
    public AdminDtoReqValidator()
    {
        RuleFor(admin => admin.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be at most 50 characters long.");

        RuleFor(admin => admin.Lastname)
            .NotEmpty().WithMessage("Lastname is required.")
            .MaximumLength(100).WithMessage("Lastname must be at most 50 characters long.");

        RuleFor(admin => admin.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(admin => admin.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]+").WithMessage("Password must contain at least one special character.");

        RuleFor(admin => admin.Role)
            .NotEmpty().WithMessage("Role is required.")
            .IsInEnum().WithMessage("Role must be valid.");

        RuleFor(admin => admin.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.")
            .Must(id => id != Guid.Empty).WithMessage("CompanyId must be a valid GUID.");

        When(admin => admin.ContactInfo != null, () =>
        {
            RuleFor(admin => admin.ContactInfo!)
                .SetValidator(new ContactInfoDtoValidator());
        });
    }
}
