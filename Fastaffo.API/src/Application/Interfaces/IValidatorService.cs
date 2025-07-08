using FluentValidation;

namespace fastaffo_api.src.Application.Interfaces;

public interface IValidatorService
{
    public Task ValidateAsync<T>(IValidator<T> validator, T request);
    public Task<bool> ExistsAsync<TEntity>(Guid? id, CancellationToken ct = default) where TEntity : class;
}
