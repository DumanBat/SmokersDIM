using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Add logging namespace

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
        var membershipInfoResult = await GetMembershipInfo();
        if (membershipInfoResult is ObjectResult membershipInfoObject)
        {
            if (membershipInfoObject.StatusCode == 200)
            {
                try
                {
                    var membershipInfo = membershipInfoObject.Value.ToString();
                    if (string.IsNullOrEmpty(membershipInfo))
                    {
                        _logger.LogError("membershipInfo == null");
                    }
                    
                    _logger.LogWarning(membershipInfo);
                    var userData = JsonDocument.Parse(membershipInfo);
                    var destinyMemberships = userData.RootElement.GetProperty("Response").GetProperty("destinyMemberships").EnumerateArray().FirstOrDefault();

                    if (destinyMemberships.ValueKind != JsonValueKind.Undefined)
                    {
                        var membershipId = destinyMemberships.GetProperty("membershipId").GetString();
                        var membershipType = destinyMemberships.GetProperty("membershipType").GetString();

                        var profileInfoResult = await GetProfile(membershipType, membershipId);
                        if (profileInfoResult is ObjectResult profileInfoObject && profileInfoObject.StatusCode == 200)
                        {
                            var profileInfo = profileInfoObject.Value.ToString();
                            var profileData = JsonDocument.Parse(profileInfo);
                            var characters = profileData.RootElement.GetProperty("Response").GetProperty("characters").GetProperty("data").EnumerateArray().FirstOrDefault();

                            if (characters.ValueKind != JsonValueKind.Undefined)
                            {
                                var characterId = characters.GetProperty("characterId").ToString();
                                var equipmentInfoResult = await GetEquipment(membershipType, membershipId, characterId);

                                if (equipmentInfoResult is ObjectResult equipmentInfoObject && equipmentInfoObject.StatusCode == 200)
                                {
                                    var equipmentInfo = equipmentInfoObject.Value.ToString();
                                    _equipmentData = JsonDocument.Parse(equipmentInfo).ToString();
                                    return StatusCode(200, "finished");
                                }
                            }
                        }
                    }
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
            else
            {
                _logger.LogError("Error fetching SHIT: {StatusCode} {ReasonPhrase}", membershipInfoObject.StatusCode, membershipInfoObject.Value);
                return StatusCode((int)membershipInfoObject.StatusCode, membershipInfoObject.Value);
            }
        }
        return StatusCode(401, "Unauthorized or Invalid Response");
    }

    private async Task<IActionResult> GetMembershipInfo()
    {
        var url = $"{_baseUrl}/User/GetMembershipsForCurrentUser/";
        _logger.LogInformation("Requesting Membership Info from URL: {Url}", url);

        //var response = await SendRequestAsync(url);
        var response = await SendRequestAsync(url);
        if (response is ObjectResult objectResult && objectResult.StatusCode != 200)
        {
            _logger.LogError("Error fetching membership data: {StatusCode} {ReasonPhrase}", objectResult.StatusCode, objectResult.Value);
        }

        return response;
    }

    private async Task<IActionResult> GetProfile(string membershipType, string membershipId)
    {
        var url = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/?components=200";
        _logger.LogInformation("Requesting Profile Info from URL: {Url}", url);

        //return await SendRequestAsync(url);
        return await SendRequestAsync(url);
    }

    private async Task<IActionResult> GetEquipment(string membershipType, string membershipId, string characterId)
    {
        var url = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/Character/{characterId}/?components=205";
        _logger.LogInformation("Requesting Equipment Info from URL: {Url}", url);

        //return await SendRequestAsync(url);
        return await SendRequestAsync(url);
    }

    private async Task<IActionResult> SendRequestAsync(string url)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, _proxyUrl);
        requestMessage.Headers.Add("MainApp-Url", url);
        //requestMessage.Headers.Add("X-API-Key", _apiKey);

        var response = await _httpClient.SendAsync(requestMessage);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Request failed: {StatusCode} {ReasonPhrase} \n RequestUrl: {requestUrl}", response.StatusCode, response.ReasonPhrase, url);
            return StatusCode((int)response.StatusCode, "Request failed");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Received Response: {Response}", responseContent);

        return Content(responseContent);
    }
}
