using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Services;
using Shared.Dto;

namespace Tests;

public class TokenServiceTests
{
    private TokenService _tokenService;
    private Mock<IConfiguration> _configurationMock;
    
    private Mock<UserManager<User>> _userManagerMock;

    public TokenServiceTests()
    {
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _configurationMock = new Mock<IConfiguration>();
        _tokenService = new TokenService(_configurationMock.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task CreateToken_WithInvalidUsername_ShouldThrowException()
    {
        // Arrange
        var invalidUsername = "~~~$#$@#@#invalidUser";
        _userManagerMock.Setup(m => m.FindByNameAsync(invalidUsername)).ReturnsAsync((User)null);
        
        // Act
        var act = async () => await _tokenService.CreateToken(invalidUsername);
        
        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("User not found");
    }

    [Fact]
    public async Task CreateToken_WithValidUsername_ShouldReturnTokenDto()
    {
        //Arange
        var validUsername = "validUser";
        _userManagerMock.Setup(m => m.FindByNameAsync(validUsername))
            .ReturnsAsync(new User { UserName = validUsername });
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["validIssuer"]).Returns("validIssuer");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["validAudience"]).Returns("validAudience");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["expires"]).Returns("30");
        
        //Act
        var result = await _tokenService.CreateToken(validUsername);

        //Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
        result.RefreshToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Refresh_WithMalformedExpiredToken_ShouldThrowException()
    {
        //Arange
        var malformedExpiredToken = new TokenDto("Invalid", "Token");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["validAudience"]).Returns("validAudience");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["validIssuer"]).Returns("validIssuer");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["expires"]).Returns("30");
        
        //Act
        var act = async () => await _tokenService.Refresh(malformedExpiredToken);
        
        //Assert
        await act.Should().ThrowAsync<SecurityTokenMalformedException>();
    }
    [Fact]
    public async Task Refresh_WithInvalidAccessTokenInExpiredTokenDto_ShouldThrowException()
    {
        //Arange
        var malformedExpiredToken = 
            new TokenDto(
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdGVyc3VwZXIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImIwYzk1OWU3LTYxNGYtNGIyNy1hYjcwLTExZDQ5ZTI2NTllNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3MTEzODkwMTB9.yQDmZGo5RZfVDg8nRziibAa6RWX6i2ow8Hlwm3fgRsE"
            ,"LE31B6+k8wDDx880Iw/tdodXKYDIhe4DcYOlQpdb9LI=");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["validAudience"]).Returns("validAudience");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["validIssuer"]).Returns("validIssuer");
        _configurationMock.Setup(m => m.GetSection("JwtSettings")["expires"]).Returns("30");
       
        //Act
        var act = async () => await _tokenService.Refresh(malformedExpiredToken);
       
        //Assert
        await act.Should().ThrowAsync<SecurityTokenException>();
    }
}