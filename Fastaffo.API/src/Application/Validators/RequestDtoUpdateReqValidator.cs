using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;

using FluentValidation;

namespace fastaffo_api.src.Application.Validators;

public class RequestDtoUpdateReqValidator : AbstractValidator<RequestDtoUpdateReq>
{
    public RequestDtoUpdateReqValidator(IValidatorService validatorService)
    {
        RuleFor(x => x.AdminId)
            .MustAsync(async (dto, adminId, cancellationToken) =>
            {
                return await validatorService.ExistsAsync<Request>(dto.Id, cancellationToken);
            })
            .WithMessage("Request not found.")
            .MustAsync(async (dto, adminId, cancellationToken) =>
            {
                var request = await validatorService.GetEntityAsync<Request>(dto.Id, cancellationToken);
                if (adminId is null)
                {
                    return request!.AdminId != null;
                }
                return await validatorService.ExistsAsync<Admin>(adminId.Value, cancellationToken);
            })
            .WithMessage("AdminId can only be null if the Request already has an Admin assigned. Otherwise, a valid Admin must be provided.");

        RuleFor(x => x.ResponsedById)
            .NotNull().WithMessage("ResponsedById is required.")
            .MustAsync(async (id, ct) =>
                await validatorService.ExistsAsync<Admin>(id, ct) ||
                await validatorService.ExistsAsync<Staff>(id, ct))
            .WithMessage("ResponsedById must reference a valid admin or staff.");
    }
}
