using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Domain.Enums;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Interfaces;
public class JobStaffService : IJobStaffService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    public JobStaffService(IHttpContextAccessor httpContextAccessor, DataContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ServiceResponseDto> RemoveJobStaff(Guid jobStaffId)
    {
        // Create RemoveStaffFromJob on JobService -> remove o staff do job: CurrentStaffCount--;
        // Adicionar no historico quando tiver essa feature (jobReq mantem mas adiciona remocao no historico);

        var jobStaff = await _context.JobStaffs.FindAsync(jobStaffId);

        if(jobStaff is null){
            return new ServiceResponseDto("Job staff not found", 404);
        }

        var job = await _context.Jobs.FindAsync(jobStaff.JobId);

        if(job is null){
            return new ServiceResponseDto("Job not found", 404);
        }

        job.CurrentStaffCount--;
        _context.JobStaffs.Remove(jobStaff);
    
        await _context.SaveChangesAsync();

        return new ServiceResponseDto("Job staff not found", 200);
    }
    
}