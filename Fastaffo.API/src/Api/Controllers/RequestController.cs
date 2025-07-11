using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;


[Route("api/")]
[ApiController]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpPost]
    [Route("request")]
    public async Task<ActionResult> CreateRequest(RequestDtoCreateReq request)
    {
        try
        {
            await _requestService.CreateRequestAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
