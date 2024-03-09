using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;
[ApiController]
[Route("")]
public class HelloContoller:ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Greetings()
    {
        return Ok("Hello,user!");
    }
}