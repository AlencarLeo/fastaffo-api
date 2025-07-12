using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Domain.Enums;

using FluentValidation;

namespace fastaffo_api.src.Application.Validators;

public class RequestDtoCreateReqValidator : AbstractValidator<RequestDtoCreateReq>
{
    public RequestDtoCreateReqValidator(IValidatorService validatorService)
    {
        // Type => not null, must be valid enum
        RuleFor(x => x.Type)
            .NotNull().WithMessage("Type is required.")
            .Must(type => Enum.IsDefined(typeof(RequestType), type))
            .WithMessage("Invalid Type value.");

        // JobId => required, must exist
        RuleFor(x => x.JobId)
            .NotNull().WithMessage("JobId is required.")
            .MustAsync(async (id, ct) => await validatorService.ExistsAsync<Job>(id, ct))
            .WithMessage("JobId must reference a valid job.");

        // StaffId => required, must exist
        RuleFor(x => x.StaffId)
            .NotNull().WithMessage("StaffId is required.")
            .MustAsync(async (id, ct) => await validatorService.ExistsAsync<Staff>(id, ct))
            .WithMessage("StaffId must reference a valid staff.");

        // AdminId => optional, must exist if provided
        RuleFor(x => x.AdminId)
            .MustAsync(async (id, ct) =>
                id == null || await validatorService.ExistsAsync<Admin>(id, ct))
            .WithMessage("AdminId must reference a valid admin if provided.");

        // CompanyId => required, must exist
        RuleFor(x => x.CompanyId)
            .NotNull().WithMessage("CompanyId is required.")
            .MustAsync(async (id, ct) => await validatorService.ExistsAsync<Company>(id, ct))
            .WithMessage("CompanyId must reference a valid company.");

        // SentById => required, must be valid Admin or Staff
        RuleFor(x => x.SentById)
            .NotNull().WithMessage("SentById is required.")
            .MustAsync(async (id, ct) =>
                await validatorService.ExistsAsync<Admin>(id, ct) ||
                await validatorService.ExistsAsync<Staff>(id, ct))
            .WithMessage("SentById must reference a valid admin or staff.");

    }

    // Type => Request or Invite (have enum for this); not null
    // Target => Job or Team (have enum for this); not null
    // JobId => Must be a valid ID, Job Id should be on database; not null
    // TeamId => null or must be a valid Team on database
    // StaffId => not null must be a valid staffid 
    // AdminId => null or must be a valid adminid
    // CompanyId => not null must be a valid companyid 
    // SentById => not null, it will come from req, valid id (can be admin or staff)
    // ResponsedById => null or valid id (can be admin or staff)
    // Statu=> pending as default
    // ResponseDate = null or must be greater than CreatedAt, it can only exist if status is not pending
}
