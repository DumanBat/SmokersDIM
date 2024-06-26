using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Immutable;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
	public const string MEMBERSHIP_TYPE_CACHE_KEY = "MembershipType";
	public const string MEMBERSHIP_ID_CACHE_KEY = "MembershipId";
	public const string PROFILE_DATA_CACHE_KEY = "ProfileData";
	public const string CHARACTER_IDS_CACHE_KEY = "CharactersIDsArray";
	private readonly IBungieApiService _bungieApiService;
	private readonly IEquipmentService _equipmentService;
	private readonly IDestinyManifectService _manifestService;
	private readonly ILogger<AccountController> _logger;
	private readonly IHttpContextAccessor _httpContextAccessor;


	public AccountController(IBungieApiService bungieApiService, IEquipmentService equipmentService, IDestinyManifectService destinyManifectService, ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
	{
		_bungieApiService = bungieApiService;
		_manifestService = destinyManifectService;
		_httpContextAccessor = httpContextAccessor;
		_equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
		_logger = logger;
	}

	[HttpGet("login")]
	public IActionResult Login()
	{
		return Redirect(UrlConstants.LoginRedirectUrl);
	}

	[HttpGet("auth-callback")]
	public async Task<IActionResult> AuthCallback()
	{
		try
		{
			await SetAccountData();
			
			var session = _httpContextAccessor.HttpContext.Session;
			var membershipType = session.GetString(MEMBERSHIP_TYPE_CACHE_KEY);
			var membershipId = session.GetString(MEMBERSHIP_ID_CACHE_KEY);
			var characterIds = session.GetString(CHARACTER_IDS_CACHE_KEY);			
			
			await _equipmentService.SetEquipmentDataAsync(membershipType, membershipId, characterIds);
			//await _manifestService.UpdateInventoryItemsManifestAsync();
			return Redirect(UrlConstants.AuthCallbackRedirectUrl);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to execute OnAuth callback");
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("show-equipment")]
	public IActionResult ShowEquipment()
	{
		var session = _httpContextAccessor.HttpContext.Session;
		var equipmentData = session.GetString(EquipmentService.CHARACTER_DATA_CACHE_KEY);

		if (!string.IsNullOrEmpty(equipmentData))
			return Content(equipmentData, AppConstants.JsonContentType);        
		else
		{
			_logger.LogError($"{EquipmentService.CHARACTER_DATA_CACHE_KEY} is empty");
			return StatusCode(500, $"{EquipmentService.CHARACTER_DATA_CACHE_KEY} is empty");
		}
	}
	
	[HttpGet("show-item-manifest")]
	public IActionResult ShowItemManifest()
	{
		var session = _httpContextAccessor.HttpContext.Session;
		var itemManifest = session.GetString(DestinyManifectService.ITEM_MANIFEST_CACHE_KEY);

		if (!string.IsNullOrEmpty(itemManifest))
			return Content(itemManifest, AppConstants.JsonContentType);        
		else
		{
			_logger.LogError($"{DestinyManifectService.ITEM_MANIFEST_CACHE_KEY} is empty");
			return StatusCode(500, $"{DestinyManifectService.ITEM_MANIFEST_CACHE_KEY} is empty");
		}
	}
	
	private async Task<IActionResult> SetAccountData()
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
		var charactersData = profileData.RootElement.GetProperty("Response").GetProperty("characters").GetProperty("data");
		var characters = charactersData.EnumerateObject().ToImmutableArray();
		
		var characterIdsString = "";
		
		for (int i = 0; i < characters.Length; i++)
		{
			var id = characters[i].Value.GetProperty("characterId").GetString();
			characterIdsString += $"{id}/";
		}		
				
		session.SetString(MEMBERSHIP_ID_CACHE_KEY, membershipId);
		session.SetString(MEMBERSHIP_TYPE_CACHE_KEY, membershipType);
		session.SetString(PROFILE_DATA_CACHE_KEY, profileInfo);
		session.SetString(CHARACTER_IDS_CACHE_KEY, characterIdsString);
		
		return StatusCode(200);
	}
}
