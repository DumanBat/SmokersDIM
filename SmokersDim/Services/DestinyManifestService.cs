using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
	private readonly DestinyDbContext _dbContext;
	
	public DestinyManifectService(IBungieApiService bungieApiService, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor, DestinyDbContext destinyDbContext)
	{
		_bungieApiService = bungieApiService ?? throw new ArgumentNullException(nameof(bungieApiService));
		_logger = logger;
		_httpContextAccessor = httpContextAccessor;
		_dbContext = destinyDbContext;
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
		
		var items = JsonSerializer.Deserialize<Dictionary<string, Item>>(itemManifest);
				
		if (items?.Count == 0)
		{
			_logger.LogWarning("Cannot get items Count");
			return "Cannot get items Count";
		}
		
		var newRoot = new RootObject();
		newRoot.Items = new Dictionary<string, Item>();
		
		foreach (var item in items)
		{
			newRoot.Items.Add(item.Key, item.Value);
		}
		
		var path = $"{AppConstants.ManifestDataFolder}/{AppConstants.DestinyInventoryItemDefinition}.json";
		await WriteRootObjectToJsonFileAsync(newRoot, path);
		//await _dbContext.SaveChangesAsync();

		return "Manifest successfully updated";
	}
	
	string CleanUpJsonString(string input)
	{
		return Regex.Replace(input, @"\\u(?<Value>[a-zA-Z0-9]{4})", m =>
		{
			var value = m.Groups["Value"].Value;
			return ((char)int.Parse(value, System.Globalization.NumberStyles.HexNumber)).ToString();
		});
	}
	
	private string DecodeUtf8Base64(string base64String)
	{
		var base64Bytes = Convert.FromBase64String(base64String);
		return Encoding.UTF8.GetString(base64Bytes);
	}
	
	private async Task WriteStringToJsonFileAsync(string jsonString, string filePath)
	{
		try
		{
			_logger.LogError(TruncateLongString(jsonString, 75));
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
	
	private async Task WriteRootObjectToJsonFileAsync(RootObject rootObject, string filePath)
	{
		try
		{
			using (FileStream createStream = File.Create(filePath))
			{
				await JsonSerializer.SerializeAsync(createStream, rootObject);
			}

			_logger.LogInformation("JSON data written to file successfully.");
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error writing JSON to file: {ex.Message}");
		}
	}
	
	public string TruncateLongString(string str, int maxLength)
	{
		return str?[0..Math.Min(str.Length, maxLength)];
	}
}