using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Callback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync("Bungie");
        if (!authenticateResult.Succeeded)
        {
            return BadRequest("Error authenticating.");
        }

        // Extract user information from the authenticateResult
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value),
            new Claim(ClaimTypes.Name, authenticateResult.Principal.Identity.Name)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        // Create a local session (optional)
        HttpContext.Session.SetString("UserId", authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value);

        return Redirect("/");
    }
}
