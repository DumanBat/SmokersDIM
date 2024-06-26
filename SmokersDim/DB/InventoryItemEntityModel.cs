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
public class InventoryItemsRootObject
{
	public Dictionary<string, Item>? Items { get; set; }
}

public class Item
{
	[Key]
	public uint hash { get; set; }
	public int index { get; set; }
	public bool redacted { get; set; }
	public bool blacklisted { get; set; }
	public DisplayProperties? displayProperties { get; set; }
	public List<TooltipNotification>? tooltipNotifications { get; set; }
	public uint? collectibleHash { get; set; }
	public string? iconWatermark { get; set; }
	public string? iconWatermarkShelved { get; set; }
	public DestinyColor? backgroundColor { get; set; }
	public string? screenshot { get; set; }
	public string? itemTypeDisplayName { get; set; }
	public string? flavorText { get; set; }
	public string? uiItemDisplayStyle { get; set; }
	public string? itemTypeAndTierDisplayName { get; set; }
	public string? displaySource { get; set; }
	public Action? action { get; set; }
	public Crafting? crafting { get; set; }
	public Inventory? inventory { get; set; }
	public SetBlock? setData { get; set; }
	public StatsBlock? stats { get; set; }
	public uint? emblemObjectiveHash { get; set; }
	public EquippingBlock? equippingBlock { get; set; }
	[NotMapped]
	public TranslationBlock? translationBlock { get; set; }
	public Quality? quality { get; set; }
	public ValueBlock? value { get; set; }
	public uint acquireRewardSiteHash { get; set; }
	public uint acquireUnlockHash { get; set; }
	public SocketBlock? sockets { get; set; }
	public TalentGrid? talentGrid { get; set; }
	public List<InvestmentStat>? investmentStats { get; set; }
	public List<PerkEntry>? perks { get; set; }
	public uint? loreHash { get; set; }
	public uint? summaryItemHash { get; set; }
	public bool allowActions { get; set; }
	public bool doesPostmasterPullHaveSideEffects { get; set; }
	public bool nonTransferrable { get; set; }
	public List<uint>? itemCategoryHashes { get; set; }
	public int specialItemType { get; set; }
	public int itemType { get; set; }
	public int itemSubType { get; set; }
	public int classType { get; set; }
	public int breakerType { get; set; }
	public uint? breakerTypeHash { get; set; }
	public bool equippable { get; set; }
	public List<uint>? damageTypeHashes { get; set; }
	public List<int>? damageTypes { get; set; }
	public int defaultDamageType { get; set; }
	public uint? defaultDamageTypeHash { get; set; }
	public uint? seasonHash { get; set; }
	public bool isWrapper { get; set; }
	public List<string>? traitIds { get; set; }
	public List<uint>? traitHashes { get; set; }
}

public class SocketBlock
{
	public uint id { get; set; }
	public string? detail { get; set; }
	public List<SocketEntry>? socketEntries { get; set; }
	public List<IntrinsicSocketEntryDefinition>? intrinsicSockets { get; set; }
	public List<SocketCategory>? socketCategories { get; set; }
}

public class SocketEntry
{
	public uint id { get; set; }
	public uint socketTypeHash { get; set; }
	public uint singleInitialItemHash { get; set; }
	public List<SocketEntryPlugItem>? reusablePlugItems { get; set; }
	public bool preventInitializationOnVendorPurchase { get; set; }
	public bool hidePerksInItemTooltip { get; set; }
	public int plugSources { get; set; }
	public uint? reusablePlugSetHash { get; set; }
	public uint? randomizedPlugSetHash { get; set; }
	public bool defaultVisible { get; set; }
}

public class IntrinsicSocketEntryDefinition
{
	public uint id { get; set; }
	public uint plugItemHash { get; set; }
	public uint socketTypeHash { get; set; }
	public bool defaultVisible { get; set; }
}

public class SocketCategory
{
	public uint id { get; set; }
	public uint socketCategoryHash { get; set; }
	public List<int>? socketIndexes { get; set; }
}

public class SocketEntryPlugItem
{
	public uint id { get; set; }
	public uint plugItemHash { get; set; }
}

public class PerkEntry
{
	public uint id { get; set; }
	public string? requirementDisplayString { get; set; }
	public uint perkHash { get; set; }
	public int perkVisibility { get; set; }
}

public class TooltipNotification
{
	public uint id { get; set; }
	public string? displayString { get; set; }
	public string? displayStyle { get; set; }
}

public class DisplayProperties
{
	public uint id { get; set; }
	public string? description { get; set; }
	public string? name { get; set; }
	public string? icon { get; set; }
	public bool hasIcon { get; set; }
}

public class DestinyColor
{
	public uint id { get; set; }
	public uint colorHash { get; set; }
	public int red { get; set; }
	public int green { get; set; }
	public int blue { get; set; }
	public int alpha { get; set; }
}

public class Action
{
	public uint id { get; set; }
	public string? verbName { get; set; }
	public string? verbDescription { get; set; }
	public bool isPositive { get; set; }
	public int requiredCooldownSeconds { get; set; }
	public string? overlayScreenName { get; set; }
	public string? overlayIcon { get; set; }
	public List<RequiredItem>? requiredItems { get; set; }
	public List<ProgressionReward>? progressionRewards { get; set; }
	public string? actionTypeLabel { get; set; }
	public string? requiredLocation { get; set; }
	public uint requiredCooldownHash { get; set; }
	public uint rewardSheetHash { get; set; }
	public uint rewardItemHash { get; set; }
	public uint rewardSiteHash { get; set; }
	public bool deleteOnAction { get; set; }
	public bool consumeEntireStack { get; set; }
	public bool useOnAcquire { get; set; }
}

public class Crafting
{
	public uint id { get; set; }
	public uint outputItemHash { get; set; }
	public List<uint>? requiredSocketTypeHashes { get; set; }
	public List<string>? failedRequirementStrings { get; set; }
	public uint baseMaterialRequirements { get; set; }
	public List<CraftingBlockBonusPlug>? bonusPlugs { get; set; }
}

public class SetBlock
{
	public uint id { get; set; }
	public List<SetBlockEntry>? itemList { get; set; }
	public bool requireOrderedSetItemAdd { get; set; }
	public bool setIsFeatured { get; set; }
	public string? setType { get; set; }
	public string? questLineName { get; set; }
	public string? questLineDescription { get; set; }
	public string? questStepSummary { get; set; }
}

public class SetBlockEntry
{
	public uint id { get; set; }
	public int trackingValue { get; set; }
	public uint itemHash { get; set; }
}

public class CraftingBlockBonusPlug
{
	public uint id { get; set; }
	public uint socketTypeHash { get; set; }
	public uint plugItemHash { get; set; }
}

public class ValueBlock
{
	public uint id { get; set; }
	public List<ItemQuantity>? itemValue { get; set; }
	public string? valueDescription { get; set; }
}

public class ItemQuantity
{
	public uint id { get; set; }
	public uint itemHash { get; set; }
	public long itemInstanceId { get; set; }
	public int quantity { get; set; }
	public bool hasConditionalVisibility { get; set; }
}

public class RequiredItem
{
	public uint id { get; set; }
	public int count { get; set; }
	public uint itemHash { get; set; }
	public bool deleteOnAction { get; set; }
}

public class ProgressionReward
{
	public uint id { get; set; }
	public uint progressionMappingHash { get; set; }
	public int amount { get; set; }
	public bool applyThrottles { get; set; }
}

public class Inventory
{
	public uint id { get; set; }
	public string? stackUniqueLabel { get; set; }
	public int maxStackSize { get; set; }
	public uint bucketTypeHash { get; set; }
	public uint recoveryBucketTypeHash { get; set; }
	public uint tierTypeHash { get; set; }
	public bool isInstanceItem { get; set; }
	public bool nonTransferrableOriginal { get; set; }
	public string? tierTypeName { get; set; }
	public int tierType { get; set; }
	public string? expirationTooltip { get; set; }
	public string? expiredInActivityMessage { get; set; }
	public string? expiredInOrbitMessage { get; set; }
	public bool suppressExpirationWhenObjectivesComplete { get; set; }
	public uint recipeItemHash { get; set; }
}

public class StatsBlock
{
	[Key]
	public uint statGroupHash { get; set; }
	public bool disablePrimaryStatDisplay { get; set; }
	public bool hasDisplayableStats { get; set; }
	public uint primaryBaseStatHash { get; set; }
	
	[NotMapped]
	public Dictionary<string, StatEntry>? stats { get; set; }
	// stats for JSON deserializing, statsList for DB setup
	public ICollection<StatEntry> statsList {get; set; } = new List<StatEntry>();
}

public class StatEntry
{
	[Key]
	public int id { get; set; }
	public uint statHash { get; set; }
	public int value { get; set; }
	public int minimum { get; set; }
	public int maximum { get; set; }
	public int displayMaximum { get; set; }
	
	public uint StatsBlockId { get; set; }
}

public class EquippingBlock
{
	public uint id { get; set; }
	public uint? gearsetItemHash { get; set; }
	public string? uniqueLabel { get; set; }
	public uint uniqueLabelHash { get; set; }
	public uint equipmentSlotTypeHash { get; set; }
	public int attributes { get; set; }
	public uint equippingSoundHash { get; set; }
	public uint hornSoundHash { get; set; }
	public int ammoType { get; set; }
	public List<string>? displayStrings { get; set; }
}

public class TranslationBlock
{
	[Key]
	public string? weaponPatternIdentifier { get; set; }
	public uint weaponPatternHash { get; set; }
	public bool hasGeometry { get; set; }
	[NotMapped]
	public ICollection<DyeReference>? defaultDyes { get; set; }
	[NotMapped]
	public ICollection<DyeReference>? lockedDyes { get; set; }
	[NotMapped]
	public ICollection<DyeReference>? customDyes { get; set; }
	public List<Arrangement>? arrangements { get; set; }
}

public class DyeReference
{
	public uint id { get; set; }
	public uint channelHash { get; set; }
	public uint dyeHash { get; set; }
	public uint translationBlockId { get; set; }
	public TranslationBlock? translationBlock { get; set; }
}

public class Arrangement
{
	public uint id { get; set; }
	public uint classHash { get; set; }
	public uint artArrangementHash { get; set; }
}

public class Quality
{
	public uint id { get; set; }
	public List<int>? itemLevels { get; set; }
	public int qualityLevel { get; set; }
	public string? infusionCategoryName { get; set; }
	public uint infusionCategoryHash { get; set; }
	public List<uint>? infusionCategoryHashes { get; set; }
	public uint progressionLevelRequirementHash { get; set; }
	public uint currentVersion { get; set; }
	public List<Version>? versions { get; set; }
	public List<string>? displayVersionWatermarkIcons { get; set; }
}

public class Version
{
	public uint id { get; set; }
	public uint powerCapHash { get; set; }
}

public class TalentGrid
{
	public uint id { get; set; }
	public uint talentGridHash { get; set; }
	public string? itemDetailString { get; set; }
	public string? buildName { get; set; }
	public int hudDamageType { get; set; }
	public string? hudIcon { get; set; }
}

public class InvestmentStat
{
	public uint id { get; set; }
	public uint statTypeHash { get; set; }
	public int value { get; set; }
	public bool isConditionallyActive { get; set; }
}