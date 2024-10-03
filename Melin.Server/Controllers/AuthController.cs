using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/auth/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost(Name = "Login")]
    public string Login()
    {
        
        return "John Doe";
    }
}