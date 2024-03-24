namespace Contracts.RepositoryContracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    IWaterConsumptionRepository WaterConsumption { get; }
    IProductConsumptionRepository ProductConsumption { get; }
    Task SaveAsync();
}