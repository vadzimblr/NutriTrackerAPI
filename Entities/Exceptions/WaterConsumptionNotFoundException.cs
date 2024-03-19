namespace Entities.Exceptions;

public class WaterConsumptionNotFoundException:NotFoundException
{
    public WaterConsumptionNotFoundException(Guid waterConsumptionId) : base($"Water consumption record with Guid: {waterConsumptionId} not found")
    {
    }
}