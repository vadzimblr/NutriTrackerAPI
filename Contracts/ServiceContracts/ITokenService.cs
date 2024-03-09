using System.Security.Claims;
using Entities.Models;
using Shared.Dto;

namespace Contracts.ServiceContracts;

public interface ITokenService
{
    Task<TokenDto> CreateToken(string username);
    Task<TokenDto> Refresh(TokenDto expiredToken);
    Task RevokeRefreshToken(string username);
}