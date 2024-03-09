using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;

public record UserLoginDto
{
    [Required(ErrorMessage = "Username is required field.")]
    public string Username { get; init; }
    [Required(ErrorMessage = "Password is required field.")]
    public string Password { get; init; }
}