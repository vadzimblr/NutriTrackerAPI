using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class User:IdentityUser
{
    [MaxLength(64,ErrorMessage = "Maximum length for the First Name is 64 characters.")]
    public string FirstName { get; set; }
    [MaxLength(64,ErrorMessage = "Maximum length for the Last Name is 64 characters.")]
    public string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}