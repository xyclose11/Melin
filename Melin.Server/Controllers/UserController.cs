using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/auth/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpGet(Name = "GetUser")]
    public async Task<IdentityUser> GetUser(string userId)
    {
        try
        {
            var u = await _userManager.FindByIdAsync(userId);
            
            if (u != null)
            {
                return u;
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }
}