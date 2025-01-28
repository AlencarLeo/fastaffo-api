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

    private async Task AssignStaffToJob(JobRequest jobRequest, Job job, Guid staffId)
    {
        jobRequest.Status = RequestStatus.Accepted;
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
    }


    public async Task<ServiceResponseDto> CreateJobRequestAsync(RequestType requestType, Guid jobId, Guid staffId)
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
            .FirstOrDefaultAsync(r => r.JobId == jobId && r.StaffId == staffId && r.Type == requestType);
        if (existingRequest != null){
            return new ServiceResponseDto("Request already exists", 409);
        }

        var jobRequest = new JobRequest
        {
            JobId = jobId,
            StaffId = staffId,
            Status = RequestStatus.Pending,
            Type = requestType,
            RequestedAt = DateTime.UtcNow
        };

        var autoAccept = await this.TryAutoAcceptRequestAsync(requestType, job, staffId, jobRequest);

        _context.JobRequests.Add(jobRequest);
        await _context.SaveChangesAsync();

        if(autoAccept){
            return new ServiceResponseDto("Request accept", 200);
        }else{
            return new ServiceResponseDto("Request done", 200);
        }
    }


    public async Task<bool> TryAutoAcceptRequestAsync(RequestType requestType, Job job, Guid staffId, JobRequest jobRequest)
    {
        var oppositeRequestType = requestType == RequestType.AdminRequest
            ? RequestType.StaffRequest
            : RequestType.AdminRequest;

        var matchingRequest = await _context.JobRequests
            .FirstOrDefaultAsync(r => r.JobId == job.Id && r.StaffId == staffId && r.Type == oppositeRequestType);


        if (matchingRequest != null && matchingRequest.Status == RequestStatus.Pending)
        {
            await this.AssignStaffToJob(jobRequest, job, staffId);
            return true;
        }

        return false;
    }

    public async Task<ServiceResponseDto> DeclineJobRequest(Guid jobRequestId)
    {
        var jobRequest = await _context.JobRequests.FindAsync(jobRequestId);

        if(jobRequest == null){
            return new ServiceResponseDto("This request don't exist ", 404);
        }

        jobRequest.Status = RequestStatus.Rejected;
        return new ServiceResponseDto("Request done", 200);
    }

    public async Task<ServiceResponseDto> ApproveJobRequest(Guid jobRequestId, Guid jobId, Guid staffId)
    {
        var jobRequest = await _context.JobRequests.FindAsync(jobRequestId);
        var job = await _context.Jobs.FindAsync(jobId);
        var staff = await _context.Staffs.FindAsync(staffId);

        if(jobRequest == null){
            return new ServiceResponseDto("This request doesn't exist ", 404);
        }

        if(job == null){
            return new ServiceResponseDto("This job doesn't exist ", 404);
        }

        if(staff == null){
            return new ServiceResponseDto("This staff doesn't exist ", 404);
        }

        if(job.CurrentStaffCount >= job.MaxStaffNumber){
            return new ServiceResponseDto("Job staff limit reached ", 409);
        }

        await this.AssignStaffToJob(jobRequest, job, staffId);

        return new ServiceResponseDto("Request done", 200);
    }

}
