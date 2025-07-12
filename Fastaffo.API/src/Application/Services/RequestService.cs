using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
using fastaffo_api.src.Domain.Enums;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Services;

public class RequestService : IRequestService
{
    private readonly DataContext _context;
    private readonly IValidatorService _validatorService;
    private readonly IValidator<RequestDtoCreateReq> _requestDtoCreateReqValidator;
    private readonly IValidator<RequestDtoUpdateReq> _requestDtoUpdateReqValidator;
    public RequestService(
        DataContext context,
        IValidatorService validatorService,
        IValidator<RequestDtoCreateReq> requestDtoCreateReqValidator,
        IValidator<RequestDtoUpdateReq> requestDtoUpdateReqValidator
    )
    {
        _context = context;
        _validatorService = validatorService;
        _requestDtoCreateReqValidator = requestDtoCreateReqValidator;
        _requestDtoUpdateReqValidator = requestDtoUpdateReqValidator;
    }

    public async Task CreateRequestAsync(RequestDtoCreateReq request, CancellationToken ct = default)
    {
        // REQUEST TO JOIN A JOB => THEN THE ADMINS IS RESPONSABLE TO STRUCT STAFFS MEMBERS INSIDE THE JOB'S TEAMS

        await _validatorService.ValidateAsync(_requestDtoCreateReqValidator, request);

        bool isDuplicate = await _context.Requests.AnyAsync(r =>
            r.Type == request.Type &&
            r.JobId == request.JobId &&
            r.StaffId == request.StaffId &&
            r.CompanyId == request.CompanyId,
            ct
        );

        if (isDuplicate)
        {
            throw new Exception("A request with the same type, job, staff, and company already exists.");
        }


        var existingRequest = await _context.Requests.FirstOrDefaultAsync(r =>
            r.Type != request.Type &&
            r.JobId == request.JobId &&
            r.StaffId == request.StaffId &&
            r.CompanyId == request.CompanyId,
            ct
        );

        if (existingRequest is not null)
        {
            existingRequest.Status = RequestStatus.Approved;
        }
        else
        {
            var newRequest = RequestMapper.ToEntity(request);
            _context.Requests.Add(newRequest);
        }

        await _context.SaveChangesAsync(ct);
    }

    private async Task UpdateRequestStatusAsync(RequestDtoUpdateReq request, RequestStatus status, CancellationToken ct = default)
    {
        await _validatorService.ValidateAsync(_requestDtoUpdateReqValidator, request);

        var existingRequest = await _context.Requests.FirstOrDefaultAsync(r => r.Id == request.Id, ct);

        if (existingRequest is null)
            throw new Exception("Request not found.");

        if (existingRequest.Status != RequestStatus.Pending)
            throw new Exception("Only pending requests can be updated.");

        request.Status = status;
        RequestMapper.ToEntity(request, existingRequest);

        await _context.SaveChangesAsync(ct);
    }

    public async Task ApproveRequestAsync(RequestDtoUpdateReq request, CancellationToken ct = default)
    {
        await UpdateRequestStatusAsync(request, RequestStatus.Approved, ct);
    }

    public async Task RejectRequestAsync(RequestDtoUpdateReq request, CancellationToken ct = default)
    {
        await UpdateRequestStatusAsync(request, RequestStatus.Rejected, ct);
    }

}
