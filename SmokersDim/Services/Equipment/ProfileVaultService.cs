
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public interface IProfileVaultService
{
	Task<string> SetVaultDataAsync(string membershipType, string membershipId);
}

public class ProfileVaultService : IProfileVaultService
{
	public const string VAULT_DATA_CACHE_KEY = "VaultData";
	public const uint KINETIC_BUCKET_HASH = 1498876634;
	public const uint ENERGY_BUCKET_HASH = 2465295065;
	public const uint HEAVY_BUCKET_HASH = 953998645;
	
	private readonly IBungieApiService _bungieApiService;
	private readonly ILogger<EquipmentService> _logger;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly DestinyDbContext _dbContext;
	
	public ProfileVaultService(IBungieApiService bungieApiService, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor, DestinyDbContext dbContext)
	{
		_bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
		_logger = logger;
		_httpContextAccessor = httpContextAccessor;
		_dbContext = dbContext;
	}
	
	public async Task<string> SetVaultDataAsync(string membershipType, string membershipId)
	{
		var session = _httpContextAccessor.HttpContext.Session;
		var vaultInfo = await _bungieApiService.GetProfileVaultData(membershipType, membershipId);

		if (ValidationHelpers.IsEmptyString(vaultInfo, out var vaultInfoMessage))
		{
			_logger.LogError(vaultInfoMessage);
			throw new Exception(vaultInfoMessage);
		}
		var vaultJson = JsonDocument.Parse(vaultInfo);
		var itemsJson = vaultJson.RootElement.GetProperty("Response").GetProperty("profileInventory").GetProperty("data").GetProperty("items").EnumerateArray();
		var items = new List<CharacterItem>();
		
		foreach (var itemJson in itemsJson)
		{
			var item = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterItem>(itemJson.ToString());
			items.Add(item);
		}
		var vaultData = new VaultData()
		{
			allVaultItems = items,
			kinetic = GetVaultItemsInSlot(items, KINETIC_BUCKET_HASH),
			energy = GetVaultItemsInSlot(items, ENERGY_BUCKET_HASH),
			heavy = GetVaultItemsInSlot(items, HEAVY_BUCKET_HASH),
		};
		
		var serializedVaultData = JsonSerializer.Serialize(vaultData);		
		session.SetString(VAULT_DATA_CACHE_KEY, serializedVaultData);
		return "";
	}
	
	private List<CharacterItem> GetVaultItemsInSlot(List<CharacterItem> inventoryItems, uint bucketHash)
	{
		var slotItems = new List<CharacterItem>();
		
		foreach (var item in inventoryItems)
		{
			var dbItem = _dbContext.Items.Include(i => i.inventory).Include(i => i.displayProperties).FirstOrDefault(i => i.hash.ToString() == item.itemHash);
			var inventory = dbItem?.inventory;
			if (inventory == null || inventory.bucketTypeHash != bucketHash)
				continue;
			
			slotItems.Add(item);
		}
		
		return slotItems;
	}
}