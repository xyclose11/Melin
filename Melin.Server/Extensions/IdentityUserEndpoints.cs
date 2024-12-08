using Melin.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Task = Melin.Server.Models.Task;

namespace Melin.Server.Extensions;

public static class IdentityUserEndpoints
{
    [AllowAnonymous]
    private static async Task<IResult> CreateUser(
        UserManager<IdentityUser> userManager,
        [FromBody] UserCreationModel userCreationModel)
    {
        IdentityUser user = new IdentityUser()
        {
            UserName = userCreationModel.Email,
            Email = userCreationModel.Email,
        };

        var result = await userManager.CreateAsync(user, userCreationModel.Password);

        await userManager.AddToRoleAsync(user, userCreationModel.Role);

        if (!result.Succeeded)
        {
            return Results.BadRequest("USER CREATION FAILED");
        }
        
        return Results.Ok(result);
    }

    [AllowAnonymous]
    private static async Task<IResult> SignIn(
        UserManager<IdentityUser> userManager,
        [FromBody] LoginModel loginModel)
    {
        var user = await userManager.FindByEmailAsync(loginModel.Email);

        if (user == null)
        {
            return Results.BadRequest("UNABLE TO SIGN IN");
        }
        
        if (await userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            return Results.BadRequest("UNABLE TO SIGN IN");
        }

        var roles = await userManager.GetRolesAsync(user);
        return null;
    }
}