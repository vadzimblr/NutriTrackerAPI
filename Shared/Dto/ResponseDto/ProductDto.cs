namespace Shared.Dto.ResponseDto;

public class ProductDto
{
    public Guid Id { get; init; }
    public string ProductName { get; init; }
    public double Calories { get; init; }  
    public string? Description { get; init; }
    public string? Brand { get; init; }
    public double? ServingSize { get; init; }
    public double? Protein { get; init; }
    public double? Fat { get; init; }
    public double? Carbs { get; init; }
    public string? Image { get; init; }
    public string UserId { get; init; }
}
