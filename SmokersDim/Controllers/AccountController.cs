using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class AccountController : Controller
{
    private readonly string _proxyUrl = "https://localhost:5099/api/proxy/data";
    private readonly string _baseUrl = "https://www.bungie.net/Platform";
    private readonly string _apiKey;
    private string _equipmentData;
    private readonly ILogger<AccountController> _logger;
    private readonly HttpClient _httpClient;
    
    public AccountController(ILogger<AccountController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _apiKey = configuration["Bungie:ApiKey"];
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("account/login")]
    public IActionResult Login()
    {
        return Redirect("https://localhost:5099/api/proxy/login");
    }

    [HttpGet("account/equipment")]
    public async Task<IActionResult> Equipment()
    {
        var result = await SetDataAsync();
        if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
        {
            return Content(_equipmentData, "application/json");
        }
        return result;
    }

    [HttpGet("account/test")]
    public IActionResult Test()
    {
        return Content(_equipmentData);
    }

    private async Task<IActionResult> SetDataAsync()
    {
        var membershipInfo = await GetMembershipInfo();
        try
        {
            if (IsEmptyString(ref membershipInfo, out var membershipInfoMessage))
                return StatusCode(500, membershipInfoMessage);

            var userData = JsonDocument.Parse(membershipInfo);
            var destinyMemberships = userData.RootElement.GetProperty("Response").GetProperty("destinyMemberships").EnumerateArray().FirstOrDefault();

            if (IsJsonValueKindUndefined(ref destinyMemberships, out var destinyMembershipsMessage))
                return StatusCode(500, destinyMembershipsMessage);
            
            var membershipId = destinyMemberships.GetProperty("membershipId").GetString();
            var membershipType = destinyMemberships.GetProperty("membershipType").GetInt32().ToString();

            var profileInfoResult = await GetProfile(membershipType, membershipId);
            if (IsEmptyString(ref profileInfoResult, out var profileEInfoMessage))
                return StatusCode(500, profileEInfoMessage);
            
            var profileInfo = profileInfoResult;
            var profileData = JsonDocument.Parse(profileInfo);
            var character = profileData.RootElement.GetProperty("Response").GetProperty("characters").GetProperty("data").EnumerateObject().FirstOrDefault().Value;

            if (IsJsonValueKindUndefined(ref character, out var characterMessage))
                return StatusCode(500, characterMessage);
            
            var characterId = character.GetProperty("characterId").ToString();
            var equipmentInfoResult = await GetEquipment(membershipType, membershipId, characterId);

            if (IsEmptyString(ref equipmentInfoResult, out var equipmentInfoMessage))
                return StatusCode(500, equipmentInfoMessage);

            var equipmentInfo = equipmentInfoResult;
            _equipmentData = JsonDocument.Parse(equipmentInfo).ToString();
            return Content(equipmentInfoResult, "application/json");
        }
        catch (JsonException jsonEx)
        {
            _logger.LogError(jsonEx, "Error parsing JSON response.");
            return StatusCode(500, "Error parsing JSON response.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    private async Task<string> GetMembershipInfo()
    {
        var url = $"{_baseUrl}/User/GetMembershipsForCurrentUser/";
        _logger.LogInformation("Requesting Membership Info from URL: {Url}", url);

        var response = await SendRequestAsync(url);

        return response.ToString();
    }

    private async Task<string> GetProfile(string membershipType, string membershipId)
    {
        var url = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/?components=200";
        _logger.LogInformation("Requesting Profile Info from URL: {Url}", url);

        return await SendRequestAsync(url);
    }

    private async Task<string> GetEquipment(string membershipType, string membershipId, string characterId)
    {
        var url = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/Character/{characterId}/?components=205";
        _logger.LogInformation("Requesting Equipment Info from URL: {Url}", url);

        return await SendRequestAsync(url);
    }

    private async Task<string> SendRequestAsync(string url)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, _proxyUrl);
        requestMessage.Headers.Add("MainApp-Url", url);

        var response = await _httpClient.SendAsync(requestMessage);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Request failed: {StatusCode} {ReasonPhrase} \n RequestUrl: {requestUrl}", response.StatusCode, response.ReasonPhrase, url);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    private bool IsEmptyString(ref string value, out string message)
    {
        var isEmpty = string.IsNullOrEmpty(value);
        message = $"{nameof(value)} is null or empty";

        if (isEmpty)
            _logger.LogError(message);

        return isEmpty;
    }
    
    private bool IsJsonValueKindUndefined(ref JsonElement value, out string message)
    {
        var isEmpty = value.ValueKind == JsonValueKind.Undefined;
        message = $"{nameof(value)} is undefined";

        if (isEmpty)
            _logger.LogError(message);

        return isEmpty;
    }
}
