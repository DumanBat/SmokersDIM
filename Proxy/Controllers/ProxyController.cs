
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
        var authenticationProperties = new AuthenticationProperties()
        {
            RedirectUri = "https://localhost:5281/index.html"
        };
        return Challenge(authenticationProperties, "Bungie");
    }

    [HttpGet("/signin-bungie")]
    public async Task<IActionResult> HandleBungieCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync("Bungie");
        if (!authenticateResult.Succeeded)
        {
            return StatusCode(401, "SHIET!!! Authentication failed.");
        }

        return Redirect("https://localhost:5281/index.html");
    }
    
    [HttpGet("api/proxy/data")]
    public async Task<IActionResult> ProxyData([FromHeader(Name = "MainApp-Url")] string mainAppUrl)
    {
        var result = await ProxyDataTest(mainAppUrl);
        return result;
    }

    public async Task<IActionResult> ProxyDataTest(string mainAppUrl)
    {
        if (string.IsNullOrEmpty(mainAppUrl))
            return BadRequest("Missing MainApp-Url header.");

        var httpClient = _httpClientFactory.CreateClient("proxyClient");
        var request = new HttpRequestMessage(HttpMethod.Get, mainAppUrl);

        if (!string.IsNullOrEmpty(_tokenService.AccessToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.AccessToken);
        
        request.Headers.Add("X-API-Key", _configuration["Bungie:ApiKey"]);

        var response = await httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Response content: {responseContent}");
            return StatusCode((int)response.StatusCode, $"Error: {response.StatusCode}\n{responseContent}");
        }

        return Content(responseContent, "application/json");
    }
}
