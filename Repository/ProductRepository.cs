using Contracts.RepositoryContracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductRepository:RepositoryBase<Product>,IProductRepository
{
    public ProductRepository(RepositoryContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(Guid productId, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(productId), trackChanges).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsByAuthorId(Guid userId, bool trackChanges)
    {
        return await FindByCondition(p => p.UserId.Equals(Convert.ToString(userId)), trackChanges).ToListAsync();
    }

    public void CreateProduct(Product productEntity)
    {
        Create(productEntity);
    }

    public void DeleteProduct(Product productEntity)
    {
        Delete(productEntity);
    }

    public void UpdateProduct(Product productEntity)
    {
        Update(productEntity);
    }
}