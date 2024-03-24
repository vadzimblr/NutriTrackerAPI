using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;

public record ManipulationWaterConsumptionDto
{
    [Range(1, int.MaxValue)]
    public int Amount { get; init; }
};