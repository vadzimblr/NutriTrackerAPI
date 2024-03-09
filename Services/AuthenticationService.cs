using AutoMapper;
using Contracts.ServiceContracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dto;

namespace Services;

public class AuthenticationService:IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public AuthenticationService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<IdentityResult> RegisterUser(UserRegistrationDto user)
    {
        var userEntity = _mapper.Map<User>(user);
        var result = await _userManager.CreateAsync(userEntity,user.Password);
        if (result.Succeeded)
            await _userManager.AddToRolesAsync(userEntity, user.Roles);
        return result;
    }

    public async Task<bool> ValidateUser(UserLoginDto user)
    {
        var userEntity = await _userManager.FindByNameAsync(user.Username);
        bool result = (userEntity != null && await _userManager.CheckPasswordAsync(userEntity, user.Password));
        return result;
    }
}