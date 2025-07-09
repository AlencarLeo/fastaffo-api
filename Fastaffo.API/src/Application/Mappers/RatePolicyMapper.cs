using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Domain.Entities;

namespace fastaffo_api.src.Application.Mappers;
public static class RatePolicyMapper
{
    // public static RatePolicyDtoRes ToDto(RatePolicy entity)
    // {
    //     return new RatePolicyDtoRes
    //     {
    //     };
    // }

    public static RatePolicy ToEntity(RatePolicyDtoReq dto)
    {
        return new RatePolicy
        {
            CompanyId = dto.CompanyId,
            OvertimeStartMinutes = dto.OvertimeStartMinutes, 
            OvertimeMultiplier = dto.OvertimeMultiplier, 
            DayMultiplier = dto.DayMultiplier, 
            TravelTimeRate = dto.TravelTimeRate, 
            KilometersRate = dto.KilometersRate 
        };
    }

}
