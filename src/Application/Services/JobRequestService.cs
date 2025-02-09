using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Domain.Enums;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class JobRequestService : IJobRequestService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    public JobRequestService(IHttpContextAccessor httpContextAccessor, DataContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    private async Task<ServiceResponseDto> AssignStaffToJob(JobRequest jobRequest, Job job, Guid adminId, Guid staffId)
    {
        var admin = await _context.Admins.FindAsync(adminId);
        if (admin is null){
            return new ServiceResponseDto("Job Request done, but admin was not found", 202);
        }

        jobRequest.AdminId = adminId;
        jobRequest.StaffId = staffId;

        jobRequest.Status = RequestStatus.Accepted;
        jobRequest.ResponsedAt = DateTime.Now;

        job.CurrentStaffCount++;
        
        var jobStaff = new JobStaff
        {
            JobId = job.Id,
            StaffId = staffId,
            AddRate = job.BaseRate,
            AddStartDateTime = job.LocalStartDateTime.DateTime,
            JobRole = "Default"
        };
        
        _context.JobStaffs.Add(jobStaff);
        await _context.SaveChangesAsync();
        return new ServiceResponseDto("Request accept", 200);
    }

    private async Task<ServiceResponseDto> TryAutoAcceptRequestAsync(RequestType requestType, Job job,  Guid adminId, Guid staffId)
    {
        var oppositeRequestType = requestType == RequestType.AdminRequest
            ? RequestType.StaffRequest
            : RequestType.AdminRequest;

        var matchingRequest = await _context.JobRequests
            .FirstOrDefaultAsync(r => r.JobId == job.Id && r.StaffId == staffId && r.Type == oppositeRequestType && r.Status == RequestStatus.Pending);


        if (matchingRequest != null && matchingRequest.Status == RequestStatus.Pending)
        {
            var result = await AssignStaffToJob(matchingRequest, job, adminId, staffId);
            await _context.SaveChangesAsync();
            return new ServiceResponseDto(result.Message, result.StatusCode);
        }

        var jobRequest = new JobRequest
        {
            JobId = job.Id,
            StaffId = staffId,
            Status = RequestStatus.Pending,
            Type = requestType,
            RequestedAt = DateTime.Now
        };
        _context.JobRequests.Add(jobRequest);
        await _context.SaveChangesAsync();

        return new ServiceResponseDto("Request done", 200);
    }

    public async Task<ServiceResponseDto> CreateJobRequestAsync(RequestType requestType, Guid jobId, Guid? adminId, Guid staffId)
    {
        var job = await _context.Jobs.FindAsync(jobId);
        var staff = await _context.Staffs.FindAsync(staffId);

        if (job is null){
            return new ServiceResponseDto("Job not found", 404);
        }

        if (staff is null){
            return new ServiceResponseDto("staff not found", 404);
        }

        if (job.CurrentStaffCount >= job.MaxStaffNumber){
            return new ServiceResponseDto("Job staff limit reached", 409);
        }

        var existingRequest = await _context.JobRequests
            .FirstOrDefaultAsync(r => 
                r.JobId == jobId && 
                r.StaffId == staffId && 
                r.Type == requestType && 
                r.Status != RequestStatus.Pending);

        if (existingRequest != null){
            return new ServiceResponseDto("Request already exists", 409);
        }
        
        if(job.JobStaffs?.Any(js => js.Id == staffId) == true){
            return new ServiceResponseDto("Staff is already on this job", 409);
        }
        

        if(adminId.HasValue){
            var autoAccept = await TryAutoAcceptRequestAsync(requestType, job, adminId.Value, staffId);
            return new ServiceResponseDto(autoAccept.Message, autoAccept.StatusCode);
        }

        var jobRequest = new JobRequest
        {
            JobId = jobId,
            StaffId = staffId,
            Status = RequestStatus.Pending,
            Type = requestType,
            RequestedAt = DateTime.Now
        };
        _context.JobRequests.Add(jobRequest);
        await _context.SaveChangesAsync();
        
        return new ServiceResponseDto("Request done", 200);
    }

    public async Task<ServiceResponseDto> DeclineJobRequest(Guid jobRequestId, Guid adminId, Guid staffId)
    {
        var jobRequest = await _context.JobRequests.FindAsync(jobRequestId);

        if(jobRequest == null){
            return new ServiceResponseDto("This request don't exist ", 404);
        }

        jobRequest.AdminId = adminId;
        jobRequest.StaffId = staffId;

        jobRequest.Status = RequestStatus.Rejected;
        jobRequest.ResponsedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return new ServiceResponseDto("Request done", 200);
    }

    public async Task<ServiceResponseDto> ApproveJobRequest(Guid jobRequestId, Guid jobId, Guid adminId, Guid staffId)
    {
        var jobRequest = await _context.JobRequests.FindAsync(jobRequestId);
        var job = await _context.Jobs.FindAsync(jobId);
        var staff = await _context.Staffs.FindAsync(staffId);
        var admin = await _context.Admins.FindAsync(adminId);

        if(jobRequest == null){
            return new ServiceResponseDto("This request doesn't exist ", 404);
        }

        if(job == null){
            return new ServiceResponseDto("This job doesn't exist ", 404);
        }

        if(staff == null){
            return new ServiceResponseDto("This staff doesn't exist ", 404);
        }

        if(admin == null){
            return new ServiceResponseDto("This admin doesn't exist ", 404);
        }

        if(job.CurrentStaffCount >= job.MaxStaffNumber){
            return new ServiceResponseDto("Job staff limit reached ", 409);
        }

        await AssignStaffToJob(jobRequest, job, adminId, staffId);

        return new ServiceResponseDto("Request done", 200);
    }

}
