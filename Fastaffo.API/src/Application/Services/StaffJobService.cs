using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Services;

public class StaffJobService : IStaffJobService
{
    private readonly DataContext _context;
    private readonly IValidatorService _validatorService;

    private readonly IValidator<StaffJobDtoReq> _staffJobDtoReqValidator;

    public StaffJobService(DataContext context, IValidatorService validatorService, IValidator<StaffJobDtoReq> staffJobDtoReqValidator)
    {
        _context = context;
        _validatorService = validatorService;
        _staffJobDtoReqValidator = staffJobDtoReqValidator;
    }

    public async Task CreateStaffJobAsync(StaffJobDtoReq request)
    {
        await _validatorService.ValidateAsync(_staffJobDtoReqValidator, request);

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
                                .Include(sj => sj.Staff)
                                    .ThenInclude(s => s!.ContactInfo)
                                .Include(sj => sj.Job)
                                .Include(sj => sj.Team)
                                .FirstOrDefaultAsync(sj => sj.Id == staffJobId);

        if (staffJob == null)
        {
            return new ServiceResponseDto<StaffJobDtoRes>(null, "StaffJob not found.", 404);
        }

        var staffDtoRes = new StaffJobDtoRes
        {
            Id = staffJob.Id,
            StaffId = staffJob.StaffId,
            Staff = staffJob.Staff is not null ? StaffMapper.ToDto(staffJob.Staff) : null,
            JobId = staffJob.JobId,
            Job = staffJob.Job is not null ? JobMapper.ToDto(staffJob.Job) : null,
            TeamId = staffJob.TeamId,
            Team = staffJob.Team is not null
                ? new TeamDtoRes
                {
                    Id = staffJob.Team.Id,
                    JobId = staffJob.Team.JobId,
                    Job = null,
                    Name = staffJob.Team.Name,
                    SupervisorStaffId = staffJob.Team.SupervisorStaffId,
                    SupervisorStaff = staffJob.Team.SupervisorStaff is not null ? StaffMapper.ToDto(staffJob.Team.SupervisorStaff) : null,
                    SupervisorAdminId = staffJob.Team.SupervisorAdminId,
                    SupervisorAdmin = staffJob.Team.SupervisorAdmin is not null ? AdminMapper.ToDto(staffJob.Team.SupervisorAdmin) : null,

                } : null,
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
