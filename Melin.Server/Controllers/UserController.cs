using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/auth/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet(Name = "GetUser")]
    public string Get()
    {
        return "John Doe";
    }
}