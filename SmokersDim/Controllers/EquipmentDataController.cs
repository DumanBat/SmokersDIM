using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

[ApiController]
[Route("character-item-data")]
public class CharacterItemDataController : ControllerBase
{
	private readonly IEquipmentService _equipmentService;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ILogger<CharacterItemDataController> _logger;
	private readonly IItemInstanceService _itemInstanceService;

	public CharacterItemDataController(IEquipmentService equipmentService, IHttpContextAccessor httpContextAccessor, ILogger<CharacterItemDataController> logger, IItemInstanceService itemInstanceService)
	{
		_equipmentService = equipmentService;
		_httpContextAccessor = httpContextAccessor;
		_itemInstanceService = itemInstanceService;
		_logger = logger;
	}	
	
	[HttpGet("get-vault-items")]
	public async Task<IActionResult> GetVaultItems()
	{
		var session = _httpContextAccessor.HttpContext.Session;
		var equipmentData = session.GetString(ProfileVaultService.VAULT_DATA_CACHE_KEY);

		if (!string.IsNullOrEmpty(equipmentData))
			return Content(equipmentData, AppConstants.JsonContentType);        
		else
		{
			_logger.LogError($"{ProfileVaultService.VAULT_DATA_CACHE_KEY} is empty");
			return StatusCode(500, $"{ProfileVaultService.VAULT_DATA_CACHE_KEY} is empty");
		}
	}
	
	[HttpGet("get-item-instance")]
	public async Task<IActionResult> GetItemInstance([FromQuery] string itemInstanceHash)
	{
		var itemInstance = await _itemInstanceService.GetItemInstanceData(itemInstanceHash);
		// var jsonSerializerSettings = new JsonSerializerSettings
		// {
		// 	ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
		// 	ContractResolver = new CamelCasePropertyNamesContractResolver()
		// };

		//var itemInstanceJson = JsonConvert.SerializeObject(itemInstance, jsonSerializerSettings);
		var itemInstanceJson = JsonConvert.SerializeObject(itemInstance);

		if (!string.IsNullOrEmpty(itemInstanceJson))
			return Content(itemInstanceJson, AppConstants.JsonContentType);        
		else
		{
			_logger.LogError($"Item instance data is empty");
			return StatusCode(500, $"Item instance data is empty");
		}
	}
}