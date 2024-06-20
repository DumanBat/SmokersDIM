// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Mvc;

// public class ProxyController: Controller
// {

//     [HttpGet("api/proxy/login")]
//     public IActionResult Login()
//     {
//         var authenticationProperties = new AuthenticationProperties
//         {
//             RedirectUri = "https://localhost:5281/api/auth/authcallback"
//         };
//         return Challenge(authenticationProperties, "Bungie");
//     }
// }
using AspNetCore.Proxy.Builders;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class ProxyController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly ILogger<ProxyController> _logger;

    public ProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ITokenService tokenService, ILogger<ProxyController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _tokenService = tokenService;
        _logger = logger;
    }

    [HttpGet("api/proxy/login")]
    public IActionResult Login()
    {
        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = "https://localhost:5281/api/auth/authcallback"
        };
        return Challenge(authenticationProperties, "Bungie");
    }

    [HttpGet("api/proxy/data")]
    public async Task<IActionResult> ProxyData([FromHeader(Name = "MainApp-Url")] string mainAppUrl)
    {
        if (string.IsNullOrEmpty(mainAppUrl))
        {
            _logger.LogError($"LOG: Access Token == {_tokenService.AccessToken}");
            return BadRequest("Missing MainApp-Url header.");
        }

        var httpClient = _httpClientFactory.CreateClient("proxyClient");
        var request = new HttpRequestMessage(HttpMethod.Get, mainAppUrl);
        _logger.LogInformation($"LOG: Access Token == {_tokenService.AccessToken}");

        if (!string.IsNullOrEmpty(_tokenService.AccessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);
            request.Headers.Add("X-API-Key", _configuration["Bungie:ApiKey"]);
        }

        var response = await httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode}\n{responseContent}");
        }

        return Content(responseContent, "application/json");
    }
}
