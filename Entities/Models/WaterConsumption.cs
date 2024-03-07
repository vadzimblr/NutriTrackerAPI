using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class WaterConsumption
{
    public Guid Id { get; set; }
    [Required]
    public DateTime ConsumptionTime { get; set; }
    [Range(10, Int32.MaxValue )]
    public int Amount { get; set; }
    public string UserId { get; set; }
}