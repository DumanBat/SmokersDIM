using System.Text.Json;
using Newtonsoft.Json;

public interface IItemInstanceService
{
	Task<ItemInstance> GetItemInstanceData(string itemInstanceId);
}

public class ItemInstanceService : IItemInstanceService
{
	private readonly string _baseUrl;
	private IHttpContextAccessor _httpContextAccessor;
	private ILogger<EquipmentService> _logger;
	private DestinyDbContext _dbContext;
	private readonly IBungieApiService _bungieApiService;
	
	public ItemInstanceService(IConfiguration configuration, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor, DestinyDbContext destinyDbContext, IBungieApiService bungieApiService)
	{
		_bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
		_logger = logger;
		_baseUrl = configuration["Bungie:BaseUrl"];
		_httpContextAccessor = httpContextAccessor;
		_dbContext = destinyDbContext;
	}
	
	public async Task<ItemInstance> GetItemInstanceData(string itemInstanceId)
	{
		var session = _httpContextAccessor.HttpContext.Session;
		var membershipType = session.GetString(AccountController.MEMBERSHIP_TYPE_CACHE_KEY);
		var membershipId = session.GetString(AccountController.MEMBERSHIP_ID_CACHE_KEY);
		
		var itemInstanceInfo = await _bungieApiService.GetItemInstance(membershipType, membershipId, itemInstanceId);			
		
		if (ValidationHelpers.IsEmptyString(itemInstanceInfo, out var itemInstanceInfoMessage))
		{
			_logger.LogError(itemInstanceInfoMessage);
			throw new Exception(itemInstanceInfoMessage);
		}
		var itemInstanceJson = JsonDocument.Parse(itemInstanceInfo);
		var itemsJson = itemInstanceJson.RootElement.GetProperty("Response").ToString();
		var itemInstance = JsonConvert.DeserializeObject<ItemInstance>(itemsJson);
		
		_logger.LogInformation($"Damage type: {itemInstance.instance.data.damageType}");
		
		return itemInstance;
	}	
}