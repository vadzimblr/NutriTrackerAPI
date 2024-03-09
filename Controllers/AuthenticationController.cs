using Contracts.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace Controllers;
[ApiController]
[Route("api/authentication")]
public class AuthenticationController:ControllerBase
{
    private readonly IServiceManager _service; 
    public AuthenticationController(IServiceManager service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userDto)
    {
        var result = await _service.Authentication.RegisterUser(userDto);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginDto userDto)
    {
        if (!await _service.Authentication.ValidateUser(userDto))
        {
            return Unauthorized();
        }

        var tokenDto = await _service.Token.CreateToken(userDto.Username);
        return Ok(tokenDto);
    }

    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
       var result = await _service.Token.Refresh(tokenDto);
       return Ok(result);
    }
    
    [HttpPost("revoke"),Authorize]
    public async Task<IActionResult> Revoke()
    {
        await _service.Token.RevokeRefreshToken(User.Identity.Name);
        return NoContent();
    }
    
}