using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProxyController : ControllerBase
{
    private readonly string _apiKey;
    private readonly IHttpClientFactory _httpClientFactory;

    public ProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _apiKey = configuration["Bungie:ApiKey"];
    }

    [Authorize]
    [HttpGet("{*url}")]
    public async Task<IActionResult> Get(string url)
    {
        var client = _httpClientFactory.CreateClient();
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://www.bungie.net/{url}");

        // Add necessary headers
        requestMessage.Headers.Add("X-API-Key", _apiKey);

        var response = await client.SendAsync(requestMessage);
        var content = await response.Content.ReadAsStringAsync();

        return Content(content, response.Content.Headers.ContentType.ToString());
    }
}
