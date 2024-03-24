using System.Dynamic;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.RepositoryContracts;

public interface IProductConsumptionRepository
{
    Task<IEnumerable<ProductConsumption>> GetAllProductConsumptionAsync(
        ProductConsumptionParameters parameters, string userId, bool trackChanges);
    Task<IEnumerable<ProductConsumption>>GetAllProductConsumptionByDateAsync(
        ProductConsumptionParameters parameters, string userId, DateTime time, bool trackChanges);

    Task<ProductConsumption> GetProductConsumptionByIdAsync(string userId,
        Guid productConsumptionId,
        bool trackChanges);

    void CreateProductConsumption(ProductConsumption productConsumption);
    void DeleteProductConsumption(ProductConsumption productConsumption);
}