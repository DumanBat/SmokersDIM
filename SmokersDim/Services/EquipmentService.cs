using System.Text.Json;

public interface IEquipmentService
{
    Task<string> SetEquipmentDataAsync();
}

public class EquipmentService : IEquipmentService
{
    public const string EQUIPMENT_DATA_CACHE_KEY = "EquipmentData";
    private readonly IBungieApiService _bungieApiService;
    private readonly ILogger<EquipmentService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EquipmentService(IBungieApiService bungieApiService, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SetEquipmentDataAsync()
    {
        var session = _httpContextAccessor.HttpContext.Session;

        var membershipInfo = await _bungieApiService.GetMembershipInfo();
        if (ValidationHelpers.IsEmptyString(membershipInfo, out var membershipInfoMessage))
        {
            _logger.LogError(membershipInfoMessage);
            throw new Exception(membershipInfoMessage);
        }

        var userData = JsonDocument.Parse(membershipInfo);
        var destinyMemberships = userData.RootElement.GetProperty("Response").GetProperty("destinyMemberships").EnumerateArray().FirstOrDefault();
        if (ValidationHelpers.IsJsonValueKindUndefined(destinyMemberships, out var destinyMembershipsMessage))
        {
            _logger.LogError(destinyMembershipsMessage);
            throw new Exception(destinyMembershipsMessage);
        }

        var membershipId = destinyMemberships.GetProperty("membershipId").GetString();
        var membershipType = destinyMemberships.GetProperty("membershipType").GetInt32().ToString();

        var profileInfo = await _bungieApiService.GetProfile(membershipType, membershipId);
        if (ValidationHelpers.IsEmptyString(profileInfo, out var profileInfoMessage))
        {
            _logger.LogError(profileInfoMessage);
            throw new Exception(profileInfoMessage);
        }

        var profileData = JsonDocument.Parse(profileInfo);
        var character = profileData.RootElement.GetProperty("Response").GetProperty("characters").GetProperty("data").EnumerateObject().FirstOrDefault().Value;
        if (ValidationHelpers.IsJsonValueKindUndefined(character, out var characterMessage))
        {
            _logger.LogError(characterMessage);
            throw new Exception(characterMessage);
        }

        var characterId = character.GetProperty("characterId").ToString();
        var equipmentInfo = await _bungieApiService.GetEquipment(membershipType, membershipId, characterId);
        if (ValidationHelpers.IsEmptyString(equipmentInfo, out var equipmentInfoMessage))
        {
            _logger.LogError(equipmentInfoMessage);
            throw new Exception(equipmentInfoMessage);
        }
        
        session.SetString(EQUIPMENT_DATA_CACHE_KEY, equipmentInfo);
        return equipmentInfo;
    }
}
