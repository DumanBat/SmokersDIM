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
	//public DbSet<TranslationBlock> TranslationBlocks { get; set; }
	//public DbSet<DyeReference> DyeReferences { get; set; }
	public DbSet<Arrangement> Arrangements { get; set; }
	public DbSet<Quality> Qualities { get; set; }
	public DbSet<Version> Versions { get; set; }
	public DbSet<TalentGrid> TalentGrids { get; set; }
	public DbSet<InvestmentStat> InvestmentStats { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		modelBuilder.Entity<Item>()
			.HasMany(i => i.tooltipNotifications)
			.WithOne()
			.HasForeignKey("ItemId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.displayProperties)
			.WithMany()
			.HasForeignKey("DisplayPropertiesId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.backgroundColor)
			.WithMany()
			.HasForeignKey("BackgroundColorId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.action)
			.WithMany()
			.HasForeignKey("ActionId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.crafting)
			.WithMany()
			.HasForeignKey("CraftingId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.inventory)
			.WithMany()
			.HasForeignKey("InventoryId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.setData)
			.WithMany()
			.HasForeignKey("SetDataId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.stats)
			.WithMany()
			.HasForeignKey("StatsId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.equippingBlock)
			.WithMany()
			.HasForeignKey("EquippingBlockId");

		// modelBuilder.Entity<Item>()
		// 	.HasOne(i => i.translationBlock)
		// 	.WithMany()
		// 	.HasForeignKey("TranslationBlockId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.quality)
			.WithMany()
			.HasForeignKey("QualityId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.value)
			.WithMany()
			.HasForeignKey("ValueId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.sockets)
			.WithMany()
			.HasForeignKey("SocketsId");

		modelBuilder.Entity<Item>()
			.HasOne(i => i.talentGrid)
			.WithMany()
			.HasForeignKey("TalentGridId");

		modelBuilder.Entity<Item>()
			.HasMany(i => i.investmentStats)
			.WithOne()
			.HasForeignKey("ItemId");

		modelBuilder.Entity<Item>()
			.HasMany(i => i.perks)
			.WithOne()
			.HasForeignKey("ItemId");

		modelBuilder.Entity<SocketBlock>()
			.HasMany(sb => sb.socketEntries)
			.WithOne()
			.HasForeignKey("SocketBlockId");

		modelBuilder.Entity<SocketBlock>()
			.HasMany(sb => sb.intrinsicSockets)
			.WithOne()
			.HasForeignKey("SocketBlockId");

		modelBuilder.Entity<SocketBlock>()
			.HasMany(sb => sb.socketCategories)
			.WithOne()
			.HasForeignKey("SocketBlockId");

		modelBuilder.Entity<SocketEntry>()
			.HasMany(se => se.reusablePlugItems)
			.WithOne()
			.HasForeignKey("SocketEntryId");

		modelBuilder.Entity<Crafting>()
			.HasMany(c => c.bonusPlugs)
			.WithOne()
			.HasForeignKey("CraftingId");

		modelBuilder.Entity<SetBlock>()
			.HasMany(sb => sb.itemList)
			.WithOne()
			.HasForeignKey("SetBlockId");

		modelBuilder.Entity<ValueBlock>()
			.HasMany(vb => vb.itemValue)
			.WithOne()
			.HasForeignKey("ValueBlockId");

		modelBuilder.Entity<Action>()
			.HasMany(a => a.requiredItems)
			.WithOne()
			.HasForeignKey("ActionId");

		modelBuilder.Entity<Action>()
			.HasMany(a => a.progressionRewards)
			.WithOne()
			.HasForeignKey("ActionId");
			
 		modelBuilder.Entity<StatsBlock>()
			.HasKey(sb => sb.statGroupHash);

		modelBuilder.Entity<StatEntry>()
			.HasKey(se => se.id);

		modelBuilder.Entity<StatsBlock>()
			.HasMany(sb => sb.statsList);
			//.WithOne(se => se.StatsBlock)
			//.HasForeignKey(se => se.StatsBlockId);
		
		/////
		// modelBuilder.Entity<TranslationBlock>()
		// 	.HasKey(tb => tb.weaponPatternHash);

		// modelBuilder.Entity<DyeReference>()
		// 	.HasKey(dr => dr.id);

		// modelBuilder.Entity<TranslationBlock>()
		// 	.HasMany(tb => tb.lockedDyes)
		// 	.WithOne(dr => dr.translationBlock)
		// 	.HasForeignKey(dr => dr.translationBlockId)
		// 	.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Quality>()
			.HasMany(q => q.versions)
			.WithOne()
			.HasForeignKey("QualityId");
	}
}
