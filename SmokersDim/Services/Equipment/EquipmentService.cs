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

	public async Task<string> SetEquipmentDataAsync(string membershipType, string membershipId, string characterIdsString)
	{
		var session = _httpContextAccessor.HttpContext.Session;
		var equipmentDatas = new EquipmentDatas();
		var equipmentDataList = new List<EquipmentData>();
		var characterIds = characterIdsString.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
				
		foreach (var id in characterIds)
		{
			var equipmentData = new EquipmentData();
			equipmentData.characterId = id;
			
			var equipmentInfo = await _bungieApiService.GetEquipment(membershipType, membershipId, id);
			if (ValidationHelpers.IsEmptyString(equipmentInfo, out var equipmentInfoMessage))
			{
				_logger.LogError(equipmentInfoMessage);
				throw new Exception(equipmentInfoMessage);
			}
			var equipmentJson = JsonDocument.Parse(equipmentInfo);
			var itemsJson = equipmentJson.RootElement.GetProperty("Response").GetProperty("equipment").GetProperty("data").GetProperty("items").EnumerateArray();
			var items = new List<Item>();
			
			foreach (var itemJson in itemsJson)
			{
				var item = new Item();

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
			
			equipmentData.items = items;
			
			equipmentDataList.Add(equipmentData);
		}
		
		equipmentDatas.equipmentDatas = equipmentDataList;
		
		var serializedEquipment = JsonSerializer.Serialize(equipmentDatas);		
		session.SetString(EQUIPMENT_DATA_CACHE_KEY, serializedEquipment);
		return serializedEquipment;
	}
}
