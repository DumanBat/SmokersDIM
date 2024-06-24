using Microsoft.EntityFrameworkCore;

public class DestinyDbContext : DbContext
{
	public DestinyDbContext(DbContextOptions<DestinyDbContext> options) : base(options) { }
	
	public DbSet<Item> Items { get; set; }
	public DbSet<DestinyColor> BackgroundColors { get; set; }
	public DbSet<DisplayProperties> DisplayProperties { get; set; }
	public DbSet<Action> Actions { get; set; }
	public DbSet<Inventory> Inventories { get; set; }
	public DbSet<StatsBlock> Stats { get; set; }
	public DbSet<EquippingBlock> EquippingBlocks { get; set; }
	public DbSet<TranslationBlock> TranslationBlocks { get; set; }
	public DbSet<Quality> Qualities { get; set; }
	public DbSet<TalentGrid> TalentGrids { get; set; }
	public DbSet<InvestmentStat> InvestmentStats { get; set; }
	public DbSet<DyeReference> Dyes { get; set; }
	public DbSet<Arrangement> Arrangements { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
