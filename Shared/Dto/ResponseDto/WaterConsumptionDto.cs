namespace Shared.Dto.ResponseDto;

public record WaterConsumptionDto
{
    public Guid Id { get; init; }
    public DateTime ConsumptionTime { get; init; }
    public int Amount { get; init; }
    public string UserId { get; init; }
}