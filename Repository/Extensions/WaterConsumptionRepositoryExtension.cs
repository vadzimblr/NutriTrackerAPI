using Entities.Models;
namespace Repository.Extensions;

public static class WaterConsumptionRepositoryExtension
{
    public static IQueryable<WaterConsumption> FilterWaterConsumptionByAmount(this IQueryable<WaterConsumption> query, int minAmount, int maxAmount)
    {
       return query.Where(w => (w.Amount <= maxAmount && w.Amount >= minAmount));
    }
}