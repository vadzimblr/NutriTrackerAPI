namespace Contracts.RepositoryContracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    IWaterConsumptionRepository WaterConsumption { get; }
    Task SaveAsync();
}