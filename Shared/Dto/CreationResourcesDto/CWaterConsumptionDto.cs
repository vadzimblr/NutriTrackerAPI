namespace Shared.Dto.CreationResourcesDto;

public record CWaterConsumptionDto:ManipulationWaterConsumptionDto
{
    public DateTime ConsumptionTime = DateTime.Now.Date;
};