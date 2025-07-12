using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Domain.Enums;

namespace fastaffo_api.src.Application.Mappers;

public static class RequestMapper
{
    // public static RequestDtoRes ToDto(Request entity)
    // {
    //     return new RequestDtoRes
    //     {
    //     };
    // }

    public static Request ToEntity(RequestDtoCreateReq dto)
    {
        return new Request
        {
            Type = dto.Type,
            JobId = dto.JobId,
            StaffId = dto.StaffId,
            AdminId = dto.AdminId,
            CompanyId = dto.CompanyId,
            SentById = dto.SentById
        };
    }

    public static Request ToEntity(RequestDtoUpdateReq dto, Request entity)
    {
        if (entity.AdminId is null)
        {
            entity.AdminId = dto.AdminId;
        }
        entity.ResponseDate = DateTime.Now;
        entity.ResponsedById = dto.ResponsedById;
        entity.Status = dto.Status;

        return entity;
    }

}
