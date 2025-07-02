using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Validators;

public class StaffJobDtoReqValidator : AbstractValidator<StaffJobDtoReq>
{
    public StaffJobDtoReqValidator(DataContext context)
    {
        RuleFor(x => x.StaffId)
            .NotEmpty().WithMessage("StaffId is required.")
            .MustAsync(async (staffId, cancellation) => await context.Staffs.AnyAsync(s => s.Id == staffId, cancellation)).WithMessage("StaffId must refer to an existing Staff.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("StartTime is required.")
            .Must(startTime => startTime > DateTime.UtcNow).WithMessage("StartTime must be in the future.");

        RuleFor(x => x.BaseRate)
            .GreaterThanOrEqualTo(0).WithMessage("BaseRate must be zero or positive.");

        RuleFor(x => x.IsPersonalJob)
            .NotNull().WithMessage("IsPersonalJob is required.");

        When(x => x.IsPersonalJob, () =>
        {
            RuleFor(x => x.JobId)
                .Null().WithMessage("JobId must be null for personal jobs.");

            RuleFor(x => x.TeamId)
                .Null().WithMessage("TeamId must be null for personal jobs.");

            RuleFor(x => x.TravelTimeMinutes)
                .Null().WithMessage("TravelTimeMinutes must be null for personal jobs.");

            RuleFor(x => x.Kilometers)
                .Null().WithMessage("Kilometers must be null for personal jobs.");
        });

        When(x => !x.IsPersonalJob, () =>
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("JobId is required for non-personal jobs.");

            RuleFor(x => x.TeamId)
                .NotEmpty().WithMessage("TeamId is required for non-personal jobs.");

            RuleFor(x => x.TravelTimeMinutes)
                .NotNull().WithMessage("TravelTimeMinutes is required for non-personal jobs.");

            RuleFor(x => x.Kilometers)
                .NotNull().WithMessage("Kilometers is required for non-personal jobs.");
        });

        RuleFor(x => x.FinishTime)
            .GreaterThanOrEqualTo(x => x.StartTime)
            .When(x => x.FinishTime.HasValue)
            .WithMessage("FinishTime must be after StartTime.");
    }
}
