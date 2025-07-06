using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;
public static class CompanyMapper
{
    public static CompanyDtoRes ToDto(Company entity)
    {
        return new CompanyDtoRes
        {
            Id = entity.Id,
            Name = entity.Name,
            ABN = entity.ABN,
            WebsiteUrl = entity.WebsiteUrl,
            ContactInfo = ContactInfoMapper.ToDto(entity.ContactInfo)
        };
    }

    public static Company ToEntity(CompanyDtoReq dto)
    {
        return new Company
        {
            Name = dto.Name, 
            ABN = dto.ABN, 
            WebsiteUrl = dto.WebsiteUrl, 
            ContactInfo = ContactInfoMapper.ToEntity(dto.ContactInfo)
        };
    }

}