using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.RepositoryContracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync(ProductParameters parameters,bool trackChanges);
    Task<Product> GetProductByIdAsync(Guid productId, bool trackChanges);
    Task<IEnumerable<Product>> GetAllProductsByAuthorId(Guid userId, bool trackChanges);
    void CreateProduct(Product productEntity);
    void DeleteProduct(Product productEntity);
    void UpdateProduct(Product productEntity);
}