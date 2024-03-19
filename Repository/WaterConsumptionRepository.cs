using Contracts.RepositoryContracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class WaterConsumptionRepository:RepositoryBase<WaterConsumption>,IWaterConsumptionRepository
{
    public WaterConsumptionRepository(RepositoryContext context) : base(context)
    {
    }
    public async Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsAsync(string userId,bool trackChanges)
    {
        return await FindByCondition(w => (w.UserId.Equals(userId)), trackChanges).ToListAsync();
    }

    public async Task<IEnumerable<WaterConsumption>> GetAllWaterConsumptionsByDateAsync(string userId,DateTime time, bool trackChanges)
    {
        return await FindByCondition(w => (w.UserId.Equals(userId) && w.ConsumptionTime.Date.Equals(time.Date)),trackChanges).ToListAsync();
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