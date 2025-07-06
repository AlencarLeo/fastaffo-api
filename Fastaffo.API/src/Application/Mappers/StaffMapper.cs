using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;
public static class StaffMapper
{
    public static StaffDtoRes ToDto(Staff entity)
    {
        return new StaffDtoRes
        {
            Id = entity.Id,
            Name = entity.Name,
            Lastname = entity.Lastname,
            Email = entity.Email,
            ContactInfo = ContactInfoMapper.ToDto(entity.ContactInfo)
        };
    }

    public static Staff ToEntity(StaffDtoReq dto)
    {
        return new Staff
        {
            Name = dto.Name,
            Lastname = dto.Lastname,
            Email = dto.Email,
            Password = dto.Password,
            ContactInfo = ContactInfoMapper.ToEntity(dto.ContactInfo)
        };
    }

}