using Contracts.RepositoryContracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ProductRepository:RepositoryBase<Product>,IProductRepository
{
    public ProductRepository(RepositoryContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync(ProductParameters parameters,bool trackChanges)
    {
        return await FindAll(trackChanges)
            .FilterProductsByCalories(parameters.MinCalories,parameters.MaxCalories)
            .FilterProductsByCarbs(parameters.MinCarbs,parameters.MaxCarbs)
            .FilterProductsByFat(parameters.MinFat,parameters.MaxFat)
            .FilterProductsByProtein(parameters.MinProtein,parameters.MaxProtein)
            .FilterProductsByServingSize(parameters.MinServingSize, parameters.MaxServingSize)
            .Search(parameters.searchTerm)
            .ToListAsync();
        
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