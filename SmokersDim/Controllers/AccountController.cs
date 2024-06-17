using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    [HttpGet("account/login")]
    public IActionResult Login()
    {
        return Redirect("https://localhost:5099/api/proxy/login");
    }

    [HttpGet("account/character")]
    public IActionResult Character()
    {
        return Redirect("https://localhost:5099/api/proxy/character");
    }
}