using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public interface IDestinyManifectService
{
	Task<string> SetDestinyManifestAsync();
}

public class DestinyManifectService : IDestinyManifectService
{	
	public const string ITEM_MANIFEST_CACHE_KEY = "ItemManifest";
	private readonly IBungieApiService _bungieApiService;
	private readonly ILogger<EquipmentService> _logger;
	private readonly IHttpContextAccessor _httpContextAccessor;
	
	public DestinyManifectService(IBungieApiService bungieApiService, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor)
	{
		_bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
		_logger = logger;
		_httpContextAccessor = httpContextAccessor;
	}
	
	public async Task<string> SetDestinyManifestAsync()
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
		
		var path = $"{AppConstants.ManifestDataFolder}/{AppConstants.DestinyInventoryItemDefinition}.json";
		await WriteStringToJsonFileAsync(itemManifest, path);
		return "";
	}
	
	private async Task WriteStringToJsonFileAsync(string jsonString, string filePath)
	{
		try
		{
			using (FileStream createStream = File.Create(filePath))
			{
				await JsonSerializer.SerializeAsync(createStream, jsonString);
			}

			_logger.LogInformation("JSON data written to file successfully.");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error writing JSON to file: {ex.Message}");
		}
	}
}