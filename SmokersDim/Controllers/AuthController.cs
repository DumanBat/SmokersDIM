using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

[Route("api/[controller]")]
public class AuthController : Controller
{
    private static JsonElement? _latestEquipmentData;
    private ILogger<AuthController> _logger;

    private string testValue;
    

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("authcallback")]
    public async Task<IActionResult> Callback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync("Bungie");
        if (!authenticateResult.Succeeded)
        {
            return BadRequest("Error authenticating.");
        }

        // Extract user information from the authenticateResult
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value),
            new Claim(ClaimTypes.Name, authenticateResult.Principal.Identity.Name)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        // Create a local session (optional)
        HttpContext.Session.SetString("UserId", authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value);

        // var httpClient = new HttpClient();
        
        // var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:5099/api/proxy/equipment");
        // await httpClient.SendAsync(requestMessage);        
        
        // var httpClient = new HttpClient();
        // var response = await httpClient.GetAsync("https://localhost:5099/api/proxy/equipment");

        // if (response.IsSuccessStatusCode)
        // {
        //     _logger.LogInformation("successsssss");
        //     var equipmentData = await response.Content.ReadAsStringAsync();
        //     var parsedEquipmentData = JsonDocument.Parse(equipmentData).RootElement;
        //     await ReceiveEquipment(parsedEquipmentData);

        //     var filePath = Path.Combine("G:\\Backend\\test", "characterInfo.json");
        //     await System.IO.File.WriteAllTextAsync(filePath, equipmentData);
        // }
        //     _logger.LogInformation("hzzzzzzzz");
        return Redirect("/");
    }

    [HttpGet("equipment-callback")]
    public async Task<IActionResult> EquipmentCallback()
    {
        return Ok();
    }

    [HttpPost("receive-equipment")]
    public async Task<IActionResult> ReceiveEquipment([FromBody] JsonElement equipmentData)
    {
        _latestEquipmentData = equipmentData;
        return Ok();
    }

    [HttpPost("receive-equipment-test")]
    public async Task<IActionResult> ReceiveEquipmentTest(StringContent equipmentData)
    {
        testValue = equipmentData.ToString();
        var filePath = Path.Combine("G:\\Backend\\test", "test.txt");
        await System.IO.File.WriteAllTextAsync(filePath, testValue);
        return Ok();
    }

    [HttpGet("get-equipment")]
    public async Task<IActionResult> GetEquipment()
    {
        if (_latestEquipmentData == null)
        {
            return NotFound("No equipment data available.");
        }

        return Ok(_latestEquipmentData.Value);
    }
}
