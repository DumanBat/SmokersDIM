using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public interface IBungieApiService
{
	Task<string> GetManifest();
	Task<string> GetSpecifiedManifest(string manifestUrl);
	
	Task<string> GetMembershipInfo();
	Task<string> GetProfile(string membershipType, string membershipId);
	Task<string> GetProfileVaultData(string membershipType, string membershipId);
	Task<string> GetEquipment(string membershipType, string membershipId, string characterId);
	Task<string> GetCharacterInventory(string membershipType, string membershipId, string characterId);
	Task<string> GetItemInstance(string membershipType, string membershipId, string itemInstanceHash);
}

public class BungieApiService : IBungieApiService
{
	private readonly HttpClient _httpClient;
	private readonly string _basePlatformUrl;
	private readonly string _baseUrl;
	private readonly string _proxyUrl;
	private readonly ILogger<BungieApiService> _logger;

	public BungieApiService(HttpClient httpClient, IConfiguration configuration, ILogger<BungieApiService> logger)
	{
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_baseUrl = configuration["Bungie:BaseUrl"];
		_basePlatformUrl = configuration["Bungie:BasePlatformUrl"];
		_proxyUrl = configuration["Proxy:Url"];
		_logger = logger;
	}

	public async Task<string> GetMembershipInfo()
	{
		var url = $"{_basePlatformUrl}/User/GetMembershipsForCurrentUser/";
		_logger.LogInformation("Requesting Membership Info from URL: {Url}", url);

		return await SendRequestAsync(url);
	}

	public async Task<string> GetProfile(string membershipType, string membershipId)
	{
		var url = $"{_basePlatformUrl}/Destiny2/{membershipType}/Profile/{membershipId}/?components=200";
		_logger.LogInformation("Requesting Profile Info from URL: {Url}", url);

		return await SendRequestAsync(url);
	}

	public async Task<string> GetEquipment(string membershipType, string membershipId, string characterId)
	{
		var url = $"{_basePlatformUrl}/Destiny2/{membershipType}/Profile/{membershipId}/Character/{characterId}/?components=205";
		_logger.LogInformation("Requesting Equipment Info from URL: {Url}", url);

		return await SendRequestAsync(url);
	}
	
	public async Task<string> GetCharacterInventory(string membershipType, string membershipId, string characterId)
	{
		var url = $"{_basePlatformUrl}/Destiny2/{membershipType}/Profile/{membershipId}/Character/{characterId}/?components=201";
		_logger.LogInformation("Requesting Character Inventory Info from URL: {Url}", url);

		return await SendRequestAsync(url);
	}
	
	public async Task<string> GetManifest()
	{
		var url = $"{_basePlatformUrl}/Destiny2/Manifest/";
		_logger.LogInformation("Requesting Destiny Manifest from URL: {Url}", url);

		return await SendRequestAsync(url);
	}
	
	public async Task<string> GetSpecifiedManifest(string manifestUrl)
	{
		var url = $"{_baseUrl}{manifestUrl}";
		_logger.LogInformation("Requesting Destiny Manifest from URL: {Url}", url);

		return await SendRequestAsync(url);
	}

	public async Task<string> GetProfileVaultData(string membershipType, string membershipId)
	{
		var url = $"{_basePlatformUrl}/Destiny2/{membershipType}/Profile/{membershipId}/?components=102";
		_logger.LogInformation("Requesting Profile Vault Data from URL: {Url}", url);

		return await SendRequestAsync(url);
	}

	public async Task<string> GetItemInstance(string membershipType, string membershipId, string itemInstanceHash)
	{
		var url = $"{_basePlatformUrl}/Destiny2/{membershipType}/Profile/{membershipId}/Item/{itemInstanceHash}/?components=300,302,304";
		_logger.LogInformation("Requesting Item Instance Data from URL: {Url}", url);

		return await SendRequestAsync(url);
	}

	private async Task<string> SendRequestAsync(string url)
	{
		var requestMessage = new HttpRequestMessage(HttpMethod.Get, _proxyUrl);
		requestMessage.Headers.Add("MainApp-Url", url);

		var response = await _httpClient.SendAsync(requestMessage);
		if (!response.IsSuccessStatusCode)
		{
			_logger.LogError("Request failed: {StatusCode} {ReasonPhrase} \n RequestUrl: {requestUrl}", response.StatusCode, response.ReasonPhrase, url);
			throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
		}

		var responseContent = await response.Content.ReadAsStringAsync();
		return responseContent;
	}
}
