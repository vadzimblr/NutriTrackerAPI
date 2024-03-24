using System.Dynamic;
using Contracts.RepositoryContracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ProductConsumptionRepository:RepositoryBase<ProductConsumption>, IProductConsumptionRepository
{
    public ProductConsumptionRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProductConsumption>> GetAllProductConsumptionAsync(ProductConsumptionParameters parameters, string userId, bool trackChanges)
    {
        return await FindByCondition(p => (p.UserId.Equals(userId)),trackChanges)
            .FilterByConsumedCalories(parameters.MinConsumedCalories,parameters.MaxConsumedCalories)
            .Include(p=>p.ConsumedProduct)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductConsumption>> GetAllProductConsumptionByDateAsync(ProductConsumptionParameters parameters, string userId, DateTime time,
        bool trackChanges)
    {
        return await FindByCondition(p => (p.UserId.Equals(userId) && p.ConsumptionTime.Date.Equals(time.Date)),trackChanges)
            .FilterByConsumedCalories(parameters.MinConsumedCalories,parameters.MaxConsumedCalories)
            .Include(p=>p.ConsumedProduct)
            .ToListAsync();
    }

    public async Task<ProductConsumption> GetProductConsumptionByIdAsync(string userId, Guid productConsumptionId,
        bool trackChanges)
    {
        return await FindByCondition(p => (p.UserId.Equals(userId) && p.Id.Equals(productConsumptionId)),trackChanges)
            .SingleOrDefaultAsync();
    }

    public void CreateProductConsumption(ProductConsumption productConsumption)
    {
        Create(productConsumption);
    }

    public void DeleteProductConsumption(ProductConsumption productConsumption)
    {
        Delete(productConsumption);
    }
}