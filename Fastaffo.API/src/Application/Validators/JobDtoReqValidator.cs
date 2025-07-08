using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;

using FluentValidation;

namespace fastaffo_api.src.Application.Validators;

public class JobDtoReqValidator : AbstractValidator<JobDtoReq>
{
    public JobDtoReqValidator(IValidatorService validatorService)
    {
        RuleFor(j => j.EventName)
            .NotEmpty().WithMessage("Event name is required.")
            .MinimumLength(3).WithMessage("Event name must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Event name must be at most 50 characters long.");

        RuleFor(j => j.ChargedAmount)
            .GreaterThan(0).WithMessage("Charged amount must be greater than 0.");

        RuleFor(j => j.ClientName)
            .NotEmpty().WithMessage("Client name is required.")
            .MinimumLength(3).WithMessage("Client name must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Client name must be at most 50 characters long.");

        RuleFor(j => j.Location)
            .MaximumLength(200).WithMessage("Location must be less than 200 characters.")
            .When(j => !string.IsNullOrWhiteSpace(j.Location));

        RuleFor(j => j.Notes)
            .MaximumLength(500).WithMessage("Notes must be less than 500 characters.")
            .When(j => !string.IsNullOrWhiteSpace(j.Notes));

        RuleFor(j => j.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.")
            .MustAsync(async (id, ct) => await validatorService.ExistsAsync<Company>(id, ct)).WithMessage("CompanyId must refer to an existing Company.");

        RuleFor(j => j.CreatedByAdminId)
            .NotEmpty().WithMessage("CreatedByAdminId is required.")
            .MustAsync(async (id, ct) => await validatorService.ExistsAsync<Admin>(id, ct)).WithMessage("CreatedByAdminId must refer to an existing Admin.");
    }
}
