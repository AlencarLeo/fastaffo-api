using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Domain.Enums;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Api.Controllers;
[Route("api/")]
[ApiController]
public class JobRequestController : ControllerBase
{

    private readonly DataContext _context;
    public JobRequestController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("job-request")]
    public async Task<ActionResult> CreateJobRequest(JobRequestDtoReq request)
    {
        var job = await _context.Jobs.FindAsync(request.JobId);
        var staff = await _context.Staffs.FindAsync(request.StaffId);
        
        if (job is null){
            return NotFound("Job not found");
        }

        if (staff is null){
            return NotFound("staff not found");
        }

        if (job.CurrentStaffCount >= job.MaxStaffNumber){
            return BadRequest("Job staff limit reached");
        }

        var existingRequest = await _context.JobRequests
            .FirstOrDefaultAsync(r => r.JobId == request.JobId && r.StaffId == request.StaffId && r.Type == request.Type);
        if (existingRequest != null){
            return BadRequest("Request already exists");
        }

        var jobRequest = new JobRequest
        {
            JobId = request.JobId,
            StaffId = request.StaffId,
            Status = RequestStatus.Pending,
            Type = request.Type,
            RequestedAt = DateTime.UtcNow
        };

        _context.JobRequests.Add(jobRequest);
        await _context.SaveChangesAsync();

        if (request.Type == RequestType.AdminRequest)
        {
            var matchingRequest = await _context.JobRequests
                .FirstOrDefaultAsync(r => r.JobId == request.JobId && r.StaffId == request.StaffId && r.Type == RequestType.StaffRequest);

            if (matchingRequest != null && matchingRequest.Status == RequestStatus.Pending)
            {
                jobRequest.Status = RequestStatus.Accepted;
                var jobStaff = new JobStaff
                {
                    JobId = job.Id,
                    StaffId = jobRequest.StaffId,
                    AddRate = job.BaseRate,
                    AddStartDateTime = job.LocalStartDateTime.DateTime,
                    JobRole = "Default" // Pode ser ajustado
                };
                _context.JobStaffs.Add(jobStaff);
                await _context.SaveChangesAsync();
                job.CurrentStaffCount++;
                await _context.SaveChangesAsync();

                return Ok("Request automatically accepted");
            }
        }
        else if (request.Type == RequestType.StaffRequest)
        {
            var matchingRequest = await _context.JobRequests
                .FirstOrDefaultAsync(r => r.JobId == request.JobId && r.StaffId == request.StaffId && r.Type == RequestType.AdminRequest);

            if (matchingRequest != null && matchingRequest.Status == RequestStatus.Pending)
            {
                jobRequest.Status = RequestStatus.Accepted;
                var jobStaff = new JobStaff
                {
                    JobId = job.Id,
                    StaffId = jobRequest.StaffId,
                    AddRate = job.BaseRate,
                    AddStartDateTime = job.LocalStartDateTime.DateTime,
                    JobRole = "Default" // Pode ser ajustado
                };
                _context.JobStaffs.Add(jobStaff);
                await _context.SaveChangesAsync();
                job.CurrentStaffCount++;
                await _context.SaveChangesAsync();
                return Ok("Request automatically accepted");
            }
        }

        await _context.SaveChangesAsync();

        return Ok("Request created");
    }

}