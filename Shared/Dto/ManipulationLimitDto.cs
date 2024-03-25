using System.ComponentModel.DataAnnotations;

namespace Shared.Dto.UpdateResourcesDto;

public record ManipulationLimitDto
{
    [Range(1, int.MaxValue)]
    public int MaxCalories { get; init; }
    [Range(1, int.MaxValue)]
    public int WaterGoal { get; init; }
};