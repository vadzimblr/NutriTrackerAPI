using Contracts.ServiceContracts;
using Controllers.ActionFilters;
using Controllers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.UpdateResourcesDto;

namespace Controllers;
[ApiController]
[Authorize]
[Route("api/limits")]
public class LimitController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public LimitController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetLimits()
    {
        var result = await _serviceManager.Limit.GetLimit(HttpContext.GetUserId(), trackChanges: false);
        return Ok(result);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> UpdateLimits([FromBody] ManipulationLimitDto? limitDto)
    {
        await _serviceManager.Limit.CreateLimit(limitDto);
        return StatusCode(201);
    }
}