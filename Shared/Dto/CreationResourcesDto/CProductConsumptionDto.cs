namespace Shared.Dto.CreationResourcesDto;

public record CProductConsumptionDto : ManipulationProductConsumptionDto
{
    public DateTime ConsumptionTime { get; set; } = DateTime.Now.Date;
}