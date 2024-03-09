using AutoMapper;
using Contracts.ServiceContracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Services;

public class ServiceManager:IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<ITokenService> _tokenService;
    public ServiceManager(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
    {
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, mapper));
        _tokenService = new Lazy<ITokenService>(() => new TokenService(configuration,userManager));
    }

    public IAuthenticationService Authentication => _authenticationService.Value;
    public ITokenService Token => _tokenService.Value;
}