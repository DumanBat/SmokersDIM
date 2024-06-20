using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IBungieApiService _bungieApiService;
    private string _equipmentData;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IBungieApiService bungieApiService, ILogger<AccountController> logger)
    {
        _bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
        _logger = logger;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Redirect("https://localhost:5099/api/proxy/login");
    }

    [HttpGet("equipment")]
    public async Task<IActionResult> Equipment()
    {
        try
        {
            var result = await SetDataAsync();
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return Content(_equipmentData, "application/json");
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    private async Task<IActionResult> SetDataAsync()
    {
        var membershipInfo = await _bungieApiService.GetMembershipInfo();
        if (IsEmptyString(membershipInfo, out var membershipInfoMessage))
        {
            _logger.LogError(membershipInfoMessage);
            return StatusCode(500, membershipInfoMessage);
        }

        var userData = JsonDocument.Parse(membershipInfo);
        var destinyMemberships = userData.RootElement.GetProperty("Response").GetProperty("destinyMemberships").EnumerateArray().FirstOrDefault();
        if (IsJsonValueKindUndefined(destinyMemberships, out var destinyMembershipsMessage))
        {
            _logger.LogError(destinyMembershipsMessage);
            return StatusCode(500, destinyMembershipsMessage);
        }

        var membershipId = destinyMemberships.GetProperty("membershipId").GetString();
        var membershipType = destinyMemberships.GetProperty("membershipType").GetInt32().ToString();

        var profileInfo = await _bungieApiService.GetProfile(membershipType, membershipId);
        if (IsEmptyString(profileInfo, out var profileInfoMessage))
        {
            _logger.LogError(profileInfoMessage);
            return StatusCode(500, profileInfoMessage);
        }

        var profileData = JsonDocument.Parse(profileInfo);
        var character = profileData.RootElement.GetProperty("Response").GetProperty("characters").GetProperty("data").EnumerateObject().FirstOrDefault().Value;
        if (IsJsonValueKindUndefined(character, out var characterMessage))
        {
            _logger.LogError(characterMessage);
            return StatusCode(500, characterMessage);
        }

        var characterId = character.GetProperty("characterId").ToString();
        var equipmentInfo = await _bungieApiService.GetEquipment(membershipType, membershipId, characterId);
        if (IsEmptyString(equipmentInfo, out var equipmentInfoMessage))
        {
            _logger.LogError(equipmentInfoMessage);
            return StatusCode(500, equipmentInfoMessage);
        }

        _equipmentData = equipmentInfo;
        return Content(equipmentInfo, "application/json");
    }

    private bool IsEmptyString(string value, out string message)
    {
        if (string.IsNullOrEmpty(value))
        {
            message = $"{nameof(value)} is null or empty";
            return true;
        }

        message = null;
        return false;
    }

    private bool IsJsonValueKindUndefined(JsonElement value, out string message)
    {
        if (value.ValueKind == JsonValueKind.Undefined)
        {
            message = $"{nameof(value)} is undefined";
            return true;
        }

        message = null;
        return false;
    }
}
