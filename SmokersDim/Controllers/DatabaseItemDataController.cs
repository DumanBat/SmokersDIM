using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("database-item-data")]
public class DatabaseItemDataController : ControllerBase
{
    private readonly IDatabaseItemDataService _databaseItemDataService;

    public DatabaseItemDataController(IDatabaseItemDataService databaseItemDataService)
    {
        _databaseItemDataService = databaseItemDataService;
    }

    [HttpGet("get-item-icon")]
    public async Task<IActionResult> GetItemIcon([FromQuery] uint itemHash)
    {
        var iconUrl = await _databaseItemDataService.GetItemIcon(itemHash);
        if (string.IsNullOrEmpty(iconUrl))
        {
            return NotFound("Icon URL not found");
        }
        return Ok(iconUrl);
    }
}