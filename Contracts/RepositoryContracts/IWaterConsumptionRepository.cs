using Entities.Models;

namespace Contracts.RepositoryContracts;

public interface IWaterConsumptionRepository
{
    Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsAsync(string userId, bool trackChanges);
    Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsByDateAsync(string userId, DateTime time, bool trackChanges);
    Task<WaterConsumption?> GetWaterConsumptionById(string userId, Guid waterConsumptionId, bool trackChanges);
    void CreateWaterConsumption(WaterConsumption waterConsumption);
    void DeleteWaterConsumption(WaterConsumption waterConsumption);
}