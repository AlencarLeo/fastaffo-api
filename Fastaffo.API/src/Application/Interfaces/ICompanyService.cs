using fastaffo_api.src.Application.DTOs;

namespace fastaffo_api.src.Application.Interfaces;

public interface ICompanyService
{
    Task CreateCompanyAsync(CompanyDtoReq request);
}
