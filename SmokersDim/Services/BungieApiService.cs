using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public interface IBungieApiService
{
    Task<string> GetMembershipInfo();
    Task<string> GetProfile(string membershipType, string membershipId);
    Task<string> GetEquipment(string membershipType, string membershipId, string characterId);
}

public class BungieApiService : IBungieApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _proxyUrl;
    private readonly ILogger<BungieApiService> _logger;

    public BungieApiService(HttpClient httpClient, IConfiguration configuration, ILogger<BungieApiService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseUrl = configuration["Bungie:BaseUrl"];
        _proxyUrl = configuration["Proxy:Url"];
        _logger = logger;
    }

    public async Task<string> GetMembershipInfo()
    {
        var url = $"{_baseUrl}/User/GetMembershipsForCurrentUser/";
        _logger.LogInformation("Requesting Membership Info from URL: {Url}", url);

        return await SendRequestAsync(url);
    }

    public async Task<string> GetProfile(string membershipType, string membershipId)
    {
        var url = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/?components=200";
        _logger.LogInformation("Requesting Profile Info from URL: {Url}", url);

        return await SendRequestAsync(url);
    }

    public async Task<string> GetEquipment(string membershipType, string membershipId, string characterId)
    {
        var url = $"{_baseUrl}/Destiny2/{membershipType}/Profile/{membershipId}/Character/{characterId}/?components=205";
        _logger.LogInformation("Requesting Equipment Info from URL: {Url}", url);

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
