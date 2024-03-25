namespace Contracts.ServiceContracts;

public interface IServiceManager
{
    ITokenService Token { get; }
    IAuthenticationService Authentication { get; }
    IProductService Product { get; }
    IWaterConsumptionService WaterConsumption { get; }
    IProductConsumptionService ProductConsumption { get; }
    ILimitService Limit { get; }
}