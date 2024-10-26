using Melin.Server.Models;
using Melin.Server.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

public class TagController : ControllerBase
{
    private readonly TagContext _tagContext;
    private readonly UserManager<IdentityUser> _userManager;

    public TagController(TagContext tagContext, UserManager<IdentityUser> userManager)
    {
        _tagContext = tagContext;
        _userManager = userManager;
    }
    
    // POST: Single Tag creation
    [HttpPost("create-tag")]
    [Authorize]
    public async Task<ActionResult<Tag>> PostTag([FromBody] Tag tag)
    {
        CheckUserAuth();
        
        

        
        return Ok();
    }
    
    private IActionResult CheckUserAuth()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Ok();
        }

        return Unauthorized("User is not authenticated.");
    }
}