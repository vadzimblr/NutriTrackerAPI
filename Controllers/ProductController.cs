using Contracts.ServiceContracts;
using Controllers.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.UpdateResourcesDto;

namespace Controllers;
[ApiController]
[Authorize]
[Route("api/product")]
public class ProductController:ControllerBase
{
    private readonly IServiceManager _service;
    private readonly UserManager<User> _userManager;
    
    public ProductController(IServiceManager service, UserManager<User> userManager)
    {
        _service = service;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var result =await _service.Product.GetAllProductsAsync(trackChanges:false);
        return Ok(result);
    }
    
    [HttpGet("{productId:guid}",Name = "GetProductById")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var result = await _service.Product.GetProductByIdAsync(productId, trackChanges: false);
        return Ok(result);
    }
    
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateProduct([FromBody] CProductDto? productDto)
    {
        var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        var result = await _service.Product.CreateProductAsync(productDto,user.Id);
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