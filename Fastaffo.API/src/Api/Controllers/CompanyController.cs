using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace fastaffo_api.src.Api.Controllers;

[Route("api/")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;

    }

    [HttpPost]
    [Route("company")]
    public async Task<ActionResult> CreateCompany(CompanyDtoReq request)
    {
        try
        {
            await _companyService.CreateCompanyAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
