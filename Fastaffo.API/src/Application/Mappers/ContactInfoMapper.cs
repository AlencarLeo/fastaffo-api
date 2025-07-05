using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;

public static class ContactInfoMapper
{
    public static ContactInfoDto? ToDto(ContactInfo? entity)
    {
        if (entity == null) return null;

        return new ContactInfoDto
        {
            PhotoLogoUrl = entity.PhotoLogoUrl,
            PhoneNumber = entity.PhoneNumber,
            PostalCode = entity.PostalCode,
            State = entity.State,
            City = entity.City,
            AddressLine1 = entity.AddressLine1,
            AddressLine2 = entity.AddressLine2
        };
    }

    public static ContactInfo? ToEntity(ContactInfoDto? dto)
    {
        if (dto == null) return null;

        return new ContactInfo
        {
            PhotoLogoUrl = dto.PhotoLogoUrl,
            PhoneNumber = dto.PhoneNumber,
            PostalCode = dto.PostalCode,
            State = dto.State,
            City = dto.City,
            AddressLine1 = dto.AddressLine1,
            AddressLine2 = dto.AddressLine2
        };
    }
}
