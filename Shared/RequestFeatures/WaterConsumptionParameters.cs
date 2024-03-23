namespace Shared.RequestFeatures;

public class WaterConsumptionParameters:RequestParameters
{
    public int MinAmount { get; set; }
    public int MaxAmount { get; set; } = Int32.MaxValue;
    public bool ValidateAmountRange => MinAmount <= MaxAmount;
}