using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ProductConsumption
{
    public Guid Id { get; set; }
    [Required]
    public DateTime ConsumptionTime { get; set; }
    [Range(1, Int32.MaxValue )]
    public int ConsumedCalories { get; set; }
    [ForeignKey("ConsumedProductId")]
    public Guid? ConsumedProductId { get; set; }
    public Product? ConsumedProduct { get; set; }
    [ForeignKey("UserId")]
    public string UserId { get; set; }
}