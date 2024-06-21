using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IEquipmentService equipmentService, ILogger<AccountController> logger)
    {
        _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        _logger = logger;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Redirect(UrlConstants.LoginRedirectUrl);
    }

    [HttpGet("auth-callback")]
    public async Task<IActionResult> AuthCallback()
    {
        try
        {
            await _equipmentService.SetEquipmentDataAsync();
            return Redirect(UrlConstants.AuthCallbackRedirectUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to set equipment data");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("show-equipment")]
    public IActionResult ShowEquipment()
    {
        var session = HttpContext.Session;
        var equipmentData = session.GetString(EquipmentService.EQUIPMENT_DATA_CACHE_KEY);

        if (!string.IsNullOrEmpty(equipmentData))
            return Content(equipmentData, AppConstants.JsonContentType);        
        else
        {
            _logger.LogError($"{EquipmentService.EQUIPMENT_DATA_CACHE_KEY} is empty");
            return StatusCode(500, $"{EquipmentService.EQUIPMENT_DATA_CACHE_KEY} is empty");
        }
    }
}
