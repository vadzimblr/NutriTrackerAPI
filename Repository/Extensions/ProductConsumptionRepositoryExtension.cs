using Entities.Models;

namespace Repository.Extensions;

public static class ProductConsumptionRepositoryExtension
{
    public static IQueryable<ProductConsumption> FilterByConsumedCalories(this IQueryable<ProductConsumption> query,int  minCalories, int maxCalories)
    {
        return query.Where(p => (p.ConsumedCalories >= minCalories && p.ConsumedCalories <= maxCalories));
    }
}