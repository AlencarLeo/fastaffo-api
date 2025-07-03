using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

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

    public async Task<ServiceResponseDto<StaffJobDtoRes>> GetStaffJobByIdAsync(Guid staffJobId)
    {
                var staffJob = await _context.StaffJobs
                                        .Include(s => s.Staff)
                                        .FirstOrDefaultAsync(s => s.Id == staffJobId);

        if (staffJob == null)
        {
            return new ServiceResponseDto<StaffJobDtoRes>(null, "StaffJob not found.", 404);
        }

        var staffDtoRes = new StaffJobDtoRes
        {
            Id = staffJob.Id,
            StaffId = staffJob.StaffId,
            Staff = staffJob.Staff is not null
                ? new StaffDtoRes
                {
                    Id = staffJob.Staff.Id,
                    Name = staffJob.Staff.Name,
                    Lastname = staffJob.Staff.Lastname,
                    Email = staffJob.Staff.Email,
                    ContactInfo = staffJob.Staff.ContactInfo is not null
                        ? new ContactInfoDto
                        {
                            PhotoLogoUrl = staffJob.Staff.ContactInfo.PhotoLogoUrl,
                            PhoneNumber = staffJob.Staff.ContactInfo.PhoneNumber,
                            PostalCode = staffJob.Staff.ContactInfo.PostalCode,
                            State = staffJob.Staff.ContactInfo.State,
                            City = staffJob.Staff.ContactInfo.City,
                            AddressLine1 = staffJob.Staff.ContactInfo.AddressLine1,
                            AddressLine2 = staffJob.Staff.ContactInfo.AddressLine2
                        }
                        : null
                }
                : null,
            JobId = staffJob.JobId,
            // Job = staffJob.Job,
            TeamId = staffJob.TeamId,
            // Team = staffJob.Team,
            Role = staffJob.Role,
            StartTime = staffJob.StartTime,
            FinishTime = staffJob.FinishTime,
            BaseRate = staffJob.BaseRate,
            TravelTimeMinutes = staffJob.TravelTimeMinutes,
            Kilometers = staffJob.Kilometers,
            Notes = staffJob.Notes,
            TotalAmount = staffJob.TotalAmount,
            Title = staffJob.Title,
            Location = staffJob.Location,
            IsPersonalJob = staffJob.IsPersonalJob,
        };

        return new ServiceResponseDto<StaffJobDtoRes>(staffDtoRes, "StaffJob retrieved successfully.", 200);
    }

}
