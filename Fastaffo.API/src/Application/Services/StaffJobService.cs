using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

namespace fastaffo_api.src.Application.Services;

public class StaffJobService : IStaffJobService
{
    private readonly DataContext _context;
    private readonly IValidator<StaffJobDtoReq> _staffJobDtoReqValidator;

    public StaffJobService(DataContext context, IValidator<StaffJobDtoReq> staffJobDtoReqValidator)
    {
        _context = context;
        _staffJobDtoReqValidator = staffJobDtoReqValidator;
    }

    public async Task CreateStaffJobAsync(StaffJobDtoReq request)
    {
        var validationResult = await _staffJobDtoReqValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new Exception($"Validation failed: {errors}");
        }

        var newStaffJob = new StaffJob
        {
            StaffId = request.StaffId,
            JobId = request.JobId,
            TeamId = request.TeamId,
            Role = request.Role,
            StartTime = request.StartTime,
            FinishTime = request.FinishTime,
            BaseRate = request.BaseRate,
            TravelTimeMinutes = request.TravelTimeMinutes,
            Kilometers = request.Kilometers,
            Notes = request.Notes,
            TotalAmount = request.TotalAmount,
            Title = request.Title,
            Location = request.Location,
            IsPersonalJob = request.IsPersonalJob
        };

        await _context.AddAsync(newStaffJob);
        await _context.SaveChangesAsync();
    }

}
