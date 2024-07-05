using System.Collections.Immutable;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;

public interface IEquipmentService
{
	Task<string> SetEquipmentDataAsync(string membershipType, string membershipId, string characterIdsString);
}

//TODO: Find a better approach to handle missing properties from JSON
public class EquipmentService : IEquipmentService
{
	public const string CHARACTER_DATA_CACHE_KEY = "CharacterData";
	public const string KINETIC_BUCKET_HASH = "1498876634";
	public const string ENERGY_BUCKET_HASH = "2465295065";
	public const string HEAVY_BUCKET_HASH = "953998645";
	private readonly IBungieApiService _bungieApiService;
	private readonly ILogger<EquipmentService> _logger;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public EquipmentService(IBungieApiService bungieApiService, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor)
	{
		_bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
		_logger = logger;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<string> SetEquipmentDataAsync(string membershipType, string membershipId, string characterIdsString)
	{
		var session = _httpContextAccessor.HttpContext.Session;
		var characterDatas = new CharacterDatas();
		var characterDataList = new List<CharacterData>();
		var characterIds = characterIdsString.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
				
		foreach (var id in characterIds)
		{
			var characterData = new CharacterData
			{
				characterId = characterIdsString,
				items = await GetCharacterItemsAsync(membershipType, membershipId, id, false),
				inventoryItems = await GetCharacterItemsAsync(membershipType, membershipId, id, true)				
			};
			characterData.kinetic = GetCharacterItemsInSlot(characterData.inventoryItems, KINETIC_BUCKET_HASH);
			characterData.energy = GetCharacterItemsInSlot(characterData.inventoryItems, ENERGY_BUCKET_HASH);
			characterData.heavy = GetCharacterItemsInSlot(characterData.inventoryItems, HEAVY_BUCKET_HASH);

			characterDataList.Add(characterData);
		}
		
		characterDatas.characterDatas = characterDataList;
		
		var serializedCharactersData = JsonSerializer.Serialize(characterDatas);		
		session.SetString(CHARACTER_DATA_CACHE_KEY, serializedCharactersData);
		return serializedCharactersData;
	}
	
	private async Task<List<CharacterItem>> GetCharacterItemsAsync(string membershipType, string membershipId, string characterIdsString, bool isInventory)
	{
		var equipmentInfo = isInventory ? 
			await _bungieApiService.GetCharacterInventory(membershipType, membershipId, characterIdsString) :
			await _bungieApiService.GetEquipment(membershipType, membershipId, characterIdsString);
			
		var collectionName = isInventory ? "inventory" : "equipment";

		if (ValidationHelpers.IsEmptyString(equipmentInfo, out var equipmentInfoMessage))
		{
			_logger.LogError(equipmentInfoMessage);
			throw new Exception(equipmentInfoMessage);
		}
		var equipmentJson = JsonDocument.Parse(equipmentInfo);
		var itemsJson = equipmentJson.RootElement.GetProperty("Response").GetProperty(collectionName).GetProperty("data").GetProperty("items").EnumerateArray();
		var items = new List<CharacterItem>();
		
		foreach (var itemJson in itemsJson)
		{
			var item = new CharacterItem();

			if (itemJson.TryGetProperty("itemHash", out var itemHash))
				item.itemHash = itemHash.ToString();

			if (itemJson.TryGetProperty("itemInstanceId", out var itemInstanceId))
				item.itemInstanceId = itemInstanceId.ToString();

			if (itemJson.TryGetProperty("quantity", out var quantity))
				item.quantity = quantity.GetInt32();

			if (itemJson.TryGetProperty("bindStatus", out var bindStatus))
				item.bindStatus = bindStatus.GetInt32();

			if (itemJson.TryGetProperty("location", out var location))
				item.location = location.GetInt32();

			if (itemJson.TryGetProperty("bucketHash", out var bucketHash))
				item.bucketHash = bucketHash.ToString();

			if (itemJson.TryGetProperty("transferStatus", out var transferStatus))
				item.transferStatus = transferStatus.GetInt32();

			if (itemJson.TryGetProperty("lockable", out var lockable))
				item.lockable = lockable.GetBoolean();

			if (itemJson.TryGetProperty("state", out var state))
				item.state = state.GetInt32();

			if (itemJson.TryGetProperty("dismantlePermission", out var dismantlePermission))
				item.dismantlePermission = dismantlePermission.GetInt32();

			if (itemJson.TryGetProperty("isWrapper", out var isWrapper))
				item.isWrapper = isWrapper.GetBoolean();

			if (itemJson.TryGetProperty("versionNumber", out var versionNumber))
				item.versionNumber = versionNumber.GetInt32();

			if (itemJson.TryGetProperty("overrideStyleItemHash", out var overrideStyleItemHash))
				item.overrideStyleItemHash = overrideStyleItemHash.ToString();

			if (itemJson.TryGetProperty("tooltipNotificationIndexes", out var tooltipNotificationIndexesElement))
			{
				var tooltipNotificationIndexes = new List<int>();
				foreach (var index in tooltipNotificationIndexesElement.EnumerateArray())
				{
					tooltipNotificationIndexes.Add(index.GetInt32());
				}
				item.tooltipNotificationIndexes = tooltipNotificationIndexes;
			}

			items.Add(item);
		}
		
		return items;
	}
	
	private List<CharacterItem> GetCharacterItemsInSlot(List<CharacterItem> inventoryItems, string bucketHash)
	{
		var slotItems = new List<CharacterItem>();
		var isPassedBucket = false;
		foreach (var item in inventoryItems)
		{
			if (item.bucketHash != bucketHash && isPassedBucket)
				break;
			if (item.bucketHash != bucketHash)
				continue;
			
			isPassedBucket = true;
			slotItems.Add(item);
		}
		
		return slotItems;
	}
}
