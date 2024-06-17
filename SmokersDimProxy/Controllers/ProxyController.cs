using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProxyController : ControllerBase
{
    private readonly string _apiKey;
    private readonly string _baseUrl = "https://www.bungie.net/Platform";
    private readonly HttpClient _httpClient;

    public ProxyController(IConfiguration configuration)
    {
        _apiKey = configuration["Bungie:ApiKey"];
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = "https://localhost:5281/callback"
        };
        return Challenge(authenticationProperties, "Bungie");
    }

    [Authorize]
    [HttpGet("{*url}")]
    public async Task<IActionResult> Get(string url)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://www.bungie.net/{url}");

        var response = await _httpClient.SendAsync(requestMessage);
        var content = await response.Content.ReadAsStringAsync();

        // Set the Access-Control-Allow-Origin header
        HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", "https://localhost:5281");

        return Content(content, response.Content.Headers.ContentType.ToString());
    }
    
    [Authorize]
    [HttpGet("character")]
    public async Task<IActionResult> GetCharacterInfo()
    {
        // Retrieve the access token from the authentication ticket
        var authenticateResult = await HttpContext.AuthenticateAsync("Bungie");
        if (!authenticateResult.Succeeded || !authenticateResult.Properties.Items.TryGetValue(".Token.access_token", out var accessToken))
        {
            return Unauthorized("Unable to retrieve access token.");
        }

        // Set up the request message with the access token
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/User/GetMembershipsForCurrentUser/");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        requestMessage.Headers.Add("X-API-Key", _apiKey);

        var membershipResponse = await _httpClient.SendAsync(requestMessage);
        if (!membershipResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)membershipResponse.StatusCode, "Error fetching membership data");
        }

        var userData = JsonDocument.Parse(await membershipResponse.Content.ReadAsStringAsync());
        var userId = userData.RootElement.GetProperty("Response").GetProperty("bungieNetUser").GetProperty("membershipId").GetString();
        var membershipType = userData.RootElement.GetProperty("Response").GetProperty("bungieNetUser").GetProperty("membershipType").GetString();
        var characterId = "0";

        // Request for character information using the user's membership details
        var endpoint = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{userId}/Character/{characterId}/?components=200";
        requestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        requestMessage.Headers.Add("X-API-Key", _apiKey);

        var response = await _httpClient.SendAsync(requestMessage);
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        var characterInfo = await response.Content.ReadAsStringAsync();

        // Optional: Save the character information to a file for debugging
        var filePath = Path.Combine("G:\\Backend\\test", "characterInfo.json");
        await System.IO.File.WriteAllTextAsync(filePath, characterInfo);

        // Set the Access-Control-Allow-Origin header
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:5281");
        HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
        
        return Content(characterInfo, "application/json");
    }
}


