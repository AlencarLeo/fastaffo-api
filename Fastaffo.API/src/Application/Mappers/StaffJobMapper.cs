using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;
public static class StaffJobMapper
{
    public static StaffJobDtoRes ToDto(StaffJob entity)
    {
        return new StaffJobDtoRes
        {
            Id = entity.Id,
            StaffId = entity.StaffId,
            Staff = entity.Staff is not null ? StaffMapper.ToDto(entity.Staff) : null,
            JobId = entity.JobId,
            Job = entity.Job is not null ? JobMapper.ToDto(entity.Job) : null,
            TeamId = entity.TeamId,
            Team = entity.Team is not null ? TeamMapper.ToDto(entity.Team) : null,
            Role = entity.Role,
            StartTime = entity.StartTime,
            FinishTime = entity.FinishTime,
            BaseRate = entity.BaseRate,
            TravelTimeMinutes = entity.TravelTimeMinutes,
            Kilometers = entity.Kilometers,
            Notes = entity.Notes,
            TotalAmount = entity.TotalAmount,
            Title = entity.Title,
            Location = entity.Location,
            IsPersonalJob = entity.IsPersonalJob,
        };
    }

    public static StaffJob ToEntity(StaffJobDtoReq dto)
    {
        return new StaffJob
        {
            StaffId = dto.StaffId,
            JobId = dto.JobId,
            TeamId = dto.TeamId,
            Role = dto.Role,
            StartTime = dto.StartTime,
            FinishTime = dto.FinishTime,
            BaseRate = dto.BaseRate,
            TravelTimeMinutes = dto.TravelTimeMinutes,
            Kilometers = dto.Kilometers,
            Notes = dto.Notes,
            TotalAmount = dto.TotalAmount,
            Title = dto.Title,
            Location = dto.Location,
            IsPersonalJob = dto.IsPersonalJob
        };
    }

}
