using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Limit
{
    public Guid Id { get; set; }
    public int MaxCalories { get; set; }
    public int WaterGoal { get; set; }
    public string UserId { get; set; }
}