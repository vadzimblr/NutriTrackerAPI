using Contracts.ServiceContracts;
using Controllers.ActionFilters;
using Controllers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Controllers;
[ApiController]
[Authorize]
[Route("api/waterconsumption")]
public class WaterConsumptionController:ControllerBase
{
    private readonly IServiceManager _service;

    public WaterConsumptionController(IServiceManager service)
    {
        _service = service;
    }
    
    [HttpGet]
    [ServiceFilter(typeof(WaterConsumptionParametersValidationFilter))]
    public async Task<IActionResult> GetAllWaterConsumption([FromQuery]WaterConsumptionParameters parameters)
    {
        var result = await _service.WaterConsumption.GetAllWaterConsumptionsAsync(parameters,
            HttpContext.GetUserId(), trackChanges: false);
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.metaData));
        return Ok(result.data);
    }

    [HttpGet("date/{date:datetime}")]
    [ServiceFilter(typeof(WaterConsumptionParametersValidationFilter))]
    public async Task<IActionResult> GetAllWaterConsumptionByDate([FromQuery]WaterConsumptionParameters parameters,DateTime date)
    {
        var result =
            await _service.WaterConsumption.GetAllWaterConsumptionsByDateTimeAsync(parameters,
                HttpContext.GetUserId(), date, trackChanges: false);
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.metaData));
        return Ok(result.data);
    }

    [HttpGet("{waterConsumptionId:guid}",Name = "GetWaterConsumptionById")]
    public async Task<IActionResult> GetWaterConsumptionById([FromQuery] WaterConsumptionParameters parameters, Guid waterConsumptionId)
    {
        var result = await _service.WaterConsumption.GetWaterConsumptionByIdAsync(parameters,HttpContext.GetUserId(),waterConsumptionId,trackChanges:false);
        return Ok(result);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateWaterConsumption([FromBody] CWaterConsumptionDto? cWaterConsumptionDto)
    {
        var result = await _service.WaterConsumption.CreateWaterConsumption(HttpContext.GetUserId(), cWaterConsumptionDto);
        return CreatedAtRoute("GetWaterConsumptionById", new { waterConsumptionId = result.Id }, result);
    }
    
    [HttpPut("{waterConsumptionId:guid}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> UpdateWaterConsumption(Guid waterConsumptionId, [FromBody]UWaterConsumptionDto? uWaterConsumptionDto)
    {
        await _service.WaterConsumption.UpdateWaterConsumption(HttpContext.GetUserId(), waterConsumptionId, uWaterConsumptionDto);
        return NoContent();
    }

    [HttpDelete("{waterConsumptionId:guid}")]
    public async Task<IActionResult> DeleteWaterConsumption(Guid waterConsumptionId)
    {
        await _service.WaterConsumption.DeleteWaterConsumption(HttpContext.GetUserId(), waterConsumptionId);
        return NoContent();
    }
}