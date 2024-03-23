using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.RepositoryContracts;

public interface IWaterConsumptionRepository
{
    Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsAsync(WaterConsumptionParameters parameters, string userId, bool trackChanges);
    Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsByDateAsync(WaterConsumptionParameters parameters,string userId, DateTime time, bool trackChanges);
    Task<WaterConsumption?> GetWaterConsumptionById(string userId, Guid waterConsumptionId, bool trackChanges);
    void CreateWaterConsumption(WaterConsumption waterConsumption);
    void DeleteWaterConsumption(WaterConsumption waterConsumption);
}