using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Product
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Product name is required field.")]
    [MaxLength(64,ErrorMessage = "Maximum length for the Product name is 64 characters.")]
    public string ProductName { get; set; }
    [Required(ErrorMessage = "Calories is required field.")]
    [Range(1, Int32.MaxValue)]
    public double Calories { get; set; }  
    [MaxLength(256,ErrorMessage = "Maximum length for the Description is 256 characters.")]
    public string? Description { get; set; }
    [MaxLength(64,ErrorMessage = "Maximum length for the Brand is 64 characters.")]
    public string? Brand { get; set; }
    public double? ServingSize { get; set; }
    public double? Protein { get; set; }
    public double? Fat { get; set; }
    public double? Carbs { get; set; }
    public string? Image { get; set; }
    public string UserId { get; set; }
}