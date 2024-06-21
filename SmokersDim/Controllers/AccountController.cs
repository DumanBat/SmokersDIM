using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private const string EQUIPMENT_DATA_CACHE_KEY = "EquipmentData";
    private readonly IBungieApiService _bungieApiService;
    private readonly ILogger<AccountController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountController(IBungieApiService bungieApiService, ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Redirect(UrlConstants.LoginRedirectUrl);
    }

    [HttpGet("auth-callback")]
    public async Task<IActionResult> AuthCallback()
    {
        await SetEquipmentDataAsync();
        return Redirect(UrlConstants.AuthCallbackRedirectUrl);
    }

    [HttpGet("show-equipment")]
    public async Task<IActionResult> ShowEquipment()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var equipmentData = session.GetString(EQUIPMENT_DATA_CACHE_KEY);

        if (!string.IsNullOrEmpty(equipmentData))
            return Content(equipmentData, AppConstants.JsonContentType);        
        else
        {
            _logger.LogError($"{EQUIPMENT_DATA_CACHE_KEY} is empty");
            return StatusCode(500, $"{EQUIPMENT_DATA_CACHE_KEY} is empty");
        }
    }

    private async Task<IActionResult> SetEquipmentDataAsync()
    {
        var session = _httpContextAccessor.HttpContext.Session;

        var membershipInfo = await _bungieApiService.GetMembershipInfo();
        if (ValidationHelpers.IsEmptyString(membershipInfo, out var membershipInfoMessage))
        {
            _logger.LogError(membershipInfoMessage);
            return StatusCode(500, membershipInfoMessage);
        }

        var userData = JsonDocument.Parse(membershipInfo);
        var destinyMemberships = userData.RootElement.GetProperty("Response").GetProperty("destinyMemberships").EnumerateArray().FirstOrDefault();
        if (ValidationHelpers.IsJsonValueKindUndefined(destinyMemberships, out var destinyMembershipsMessage))
        {
            _logger.LogError(destinyMembershipsMessage);
            return StatusCode(500, destinyMembershipsMessage);
        }

        var membershipId = destinyMemberships.GetProperty("membershipId").GetString();
        var membershipType = destinyMemberships.GetProperty("membershipType").GetInt32().ToString();

        var profileInfo = await _bungieApiService.GetProfile(membershipType, membershipId);
        if (ValidationHelpers.IsEmptyString(profileInfo, out var profileInfoMessage))
        {
            _logger.LogError(profileInfoMessage);
            return StatusCode(500, profileInfoMessage);
        }

        var profileData = JsonDocument.Parse(profileInfo);
        var character = profileData.RootElement.GetProperty("Response").GetProperty("characters").GetProperty("data").EnumerateObject().FirstOrDefault().Value;
        if (ValidationHelpers.IsJsonValueKindUndefined(character, out var characterMessage))
        {
            _logger.LogError(characterMessage);
            return StatusCode(500, characterMessage);
        }

        var characterId = character.GetProperty("characterId").ToString();
        var equipmentInfo = await _bungieApiService.GetEquipment(membershipType, membershipId, characterId);
        if (ValidationHelpers.IsEmptyString(equipmentInfo, out var equipmentInfoMessage))
        {
            _logger.LogError(equipmentInfoMessage);
            return StatusCode(500, equipmentInfoMessage);
        }
        
        session.SetString(EQUIPMENT_DATA_CACHE_KEY, equipmentInfo);
        return Content(equipmentInfo, AppConstants.JsonContentType);
    }
}
