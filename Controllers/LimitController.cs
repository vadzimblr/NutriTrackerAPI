using Asp.Versioning;
using Contracts.ServiceContracts;
using Controllers.ActionFilters;
using Controllers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.UpdateResourcesDto;

namespace Controllers;
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Authorize]
[Route("api/{v:apiversion}/limits")]


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
    public async Task<IActionResult> UpdateLimits( [FromBody] ManipulationLimitDto? limitDto)
    {
        await _serviceManager.Limit.CreateLimit(HttpContext.GetUserId(),limitDto);
        
        return StatusCode(201);
    }
}