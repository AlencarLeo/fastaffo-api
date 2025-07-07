using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
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

        var newStaffJob = StaffJobMapper.ToEntity(request);

        await _context.AddAsync(newStaffJob);
        await _context.SaveChangesAsync();
    }

    public async Task<ServiceResponseDto<StaffJobDtoRes>> GetStaffJobByIdAsync(Guid staffJobId)
    {
        var staffJob = await _context.StaffJobs
                                                .Include(sj => sj.Staff!.ContactInfo)
                                                .Include(sj => sj.Job)
                                                    .ThenInclude(j => j!.CreatedBy!.ContactInfo)
                                                .Include(sj => sj.Job)
                                                    .ThenInclude(j => j!.Company!.ContactInfo)
                                                .Include(sj => sj.Team)
                                                    .ThenInclude(t => t!.SupervisorAdmin!.ContactInfo)
                                                .Include(sj => sj.Team)
                                                    .ThenInclude(t => t!.SupervisorStaff!.ContactInfo)
                                                .Include(sj => sj.Team)
                                                    .ThenInclude(t => t!.Job)
                                                        .ThenInclude(j => j!.CreatedBy!.ContactInfo)
                                                .Include(sj => sj.Team)
                                                    .ThenInclude(t => t!.Job)
                                                        .ThenInclude(j => j!.Company!.ContactInfo)
                                                .FirstOrDefaultAsync(sj => sj.Id == staffJobId);

        if (staffJob == null)
        {
            return new ServiceResponseDto<StaffJobDtoRes>(null, "StaffJob not found.", 404);
        }

        var staffDtoRes = StaffJobMapper.ToDto(staffJob);

        return new ServiceResponseDto<StaffJobDtoRes>(staffDtoRes, "StaffJob retrieved successfully.", 200);
    }

}
