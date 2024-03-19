using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Contracts.ServiceContracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Dto;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Services;

public class TokenService:ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    
    public async Task<TokenDto> CreateToken(string username)
    {
        var userEntity = await _userManager.FindByNameAsync(username);
        if (userEntity == null)
        {
            throw new InvalidOperationException("User not found");
        }
        var claims = await GetClaims(userEntity);
        var signingCredentials = GetSigningCredentials();
        return await IssueTokens(claims,signingCredentials,userEntity);
    }

    public async Task<TokenDto> Refresh(TokenDto expiredToken)
    {
        var principalClaims = GetClaimsFromExpiredToken(expiredToken.AccessToken);
        if (IsAccessTokenValid(principalClaims))
        {
            return expiredToken;
        }
        var userEntity = await _userManager.FindByNameAsync(principalClaims.Identity.Name);
        if (userEntity == null || userEntity.RefreshToken != expiredToken.RefreshToken ||
            userEntity.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new SecurityTokenException("Invalid token credentials");
        }
        var signingCredentials = GetSigningCredentials();
        return await IssueTokens(principalClaims.Claims.ToList(), signingCredentials, userEntity);
    }

    private bool IsAccessTokenValid(ClaimsPrincipal claimsPrincipal)
    {
        var expired = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Exp);
        if (expired == null)
        {
            return false;
        }
        var expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expired.Value)).UtcDateTime;
        return expiryDateTime > DateTime.UtcNow;
    }
    public async Task RevokeRefreshToken(string username)
    {
        var userEntity = await _userManager.FindByNameAsync(username);
        if (userEntity == null)
        {
            throw new InvalidOperationException("User not found");
        }
        userEntity.RefreshToken = null;
        userEntity.RefreshTokenExpiryTime = null;
        await _userManager.UpdateAsync(userEntity);
    }

    private JwtSecurityToken GenerateJwtSecurityToken(List<Claim> claims, SigningCredentials credentials)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var token = new JwtSecurityToken(
                issuer:jwtSettings["validIssuer"],
                audience:jwtSettings["validAudience"],
                claims:claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials:credentials
            );
        return token;
    }

    private async Task<List<Claim>> GetClaims(User userEntity)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userEntity.UserName),
            new Claim(ClaimTypes.NameIdentifier,userEntity.Id)
        };
        var roles = await _userManager.GetRolesAsync(userEntity);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role));
        }
        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var encodedKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
        var key = new SymmetricSecurityKey(encodedKey);
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }

    private string GenerateRefreshToken()
    {
        byte[] key = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
            return Convert.ToBase64String(key);
        }
    }

    private ClaimsPrincipal GetClaimsFromExpiredToken(string accessToken)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Environment.GetEnvironmentVariable("SECRET");
        var validateParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = jwtSettings["validAudience"],
            ValidIssuer = jwtSettings["validIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var claimsPrincipal = tokenHandler.ValidateToken(accessToken, validateParameters, out securityToken);
        var jwtValidatedToken = securityToken as JwtSecurityToken;
        if (jwtValidatedToken == null || !jwtValidatedToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return claimsPrincipal;
    }
    private async Task<TokenDto> IssueTokens(List<Claim> claims, SigningCredentials signingCredentials, User userEntity)
    {
        var token = GenerateJwtSecurityToken(claims,signingCredentials);
        var refreshToken = GenerateRefreshToken();
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        userEntity.RefreshToken = refreshToken;
        userEntity.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(userEntity);
        var result = new TokenDto(accessToken,refreshToken);
        return result;
    }
}