using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;
public static class AdminMapper
{
    public static AdminDtoRes ToDto(Admin entity)
    {
        return new AdminDtoRes
        {
            Id = entity.Id,
            Name = entity.Name,
            Lastname = entity.Lastname,
            Email = entity.Email,
            Role = entity.Role,
            CompanyId = entity.CompanyId,
            ContactInfo = ContactInfoMapper.ToDto(entity.ContactInfo)
        };
    }

    public static Admin ToEntity(AdminDtoReq dto)
    {
        return new Admin
        {
            Name = dto.Name,
            Lastname = dto.Lastname,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role,
            CompanyId = dto.CompanyId,
            ContactInfo = ContactInfoMapper.ToEntity(dto.ContactInfo)
        };
    }

}
