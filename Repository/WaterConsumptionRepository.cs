using Contracts.RepositoryContracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class WaterConsumptionRepository:RepositoryBase<WaterConsumption>,IWaterConsumptionRepository
{
    public WaterConsumptionRepository(RepositoryContext context) : base(context)
    {
    }
    public async Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsAsync(WaterConsumptionParameters parameters, string userId,bool trackChanges)
    {
        return await FindByCondition(w => (w.UserId.Equals(userId)), trackChanges)
            .FilterWaterConsumptionByAmount(parameters.MinAmount,parameters.MaxAmount)
            .ToListAsync();
    }

    public async Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsByDateAsync(WaterConsumptionParameters parameters,string userId,DateTime time, bool trackChanges)
    {
        return await FindByCondition(w => (w.UserId.Equals(userId) && w.ConsumptionTime.Date.Equals(time.Date)),trackChanges)
            .FilterWaterConsumptionByAmount(parameters.MinAmount,parameters.MaxAmount)
            .ToListAsync();
    }

    public async Task<WaterConsumption?> GetWaterConsumptionById(string userId,Guid waterConsumptionId, bool trackChanges)
    {
        return await FindByCondition(w =>(w.UserId.Equals(userId) && w.Id.Equals(waterConsumptionId)), trackChanges).SingleOrDefaultAsync();
    }

    public void CreateWaterConsumption(WaterConsumption waterConsumption)
    {
        Create(waterConsumption);
    }

    public void DeleteWaterConsumption(WaterConsumption waterConsumption)
    {
        Delete(waterConsumption);
    }

 
}