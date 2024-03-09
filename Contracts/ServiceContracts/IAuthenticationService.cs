using Microsoft.AspNetCore.Identity;
using Shared.Dto;

namespace Contracts.ServiceContracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserRegistrationDto user);
    Task<bool> ValidateUser(UserLoginDto user);
    
    

}