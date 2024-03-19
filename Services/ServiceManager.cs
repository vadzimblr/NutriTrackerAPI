using AutoMapper;
using Contracts;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.Dto.ResponseDto;

namespace Services;

public class ServiceManager:IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<ITokenService> _tokenService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IWaterConsumptionService> _waterConsumptionService;
    public ServiceManager(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IRepositoryManager repositoryManager
    ,IDataShaper<ProductDto> dataShaper)
    {
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, mapper));
        _tokenService = new Lazy<ITokenService>(() => new TokenService(configuration,userManager));
        _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper, userManager,dataShaper));
        _waterConsumptionService =
            new Lazy<IWaterConsumptionService>(() => new WaterConsumptionService(mapper, repositoryManager));
    }
    
    public IAuthenticationService Authentication => _authenticationService.Value;
    public ITokenService Token => _tokenService.Value;
    public IProductService Product => _productService.Value;
    public IWaterConsumptionService WaterConsumption => _waterConsumptionService.Value;
}