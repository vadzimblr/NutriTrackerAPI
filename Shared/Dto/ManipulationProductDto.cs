using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;

public record ManipulationProductDto
{
    [Required(ErrorMessage = "Product name is required field.")]
    [MaxLength(64,ErrorMessage = "Maximum length for the Product name is 64 characters.")]
    public string ProductName { get; init; }
    [Required(ErrorMessage = "Calories is required field.")]
    [Range(1, Int32.MaxValue)]
    public double Calories { get; init; }  
    [MaxLength(256,ErrorMessage = "Maximum length for the Description is 256 characters.")]
    public string? Description { get; init; }
    [MaxLength(64,ErrorMessage = "Maximum length for the Brand is 64 characters.")]
    public string? Brand { get; init; }
    public double? ServingSize { get; init; }
    public double? Protein { get; init; }
    public double? Fat { get; init; }
    public double? Carbs { get; init; }
    public string? Image { get; init; }
}