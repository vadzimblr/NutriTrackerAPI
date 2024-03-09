namespace Contracts.ServiceContracts;

public interface IServiceManager
{
    ITokenService Token { get; }
    IAuthenticationService Authentication { get; }
}