using Entities.Models;

namespace Shared.Dto.ResponseDto;

public class ProductConsumptionDto
{
    public Guid Id { get; set; }
    public DateTime ConsumptionTime { get; set; }
    public int ConsumedCalories { get; set; }
    public Product? ConsumedProduct { get; set; }
    public string UserId { get; set; }
}