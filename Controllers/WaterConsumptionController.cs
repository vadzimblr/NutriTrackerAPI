using System.Security.Claims;
using Contracts.ServiceContracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;
[ApiController]
[Authorize]
[Route("api/waterconsumption")]
public class WaterConsumptionController:ControllerBase
{
    private readonly IServiceManager _service;
    private readonly string? _userId;

    public WaterConsumptionController(IServiceManager service)
    {
        _service = service;
        _userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllWaterConsumption()
    {
        
        return Ok("okich");
    }
}