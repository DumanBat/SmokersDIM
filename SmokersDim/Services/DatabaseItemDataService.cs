using Microsoft.EntityFrameworkCore;

public interface IDatabaseItemDataService
{
	Task<string> GetItemIcon(uint itemHash);
	Task<DisplayProperties> GetItemDisplayProperties(uint itemHash);
}

public class DatabaseItemDataService : IDatabaseItemDataService
{
	private readonly string _baseUrl;
	private IHttpContextAccessor _httpContextAccessor;
	private ILogger<EquipmentService> _logger;
	private DestinyDbContext _dbContext;
	
	public DatabaseItemDataService(IConfiguration configuration, ILogger<EquipmentService> logger, IHttpContextAccessor httpContextAccessor, DestinyDbContext destinyDbContext)
	{
		_logger = logger;
		_baseUrl = configuration["Bungie:BaseUrl"];
		_httpContextAccessor = httpContextAccessor;
		_dbContext = destinyDbContext;
	}

	public async Task<DisplayProperties?> GetItemDisplayProperties(uint itemHash)
	{
		var item = _dbContext.Items.Include(i => i.displayProperties).FirstOrDefault(i => i.hash == itemHash);
		if (item == null || item.displayProperties == null)
		{
			_logger.LogWarning($"Item with hash : {itemHash} is NULL or doesn't have display properties");
			return null;
		}
		
		return item.displayProperties;
	}

	public async Task<string> GetItemIcon(uint itemHash)
	{
		var item = _dbContext.Items.Include(i => i.displayProperties).FirstOrDefault(i => i.hash == itemHash);
		if (item == null || item.displayProperties == null)
		{
			_logger.LogWarning($"Item with hash : {itemHash} is NULL or doesn't have display properties");
			return "";
		}
		
		var iconUrl = item.displayProperties.hasIcon ? item.displayProperties.icon : "";
		var fullUrl = $"{_baseUrl}{iconUrl}";
		return fullUrl;
	}
}