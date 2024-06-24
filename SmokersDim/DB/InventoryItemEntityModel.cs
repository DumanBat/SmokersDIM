using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using Newtonsoft.Json;

// Missed from the Item definition:
// Collectible
// Objectives
// Metrics
// Plug
// Gearset
// Sack
// Summary
// Animations
public class RootObject
{
	public Dictionary<string, Item> Items { get; set; }
}

public class Item
{
	[Key]
	public uint Hash { get; set; }
	public int Index { get; set; }
	public bool Redacted { get; set; }
	public bool Blacklisted { get; set; }
	public DisplayProperties? DisplayProperties { get; set; }
	public List<TooltipNotification>? TooltipNotifications { get; set; }
	public uint? CollectibleHash { get; set; }
	public string? IconWatermark { get; set; }
	public string? IconWatermarkShelved { get; set; }
	public DestinyColor? BackgroundColor { get; set; }
	public string? Screenshot { get; set; }
	public string? ItemTypeDisplayName { get; set; }
	public string? FlavorText { get; set; }
	public string? UiItemDisplayStyle { get; set; }
	public string? ItemTypeAndTierDisplayName { get; set; }
	public string? DisplaySource { get; set; }
	public Action? Action { get; set; }
	public Crafting? Crafting { get; set; }
	public Inventory? Inventory { get; set; }
	public SetBlock? SetData { get; set; }
	public StatsBlock? Stats { get; set; }
	public uint? EmblemObjectiveHash { get; set; }
	public EquippingBlock? EquippingBlock { get; set; }
	public TranslationBlock? TranslationBlock { get; set; }
	public Quality? Quality { get; set; }
	public ValueBlock? Value { get; set; }
	public uint AcquireRewardSiteHash { get; set; }
	public uint AcquireUnlockHash { get; set; }
	public SocketBlock? Sockets { get; set; }
	public TalentGrid? TalentGrid { get; set; }
	public List<InvestmentStat>? InvestmentStats { get; set; }
	public List<PerkEntry>? Perks { get; set; }
	public uint? LoreHash { get; set; }
	public uint? SummaryItemHash { get; set; }
	public bool AllowActions { get; set; }
	public bool DoesPostmasterPullHaveSideEffects { get; set; }
	public bool NonTransferrable { get; set; }
	public List<uint>? ItemCategoryHashes { get; set; }
	public int SpecialItemType { get; set; }
	public int ItemType { get; set; }
	public int ItemSubType { get; set; }
	public int ClassType { get; set; }
	public int BreakerType { get; set; }
	public uint? BreakerTypeHash { get; set; }
	public bool Equippable { get; set; }
	public List<uint>? DamageTypeHashes { get; set; }
	public List<int>? DamageTypes { get; set; }
	public int DefaultDamageType { get; set; }
	public uint? DefaultDamageTypeHash { get; set; }
	public uint? SeasonHash { get; set; }
	public bool IsWrapper { get; set; }
	public List<string>? TraitIds { get; set; }
	public List<uint>? TraitHashes { get; set; }
}

public class SocketBlock
{
	public uint Id { get; set; }
	public string? Detail { get; set; }
	public List<SocketEntry>? SocketEntries { get; set; }
	public List<IntrinsicSocketEntryDefinition>? IntrinsicSockets { get; set; }
	public List<SocketCategory>? SocketCategories { get; set; }
}

public class SocketEntry
{
	public uint Id { get; set; }
	public uint SocketTypeHash { get; set; }
	public uint SingleInitialItemHash { get; set; }
	public List<SocketEntryPlugItem>? ReusablePlugItems { get; set; }
	public bool PreventInitializationOnVendorPurchase { get; set; }
	public bool HidePerksInItemTooltip { get; set; }
	public int PlugSources { get; set; }
	public uint? ReusablePlugSetHash { get; set; }
	public uint? RandomizedPlugSetHash { get; set; }
	public bool DefaultVisible { get; set; }
}

public class IntrinsicSocketEntryDefinition
{
	public uint Id { get; set; }
	public uint PlugItemHash { get; set; }
	public uint SocketTypeHash { get; set; }
	public bool DefaultVisible { get; set; }
}

public class SocketCategory
{
	public uint Id { get; set; }
	public uint SocketCategoryHash { get; set; }
	public List<int>? SocketIndexes { get; set; }
}

public class SocketEntryPlugItem
{
	public uint Id { get; set; }
	public uint PlugItemHash { get; set; }
}

public class PerkEntry
{
	public uint Id { get; set; }
	public string? RequirementDisplayString { get; set; }
	public uint PerkHash { get; set; }
	public int PerkVisibility { get; set; }
}

public class TooltipNotification
{
	public uint Id { get; set; }
	public string? DisplayString { get; set; }
	public string? DisplayStyle { get; set; }
}

public class DisplayProperties
{
	public uint Id { get; set; }
	public string? Description { get; set; }
	public string? Name { get; set; }
	public string? Icon { get; set; }
	public bool HasIcon { get; set; }
}

public class DestinyColor
{
	public uint Id { get; set; }
	public uint ColorHash { get; set; }
	public int Red { get; set; }
	public int Green { get; set; }
	public int Blue { get; set; }
	public int Alpha { get; set; }
}

public class Action
{
	public uint Id { get; set; }
	public string? VerbName { get; set; }
	public string? VerbDescription { get; set; }
	public bool IsPositive { get; set; }
	public int RequiredCooldownSeconds { get; set; }
	public string? OverlayScreenName { get; set; }
	public string? OverlayIcon { get; set; }
	public List<RequiredItem>? RequiredItems { get; set; }
	public List<ProgressionReward>? ProgressionRewards { get; set; }
	public string? ActionTypeLabel { get; set; }
	public string? RequiredLocation { get; set; }
	public uint RequiredCooldownHash { get; set; }
	public uint RewardSheetHash { get; set; }
	public uint RewardItemHash { get; set; }
	public uint RewardSiteHash { get; set; }
	public bool DeleteOnAction { get; set; }
	public bool ConsumeEntireStack { get; set; }
	public bool UseOnAcquire { get; set; }
}

public class Crafting
{
	public uint Id { get; set; }
	public uint OutputItemHash { get; set; }
	public List<uint>? RequiredSocketTypeHashes { get; set; }
	public List<string>? FailedRequirementStrings { get; set; }
	public uint BaseMaterialRequirements { get; set; }
	public List<CraftingBlockBonusPlug>? BonusPlugs { get; set; }
}

public class SetBlock
{
	public uint Id { get; set; }
	public List<SetBlockEntry>? ItemList { get; set; }
	public bool RequireOrderedSetItemAdd { get; set; }
	public bool SetIsFeatured { get; set; }
	public string? SetType { get; set; }
	public string? QuestLineName { get; set; }
	public string? QuestLineDescription { get; set; }
	public string? QuestStepSummary { get; set; }
}

public class SetBlockEntry
{
	public uint Id { get; set; }
	public int TrackingValue { get; set; }
	public uint ItemHash { get; set; }
}

public class CraftingBlockBonusPlug
{
	public uint Id { get; set; }
	public uint SocketTypeHash { get; set; }
	public uint PlugItemHash { get; set; }
}

public class ValueBlock
{
	public uint Id { get; set; }
	public List<ItemQuantity>? ItemValue { get; set; }
	public string? ValueDescription { get; set; }
}

public class ItemQuantity
{
	public uint Id { get; set; }
	public uint ItemHash { get; set; }
	public long ItemInstanceId { get; set; }
	public int Quantity { get; set; }
	public bool HasConditionalVisibility { get; set; }
}

public class RequiredItem
{
	public uint Id { get; set; }
	public int Count { get; set; }
	public uint ItemHash { get; set; }
	public bool DeleteOnAction { get; set; }
}

public class ProgressionReward
{
	public uint Id { get; set; }
	public uint ProgressionMappingHash { get; set; }
	public int Amount { get; set; }
	public bool ApplyThrottles { get; set; }
}

public class Inventory
{
	public uint Id { get; set; }
	public string? StackUniqueLabel { get; set; }
	public int MaxStackSize { get; set; }
	public uint BucketTypeHash { get; set; }
	public uint RecoveryBucketTypeHash { get; set; }
	public uint TierTypeHash { get; set; }
	public bool IsInstanceItem { get; set; }
	public bool NonTransferrableOriginal { get; set; }
	public string? TierTypeName { get; set; }
	public int TierType { get; set; }
	public string? ExpirationTooltip { get; set; }
	public string? ExpiredInActivityMessage { get; set; }
	public string? ExpiredInOrbitMessage { get; set; }
	public bool SuppressExpirationWhenObjectivesComplete { get; set; }
	public uint RecipeItemHash { get; set; }
}

public class StatsBlock
{
	[Key]
	public uint StatGroupHash { get; set; }
	public bool DisablePrimaryStatDisplay { get; set; }
	public bool HasDisplayableStats { get; set; }
	public uint PrimaryBaseStatHash { get; set; }
	
	public ICollection<StatEntry>? Stats { get; set; }
}

public class StatEntry
{
	[Key]
	public int Id { get; set; }
	public uint StatHash { get; set; }
	public int Value { get; set; }
	public int Minimum { get; set; }
	public int Maximum { get; set; }
	public int DisplayMaximum { get; set; }
	
	public uint StatsBlockId { get; set; }
	public StatsBlock StatsBlock { get; set; }
}

public class EquippingBlock
{
	public uint Id { get; set; }
	public uint? GearsetItemHash { get; set; }
	public string? UniqueLabel { get; set; }
	public uint UniqueLabelHash { get; set; }
	public uint EquipmentSlotTypeHash { get; set; }
	public int Attributes { get; set; }
	public uint EquippingSoundHash { get; set; }
	public uint HornSoundHash { get; set; }
	public int AmmoType { get; set; }
	public List<string>? DisplayStrings { get; set; }
}

public class TranslationBlock
{
	[Key]
	public string? WeaponPatternIdentifier { get; set; }
	public uint WeaponPatternHash { get; set; }
	public bool HasGeometry { get; set; }
	[NotMapped]
	public ICollection<DyeReference>? DefaultDyes { get; set; }
	[NotMapped]
	public ICollection<DyeReference>? LockedDyes { get; set; }
	[NotMapped]
	public ICollection<DyeReference>? CustomDyes { get; set; }
	public List<Arrangement>? Arrangements { get; set; }
}

public class DyeReference
{
	public uint Id { get; set; }
	public uint ChannelHash { get; set; }
	public uint DyeHash { get; set; }
	public uint TranslationBlockId { get; set; }
	public TranslationBlock? TranslationBlock { get; set; }
}

public class Arrangement
{
	public uint Id { get; set; }
	public uint ClassHash { get; set; }
	public uint ArtArrangementHash { get; set; }
}

public class Quality
{
	public uint Id { get; set; }
	public List<int>? ItemLevels { get; set; }
	public int QualityLevel { get; set; }
	public string? InfusionCategoryName { get; set; }
	public uint InfusionCategoryHash { get; set; }
	public List<uint>? InfusionCategoryHashes { get; set; }
	public uint ProgressionLevelRequirementHash { get; set; }
	public uint CurrentVersion { get; set; }
	public List<Version>? Versions { get; set; }
	public List<string>? DisplayVersionWatermarkIcons { get; set; }
}

public class Version
{
	public uint Id { get; set; }
	public uint PowerCapHash { get; set; }
}

public class TalentGrid
{
	public uint Id { get; set; }
	public uint TalentGridHash { get; set; }
	public string? ItemDetailString { get; set; }
	public string? BuildName { get; set; }
	public int HudDamageType { get; set; }
	public string? HudIcon { get; set; }
}

public class InvestmentStat
{
	public uint Id { get; set; }
	public uint StatTypeHash { get; set; }
	public int Value { get; set; }
	public bool IsConditionallyActive { get; set; }
}