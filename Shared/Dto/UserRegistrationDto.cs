using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Shared.Dto;

public record UserRegistrationDto
{
    [Required(ErrorMessage = "First Name is required field.")]
    public string FirstName { get; init; }

    [Required(ErrorMessage = "Last Name is required field.")]
    public string LastName { get; init; }

    [Required(ErrorMessage = "Username is required field.")]
    public string Username { get; init; }

    [Required(ErrorMessage = "Password is required field.")]
    public string Password { get; init; }

    [Required(ErrorMessage = "Email is required field.")]
    public string Email { get; init; }

    public string? PhoneNumber { get; init; }
    public ICollection<string> Roles { get; init; }
}