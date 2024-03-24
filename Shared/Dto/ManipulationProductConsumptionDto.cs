using System.ComponentModel.DataAnnotations;
using Entities.Models;

namespace Shared.Dto;

public record ManipulationProductConsumptionDto
{
    [Range(1, int.MaxValue)]
    public int ConsumedCalories { get; set; }
    public Product? ConsumedProduct { get; set; }
}