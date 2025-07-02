using fastaffo_api.src.Application.DTOs;

using FluentValidation;

namespace fastaffo_api.src.Application.Validators;

public class StaffDtoReqValidator : AbstractValidator<StaffDtoReq>
{
    public StaffDtoReqValidator()
    {
        RuleFor(staff => staff.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be at most 50 characters long.");

        RuleFor(staff => staff.Lastname)
            .NotEmpty().WithMessage("Lastname is required.")
            .MaximumLength(100).WithMessage("Lastname must be at most 50 characters long.");

        RuleFor(staff => staff.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(staff => staff.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]+").WithMessage("Password must contain at least one special character.");

        When(staff => staff.ContactInfo != null, () =>
        {
            RuleFor(staff => staff.ContactInfo!)
                .SetValidator(new ContactInfoDtoValidator());
        });
    }
}
