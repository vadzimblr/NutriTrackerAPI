using System.Security.Claims;
using System.Text.Json;
using Contracts.ServiceContracts;
using Controllers.ActionFilters;
using Controllers.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.UpdateResourcesDto;
using Shared.RequestFeatures;

namespace Controllers;
[ApiController]
[Authorize]
[Route("api/product")]
public class ProductController:ControllerBase
{
    private readonly IServiceManager _service;
    
    
    public ProductController(IServiceManager service)
    {
        _service = service;
    }
    
    [HttpGet]
    [ServiceFilter(typeof(ProductParametersValidationFilter))]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters parameters)
    {
        var result =await _service.Product.GetAllProductsAsync(parameters,trackChanges:false);
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.metaData));
        
        return Ok(result.products);
        
    }
    
    [HttpGet("{productId:guid}",Name = "GetProductById")]
    public async Task<IActionResult> GetProductById(Guid productId,[FromQuery] ProductParameters parameters)
    {
        var result = await _service.Product.GetProductByIdAsync(productId, parameters,trackChanges: false);
        return Ok(result);
    }
    
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateProduct([FromBody] CProductDto? productDto)
    {
        var result = await _service.Product.CreateProductAsync(productDto,HttpContext.GetUserId());
        return CreatedAtRoute("GetProductById",new {productId = result.Id},result);
    }
    
    [HttpPut("{productId:guid}")]
    [ServiceFilter(typeof(ValidationFilter))]
    [ServiceFilter(typeof(ProductEditPermissionFilter))]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UProductDto? productDto)
    {
        await _service.Product.UpdateProductAsync(productId, productDto);
        return NoContent();
    }
    
    [HttpDelete("{productId}")]
    [ServiceFilter(typeof(ProductEditPermissionFilter))]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _service.Product.DeleteProductAsync(productId);
        return NoContent();
    }

    [HttpPatch("{productId:guid}")]
    [ServiceFilter(typeof(ProductEditPermissionFilter))]
    public async Task<IActionResult> PatchProduct(Guid productId, [FromBody] JsonPatchDocument<UProductDto>? patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest("Provided patchDoc is null.");
        }
        var result = await _service.Product.GetProductForPatchAsync(productId);
        patchDoc.ApplyTo(result.productDto,ModelState);
        TryValidateModel(result.productDto);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _service.Product.SavePatchedProductAsync(result.productEntity, result.productDto);
        return NoContent();
    }
}