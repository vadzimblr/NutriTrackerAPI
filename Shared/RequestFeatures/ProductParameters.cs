namespace Shared.RequestFeatures;

public class ProductParameters:RequestParameters
{
    public double MinFat { get; set; }
    public double MaxFat { get; set; } = double.MaxValue;
    public bool ValidateFatRange => MaxFat > MinFat;
    public double MinProtein { get; set; }
    public double MaxProtein { get; set; } = double.MaxValue;
    public bool ValidateProteinRange => MaxProtein > MinProtein;
    public double MinCarbs{ get; set; }
    public double MaxCarbs { get; set; } = double.MaxValue;
    public bool ValidateCarbsRange => MaxCarbs > MinCarbs;  
    public double MinCalories{ get; set; }
    public double MaxCalories{ get; set; } = double.MaxValue;
    public bool ValidateCaloriesRange => MaxCalories > MinCalories;
    public double MinServingSize{ get; set; }
    public double MaxServingSize{ get; set; } = double.MaxValue;
    public bool ValidateServingSizeRange => MaxServingSize > MinServingSize;
    public string? searchTerm { get; set; }
}