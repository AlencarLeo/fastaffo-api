using fastaffo_api.src.Application.Interfaces;

using FluentValidation;

namespace fastaffo_api.src.Application.Services;

public class ValidatorService : IValidatorService
{
    public async Task ValidateAsync<T>(IValidator<T> validator, T request)
    {
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new Exception($"Validation failed: {errors}");
        }
    }

}