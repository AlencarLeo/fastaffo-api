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
            Staff = staffJob.Staff is not null
                ? new StaffDtoRes
                {
                    Id = staffJob.Staff.Id,
                    Name = staffJob.Staff.Name,
                    Lastname = staffJob.Staff.Lastname,
                    Email = staffJob.Staff.Email,
                    ContactInfo = ContactInfoMapper.ToDto(staffJob.Staff.ContactInfo)
                }
                : null,
            JobId = staffJob.JobId,
            Job = staffJob.Job is not null
                ? new JobDtoRes
                {
                    Id = staffJob.Job.Id,
                    JobRef = staffJob.Job.JobRef,
                    EventName = staffJob.Job.EventName,
                    ChargedAmount = staffJob.Job.ChargedAmount,
                    ClientName = staffJob.Job.ClientName,
                    Location = staffJob.Job.Location,
                    Notes = staffJob.Job.Notes,
                    Status = staffJob.Job.Status,
                    CompanyId = staffJob.Job.CompanyId,
                    Company = staffJob.Job.Company is not null ? new CompanyDtoRes
                    {
                        Id = staffJob.Job.Company.Id,
                        Name = staffJob.Job.Company.Name,
                        ABN = staffJob.Job.Company.ABN,
                        WebsiteUrl = staffJob.Job.Company.WebsiteUrl,
                        ContactInfo = ContactInfoMapper.ToDto(staffJob.Job.Company.ContactInfo)
                    }
                    : null,
                    CreatedByAdminId = staffJob.Job.CreatedByAdminId,
                    CreatedBy = staffJob.Job.CreatedBy is not null
                    ? new AdminDtoRes
                    {
                        Id = staffJob.Job.CreatedBy.Id,
                        Name = staffJob.Job.CreatedBy.Name,
                        Lastname = staffJob.Job.CreatedBy.Lastname,
                        Email = staffJob.Job.CreatedBy.Email,
                        Role = staffJob.Job.CreatedBy.Role,
                        CompanyId = staffJob.Job.CreatedBy.CompanyId,
                        ContactInfo = ContactInfoMapper.ToDto(staffJob.Job.CreatedBy.ContactInfo)
                    }
                    : null,

                } : null,
            TeamId = staffJob.TeamId,
            Team = staffJob.Team is not null
                ? new TeamDtoRes
                {
                    Id = staffJob.Team.Id,
                    JobId = staffJob.Team.JobId,
                    Job = null,
                    Name = staffJob.Team.Name,
                    SupervisorStaffId = staffJob.Team.SupervisorStaffId,
                    SupervisorStaff = staffJob.Team.SupervisorStaff is not null
                ? new StaffDtoRes
                {
                    Id = staffJob.Team.SupervisorStaff.Id,
                    Name = staffJob.Team.SupervisorStaff.Name,
                    Lastname = staffJob.Team.SupervisorStaff.Lastname,
                    Email = staffJob.Team.SupervisorStaff.Email,
                    ContactInfo = ContactInfoMapper.ToDto(staffJob.Team.SupervisorStaff.ContactInfo)
                }
                : null,
                    SupervisorAdminId = staffJob.Team.SupervisorAdminId,
                    SupervisorAdmin = staffJob.Team.SupervisorAdmin is not null
                    ? new AdminDtoRes
                    {
                        Id = staffJob.Team.SupervisorAdmin.Id,
                        Name = staffJob.Team.SupervisorAdmin.Name,
                        Lastname = staffJob.Team.SupervisorAdmin.Lastname,
                        Email = staffJob.Team.SupervisorAdmin.Email,
                        Role = staffJob.Team.SupervisorAdmin.Role,
                        CompanyId = staffJob.Team.SupervisorAdmin.CompanyId,
                        ContactInfo = ContactInfoMapper.ToDto(staffJob.Team.SupervisorAdmin.ContactInfo)
                    }
                    : null,

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
