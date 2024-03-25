namespace Contracts.RepositoryContracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    IWaterConsumptionRepository WaterConsumption { get; }
    IProductConsumptionRepository ProductConsumption { get; }
    ILimitRepository Limit { get; }
    Task SaveAsync();
}