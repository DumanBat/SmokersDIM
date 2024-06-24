using Microsoft.EntityFrameworkCore;

public class DestinyDbContext : DbContext
{
	public DestinyDbContext(DbContextOptions<DestinyDbContext> options) : base(options) { }
	
	public DbSet<Item> Items { get; set; }
	public DbSet<SocketBlock> SocketBlocks { get; set; }
	public DbSet<SocketEntry> SocketEntries { get; set; }
	public DbSet<IntrinsicSocketEntryDefinition> IntrinsicSocketEntryDefinitions { get; set; }
	public DbSet<SocketCategory> SocketCategories { get; set; }
	public DbSet<SocketEntryPlugItem> SocketEntryPlugItems { get; set; }
	public DbSet<PerkEntry> PerkEntries { get; set; }
	public DbSet<TooltipNotification> TooltipNotifications { get; set; }
	public DbSet<DisplayProperties> DisplayProperties { get; set; }
	public DbSet<DestinyColor> DestinyColors { get; set; }
	public DbSet<Action> Actions { get; set; }
	public DbSet<Crafting> Craftings { get; set; }
	public DbSet<SetBlock> SetBlocks { get; set; }
	public DbSet<SetBlockEntry> SetBlockEntries { get; set; }
	public DbSet<CraftingBlockBonusPlug> CraftingBlockBonusPlugs { get; set; }
	public DbSet<ValueBlock> ValueBlocks { get; set; }
	public DbSet<ItemQuantity> ItemQuantities { get; set; }
	public DbSet<RequiredItem> RequiredItems { get; set; }
	public DbSet<ProgressionReward> ProgressionRewards { get; set; }
	public DbSet<Inventory> Inventories { get; set; }
	public DbSet<StatEntry> StatEntries { get; set; }
	public DbSet<StatsBlock> StatsBlocks { get; set; }
	public DbSet<EquippingBlock> EquippingBlocks { get; set; }
	public DbSet<TranslationBlock> TranslationBlocks { get; set; }
	public DbSet<DyeReference> DyeReferences { get; set; }
	public DbSet<Arrangement> Arrangements { get; set; }
	public DbSet<Quality> Qualities { get; set; }
	public DbSet<Version> Versions { get; set; }
	public DbSet<TalentGrid> TalentGrids { get; set; }
	public DbSet<InvestmentStat> InvestmentStats { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		modelBuilder.Entity<Item>()
			.HasMany(i => i.TooltipNotifications)
			.WithOne()
			.HasForeignKey("ItemId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.DisplayProperties)
			.WithMany()
			.HasForeignKey("DisplayPropertiesId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.BackgroundColor)
			.WithMany()
			.HasForeignKey("BackgroundColorId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.Action)
			.WithMany()
			.HasForeignKey("ActionId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.Crafting)
			.WithMany()
			.HasForeignKey("CraftingId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.Inventory)
			.WithMany()
			.HasForeignKey("InventoryId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.SetData)
			.WithMany()
			.HasForeignKey("SetDataId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.Stats)
			.WithMany()
			.HasForeignKey("StatsId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.EquippingBlock)
			.WithMany()
			.HasForeignKey("EquippingBlockId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.TranslationBlock)
			.WithMany()
			.HasForeignKey("TranslationBlockId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.Quality)
			.WithMany()
			.HasForeignKey("QualityId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.Value)
			.WithMany()
			.HasForeignKey("ValueId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.Sockets)
			.WithMany()
			.HasForeignKey("SocketsId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.TalentGrid)
			.WithMany()
			.HasForeignKey("TalentGridId");

		modelBuilder.Entity<Item>()
			.HasMany(i => i.InvestmentStats)
			.WithOne()
			.HasForeignKey("ItemId");

		modelBuilder.Entity<Item>()
			.HasMany(i => i.Perks)
			.WithOne()
			.HasForeignKey("ItemId");

		modelBuilder.Entity<SocketBlock>()
			.HasMany(sb => sb.SocketEntries)
			.WithOne()
			.HasForeignKey("SocketBlockId");

		modelBuilder.Entity<SocketBlock>()
			.HasMany(sb => sb.IntrinsicSockets)
			.WithOne()
			.HasForeignKey("SocketBlockId");

		modelBuilder.Entity<SocketBlock>()
			.HasMany(sb => sb.SocketCategories)
			.WithOne()
			.HasForeignKey("SocketBlockId");

		modelBuilder.Entity<SocketEntry>()
			.HasMany(se => se.ReusablePlugItems)
			.WithOne()
			.HasForeignKey("SocketEntryId");

		modelBuilder.Entity<Crafting>()
			.HasMany(c => c.BonusPlugs)
			.WithOne()
			.HasForeignKey("CraftingId");

		modelBuilder.Entity<SetBlock>()
			.HasMany(sb => sb.ItemList)
			.WithOne()
			.HasForeignKey("SetBlockId");

		modelBuilder.Entity<ValueBlock>()
			.HasMany(vb => vb.ItemValue)
			.WithOne()
			.HasForeignKey("ValueBlockId");

		modelBuilder.Entity<Action>()
			.HasMany(a => a.RequiredItems)
			.WithOne()
			.HasForeignKey("ActionId");

		modelBuilder.Entity<Action>()
			.HasMany(a => a.ProgressionRewards)
			.WithOne()
			.HasForeignKey("ActionId");
			
 		modelBuilder.Entity<StatsBlock>()
			.HasKey(sb => sb.StatGroupHash);

		modelBuilder.Entity<StatEntry>()
			.HasKey(se => se.Id);

		modelBuilder.Entity<StatsBlock>()
			.HasMany(sb => sb.Stats)
			.WithOne(se => se.StatsBlock)
			.HasForeignKey(se => se.StatsBlockId);
		
		/////
		modelBuilder.Entity<TranslationBlock>()
			.HasKey(tb => tb.WeaponPatternHash);

		modelBuilder.Entity<DyeReference>()
			.HasKey(dr => dr.Id);

		modelBuilder.Entity<TranslationBlock>()
			.HasMany(tb => tb.LockedDyes)
			.WithOne(dr => dr.TranslationBlock)
			.HasForeignKey(dr => dr.TranslationBlockId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Quality>()
			.HasMany(q => q.Versions)
			.WithOne()
			.HasForeignKey("QualityId");
	}
}
