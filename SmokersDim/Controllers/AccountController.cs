using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl = "https://www.bungie.net/Platform";

    public AccountController(IConfiguration configuration)
    {
        _configuration = configuration;
        _apiKey = configuration["Bungie:ApiKey"];
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Bungie");
    } 

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }

    [Authorize]
    [HttpGet("character")]
    public async Task<IActionResult> GetCharacterInfo()
    {
        var membershipDataEndpoint = $"{_baseUrl}/User/GetMembershipsForCurrentUser/";
        var membershipResponse = await _httpClient.GetAsync(membershipDataEndpoint);

        if (!membershipResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)membershipResponse.StatusCode, "Error fetching membership data");
        }

        var userData = JsonDocument.Parse(await membershipResponse.Content.ReadAsStringAsync());
        var userId = userData.RootElement.GetProperty("Response").GetProperty("bungieNetUser").GetProperty("membershipId").GetString();
        var membershipType = userData.RootElement.GetProperty("Response").GetProperty("bungieNetUser").GetProperty("membershipType").GetString();
        var characterId = "0";

        var endpoint = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{userId}/Character/{characterId}/?components=200";
        var response = await _httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        var characterInfo = await response.Content.ReadAsStringAsync();

        var filePath = Path.Combine("G:\\Backend\\test", "characterInfo.json");
        await System.IO.File.WriteAllTextAsync(filePath, characterInfo);
        
        return Content(characterInfo, "application/json");
    }
}
