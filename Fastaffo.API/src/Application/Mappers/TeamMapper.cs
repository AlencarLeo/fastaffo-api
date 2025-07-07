using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;
public static class TeamMapper
{
    public static TeamDtoRes ToDto(Team entity)
    {
        return new TeamDtoRes
        {
            Id = entity.Id,
            JobId = entity.JobId,
            Job = entity.Job is not null ? JobMapper.ToDto(entity.Job) : null,
            Name = entity.Name,
            SupervisorStaffId = entity.SupervisorStaffId,
            SupervisorStaff = entity.SupervisorStaff is not null ? StaffMapper.ToDto(entity.SupervisorStaff) : null,
            SupervisorAdminId = entity.SupervisorAdminId,
            SupervisorAdmin = entity.SupervisorAdmin is not null ? AdminMapper.ToDto(entity.SupervisorAdmin) : null,
        };
    }

    // public static Team ToEntity(TeamDtoReq dto)
    // {
    //     return new Team
    //     {
    //     };
    // }

}
