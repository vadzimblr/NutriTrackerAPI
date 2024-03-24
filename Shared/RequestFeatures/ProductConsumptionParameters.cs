namespace Shared.RequestFeatures;

public class ProductConsumptionParameters:RequestParameters
{
    public int MinConsumedCalories { get; set; }
    public int MaxConsumedCalories { get; set; } = int.MaxValue;
    public bool ValidateConsumedCaloriesRange => MinConsumedCalories <= MaxConsumedCalories;
}