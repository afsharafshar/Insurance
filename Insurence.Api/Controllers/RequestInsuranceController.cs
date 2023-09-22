using Insurence.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurence.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RequestInsuranceController : ControllerBase
{
    private readonly RequestService _requestService;

    public RequestInsuranceController(RequestService requestService)
    {
        _requestService = requestService;
    }
    
    [HttpGet("{Email}")]
    public async Task<IActionResult> Get(string email,CancellationToken cancellationToken)
    {
        var result =await _requestService.ShowInsurance(email,cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InsertRequestDto dto, CancellationToken cancellationToken)
    {
         await _requestService.InsertRequest(dto, cancellationToken);
         return Ok();
    }
}