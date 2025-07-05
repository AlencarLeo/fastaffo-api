using FluentValidation;

namespace fastaffo_api.src.Application.Interfaces;
public interface IValidatorService
{
    public Task ValidateAsync<T>(IValidator<T> validator, T request);
}