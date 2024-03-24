using System.Text.Json;
using Contracts.ServiceContracts;
using Controllers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;

namespace Controllers;

[ApiController]
[Authorize]
[Route("api/productconsumption")]
public class ProductConsumptionController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ProductConsumptionController(IServiceManager serviceManger)
    {
        _serviceManager = serviceManger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductConsumptions([FromQuery] ProductConsumptionParameters parameters)
    {
        var result =
            await _serviceManager.ProductConsumption.GetAllProductConsumptionAsync(parameters, HttpContext.GetUserId(),
                trackChanges: false);
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.metaData));
        return Ok(result.data);
    }
    [HttpGet("date/{time:datetime}")]
    public async Task<IActionResult> GetAllProductConsumtionsByDate([FromQuery] ProductConsumptionParameters parameters, DateTime time)
    {
        var result =
            await _serviceManager.ProductConsumption.GetAllProductConsumptionByDateAsync(parameters,
                HttpContext.GetUserId(), time, trackChanges: false);
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.metaData));
        return Ok(result.data);
    }

    [HttpGet("{productConsumptionId:guid}",Name = "GetProductConsumptionById")]
    public async Task<IActionResult> GetProductConsumptionById([FromQuery] ProductConsumptionParameters parameters,
        Guid productConsumptionId)
    {
        var result =
            await _serviceManager.ProductConsumption.GetProductConsumptionByIdAsync(parameters,HttpContext.GetUserId(),productConsumptionId,trackChanges:false);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductConsumption([FromBody] CProductConsumptionDto productDto)
    {
        var result = await _serviceManager.ProductConsumption.CreateProductConsumption(HttpContext.GetUserId()
            , productDto);
        return CreatedAtRoute("GetProductConsumptionById", new { productConsumptionId = result.Id }, result);
    }

    [HttpPut("{productConsumptionId:guid}")]
    public async Task<IActionResult> UpdateProductConsumption([FromBody] UProductConsumptionDto productConsumptionDto,
        Guid productConsumptionId)
    {
        await _serviceManager.ProductConsumption.UpdateProductConsumption(HttpContext.GetUserId(),
            productConsumptionDto, productConsumptionId);
        return NoContent();
    }

    [HttpDelete("{productConsumptionId:guid}")]
    public async Task<IActionResult> DeleteProductConsumption(Guid productConsumptionId)
    {
        await _serviceManager.ProductConsumption.DeleteProductConsumption(HttpContext.GetUserId(),productConsumptionId);
        return NoContent();
    }
}