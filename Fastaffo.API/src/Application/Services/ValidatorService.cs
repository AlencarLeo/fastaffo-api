using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

namespace fastaffo_api.src.Application.Services;

public class ValidatorService : IValidatorService
{
    private readonly DataContext _context;

    public ValidatorService(DataContext context)
    {
        _context = context;
    }

    public async Task ValidateAsync<T>(IValidator<T> validator, T request)
    {
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new Exception($"Validation failed: {errors}");
        }
    }

    public async Task<bool> ExistsAsync<TEntity>(Guid? id, CancellationToken ct = default) where TEntity : class
    {
        if (id is null)
        {
            return false;
        }
        
        var set = _context.Set<TEntity>();
        var entity = await set.FindAsync([id], ct);
        return entity != null;
    }

}
