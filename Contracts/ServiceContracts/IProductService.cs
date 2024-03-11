using Entities.Models;
using Shared.Dto.CreationResourcesDto;
using Shared.Dto.ResponseDto;
using Shared.Dto.UpdateResourcesDto;

namespace Contracts.ServiceContracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(bool trackChanges);
    Task<ProductDto> GetProductByIdAsync(Guid productId,bool trackChanges);

    Task<ProductDto> CreateProductAsync(CProductDto productDto, string userId);
    Task<bool> IsProductCreator(Guid productId,string userId);
    Task UpdateProductAsync(Guid productId, UProductDto productDto);
    Task DeleteProductAsync(Guid productId);
    Task<(Product productEntity, UProductDto productDto)> GetProductForPatchAsync(Guid productId);
    Task SavePatchedProductAsync(Product productEntity, UProductDto productDto);

}