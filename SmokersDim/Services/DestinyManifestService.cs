using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public interface IDestinyManifectService
{
	Task<string> UpdateInventoryItemsManifestAsync();
}

public class DestinyManifectService : IDestinyManifectService
{	
	public const string ITEM_MANIFEST_CACHE_KEY = "ItemManifest";
	private readonly IBungieApiService _bungieApiService;
	private readonly ILogger<EquipmentService> _logger;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly DestinyDbContext _dbContext;
	
	public DestinyManifectService(IBungieApiService bungieApiService, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor, DestinyDbContext destinyDbContext)
	{
		_bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
		_logger = logger;
		_httpContextAccessor = httpContextAccessor;
		_dbContext = destinyDbContext;
	}
	
	public async Task<string> UpdateInventoryItemsManifestAsync()
	{
		var session = _httpContextAccessor.HttpContext.Session;		
		var manifest = await _bungieApiService.GetManifest();
		
		if (ValidationHelpers.IsEmptyString(manifest, out var manifestMessage))
		{
			_logger.LogError(manifestMessage);
			throw new Exception(manifestMessage);
		}
		
		var manifestData = JsonDocument.Parse(manifest);
		var languages = manifestData.RootElement.GetProperty("Response").GetProperty("jsonWorldComponentContentPaths").EnumerateObject();
		var manifestsCollection = new JsonElement();
		
		foreach (var language in languages)
		{
			if (language.Name == "en")
			{
				manifestsCollection = language.Value;
				break;
			}
		}
		
		var itemManifestUrl = manifestsCollection.GetProperty(AppConstants.DestinyInventoryItemDefinition).ToString();		
		var itemManifest = await _bungieApiService.GetSpecifiedManifest(itemManifestUrl);
		
		Dictionary<string, Item> items = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Item>>(itemManifest);
		var rootObject = new InventoryItemsRootObject();
		foreach (var itemPair in items)
		{
			var item = HandleStatsProperty(itemPair.Value);
			
			_dbContext.Add(item);
		}
		
		rootObject.Items = items;
		
		var path = $"{AppConstants.ManifestDataFolder}/{AppConstants.DestinyInventoryItemDefinition}.json";
		await WriteRootObjectToJsonFileAsync(rootObject, path);
		await _dbContext.SaveChangesAsync();

		return "Manifest successfully updated";
	}
	
	private Item HandleStatsProperty(Item item)
	{
		var statsBlock = item.stats;
		if (statsBlock == null || statsBlock.stats == null)
			return item;
		
		statsBlock.statsList = statsBlock.stats.Values.ToList();
		
		var existingStatsBlock = _dbContext.StatsBlocks.AsNoTracking().SingleOrDefault(s => s.statGroupHash == statsBlock.statGroupHash);
			
		if (existingStatsBlock != null)
		{
			_dbContext.Entry(statsBlock).State = EntityState.Detached;
			
			_dbContext.Entry(existingStatsBlock).State = EntityState.Modified;
			existingStatsBlock.disablePrimaryStatDisplay = statsBlock.disablePrimaryStatDisplay;
			existingStatsBlock.hasDisplayableStats = statsBlock.hasDisplayableStats;
			existingStatsBlock.primaryBaseStatHash = statsBlock.primaryBaseStatHash;
			
			existingStatsBlock.statsList.Clear();
			foreach (var statEntry in statsBlock.statsList)
			{
				existingStatsBlock.statsList.Add(statEntry);
			}

			item.stats = existingStatsBlock;
		}
		else
		{
			var trackedEntity = _dbContext.StatsBlocks.Local.SingleOrDefault(s => s.statGroupHash == statsBlock.statGroupHash);
			if (trackedEntity == null)
			{
				_dbContext.StatsBlocks.Add(statsBlock);
				item.stats = statsBlock;
			}
			else
			{
				item.stats = trackedEntity;
			}
		}
		
		return item;
	}
	
	private async Task WriteRootObjectToJsonFileAsync(InventoryItemsRootObject rootObject, string filePath)
	{
		try
		{
			using (FileStream createStream = File.Create(filePath))
			{
				await System.Text.Json.JsonSerializer.SerializeAsync(createStream, rootObject);
			}

			_logger.LogInformation("JSON data written to file successfully.");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error writing JSON to file: {ex.Message}");
		}
	}
}