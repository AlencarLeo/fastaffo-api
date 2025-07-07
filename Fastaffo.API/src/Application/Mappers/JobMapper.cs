using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;
public static class JobMapper
{
    public static JobDtoRes ToDto(Job entity)
    {
        return new JobDtoRes
        {
            Id = entity.Id,
            JobRef = entity.JobRef,
            EventName = entity.EventName,
            ChargedAmount = entity.ChargedAmount,
            ClientName = entity.ClientName,
            Location = entity.Location,
            Notes = entity.Notes,
            Status = entity.Status,
            CompanyId = entity.CompanyId,
            Company = entity.Company is not null ? CompanyMapper.ToDto(entity.Company) : null,
            CreatedByAdminId = entity.CreatedByAdminId,
            CreatedBy = entity.CreatedBy is not null ? AdminMapper.ToDto(entity.CreatedBy) : null,
        };
    }

    public static Job ToEntity(JobDtoReq dto, string jobRef)
    {
        return new Job
        {
            JobRef = jobRef,
            EventName = dto.EventName,
            ChargedAmount = dto.ChargedAmount,
            ClientName = dto.ClientName,
            Location = dto.Location,
            Notes = dto.Notes,
            Status = dto.Status,
            CompanyId = dto.CompanyId,
            CreatedByAdminId = dto.CreatedByAdminId
        };
    }

}
