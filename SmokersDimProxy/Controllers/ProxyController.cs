using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProxyController : ControllerBase
{
    private readonly string _apiKey;
    private readonly string _baseUrl = "https://www.bungie.net/Platform";
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;
    private readonly ILogger<ProxyController> _logger;

    public ProxyController(IConfiguration configuration, ITokenService tokenService, ILogger<ProxyController> logger)
    {
        _apiKey = configuration["Bungie:ApiKey"];
        _tokenService = tokenService;
        _logger = logger;
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
    [HttpGet("equipment")]
    public async Task<IActionResult> GetEquipment()
    {
        var accessToken = _tokenService.AccessToken;
        _logger.LogInformation($"Getting equipment. Access token - {accessToken}");
        if (string.IsNullOrEmpty(accessToken))
        {
            _logger.LogInformation($"Access token is missing.");
            return Unauthorized("Access token is missing.");
        }

        var membershipInfo = await GetMembershipInfo(accessToken);
        if (membershipInfo == null)
        {
            _logger.LogInformation($"membershipInfo == null");
            return StatusCode(401);
        }
        var userData = JsonDocument.Parse(membershipInfo.ToString());
        var destinyMembership = userData.RootElement.GetProperty("Response").GetProperty("destinyMemberships").EnumerateArray().FirstOrDefault();
        var membershipId = destinyMembership.GetProperty("membershipId").GetString();
        var membershipType = destinyMembership.GetProperty("membershipType").GetString();

        var profileInfo = await GetProfile(membershipType, membershipId, accessToken);
        if (profileInfo == null)
        {
            _logger.LogInformation($"profileInfo == null");
            return StatusCode(401);
        }
        var profileData = JsonDocument.Parse(profileInfo.ToString());
        var character = userData.RootElement.GetProperty("Response").GetProperty("characters").GetProperty("data").EnumerateArray().FirstOrDefault();
        var characterId = character.GetProperty("characterId").ToString();

        var equipmentInfo = await GetEquipment(membershipType, membershipId, characterId, accessToken);
        if (equipmentInfo == null)
        {
            _logger.LogInformation($"equipmentInfo == null");
            return StatusCode(401);
        }
        var equipmentData = JsonDocument.Parse(profileInfo.ToString()).ToString();
        var mainAppUrl = "https://localhost:5281/receive-equipment";
        var content = new StringContent(equipmentData, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync(mainAppUrl, content);
        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to send equipment data to the Main app");
            return StatusCode(500, "Failed to send data to the Main app");
        }

        return Ok(equipmentData);
    }

    private async Task<IActionResult> GetMembershipInfo(string accessToken)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/User/GetMembershipsForCurrentUser/");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var membershipResponse = await _httpClient.SendAsync(requestMessage);
        if (!membershipResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)membershipResponse.StatusCode, "Error fetching membership data");
        }
        var response = await membershipResponse.Content.ReadAsStringAsync();
        return Content(response);
    } 

    private async Task<IActionResult> GetProfile(string membershipType, string membershipId, string accessToken)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/?components=200");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var membershipResponse = await _httpClient.SendAsync(requestMessage);
        if (!membershipResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)membershipResponse.StatusCode, "Error fetching membership data");
        }
        var response = await membershipResponse.Content.ReadAsStringAsync();
        return Content(response);
    } 

    private async Task<IActionResult> GetEquipment(string membershipType, string membershipId, string characterId, string accessToken)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/Character/{characterId}/?components=205");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var membershipResponse = await _httpClient.SendAsync(requestMessage);
        if (!membershipResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)membershipResponse.StatusCode, "Error fetching membership data");
        }
        var response = await membershipResponse.Content.ReadAsStringAsync();
        return Content(response);
    } 
}


