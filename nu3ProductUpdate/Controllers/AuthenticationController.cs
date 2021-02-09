using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using nu3ProductUpdate.Classes.Extensions;
using System.Net;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromForm] string provider)
    {
        if (string.IsNullOrWhiteSpace(provider))
        {
            return BadRequest();
        }

        if (!await HttpContext.IsProviderSupportedAsync(provider))
        {
            return BadRequest();
        }

        return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
    }

    [HttpGet("signout")]
    [HttpPost("signout")]
    public async Task<IActionResult> SignOutCurrentUser()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }

    [HttpPost("check")]
    public IActionResult Check()
    {
        if (User != null && User.Identity.IsAuthenticated)
        {
            return Ok(new { User.Identity.Name });
        }

        return Problem(detail: "NotAuthorized", statusCode: (int)HttpStatusCode.Unauthorized);
    }
}