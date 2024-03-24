namespace Entities.Exceptions;

public class ProductConsumptionNotFoundException:NotFoundException
{
    public ProductConsumptionNotFoundException(Guid productConsumptionId) : base($"Product consumption record with Guid: {productConsumptionId} not found")
    {
    }
}