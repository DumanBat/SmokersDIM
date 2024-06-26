using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmokersDim.Migrations
{
    /// <inheritdoc />
    public partial class CaseUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrangements_TranslationBlocks_TranslationBlockWeaponPatter~",
                table: "Arrangements");

            migrationBuilder.DropForeignKey(
                name: "FK_DyeReferences_TranslationBlocks_TranslationBlockId",
                table: "DyeReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_StatEntries_StatsBlocks_StatsBlockId",
                table: "StatEntries");

            migrationBuilder.RenameColumn(
                name: "PowerCapHash",
                table: "Versions",
                newName: "powerCapHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Versions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ValueDescription",
                table: "ValueBlocks",
                newName: "valueDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ValueBlocks",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WeaponPatternIdentifier",
                table: "TranslationBlocks",
                newName: "weaponPatternIdentifier");

            migrationBuilder.RenameColumn(
                name: "HasGeometry",
                table: "TranslationBlocks",
                newName: "hasGeometry");

            migrationBuilder.RenameColumn(
                name: "WeaponPatternHash",
                table: "TranslationBlocks",
                newName: "weaponPatternHash");

            migrationBuilder.RenameColumn(
                name: "DisplayStyle",
                table: "TooltipNotifications",
                newName: "displayStyle");

            migrationBuilder.RenameColumn(
                name: "DisplayString",
                table: "TooltipNotifications",
                newName: "displayString");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TooltipNotifications",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TalentGridHash",
                table: "TalentGrids",
                newName: "talentGridHash");

            migrationBuilder.RenameColumn(
                name: "ItemDetailString",
                table: "TalentGrids",
                newName: "itemDetailString");

            migrationBuilder.RenameColumn(
                name: "HudIcon",
                table: "TalentGrids",
                newName: "hudIcon");

            migrationBuilder.RenameColumn(
                name: "HudDamageType",
                table: "TalentGrids",
                newName: "hudDamageType");

            migrationBuilder.RenameColumn(
                name: "BuildName",
                table: "TalentGrids",
                newName: "buildName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TalentGrids",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PrimaryBaseStatHash",
                table: "StatsBlocks",
                newName: "primaryBaseStatHash");

            migrationBuilder.RenameColumn(
                name: "HasDisplayableStats",
                table: "StatsBlocks",
                newName: "hasDisplayableStats");

            migrationBuilder.RenameColumn(
                name: "DisablePrimaryStatDisplay",
                table: "StatsBlocks",
                newName: "disablePrimaryStatDisplay");

            migrationBuilder.RenameColumn(
                name: "StatGroupHash",
                table: "StatsBlocks",
                newName: "statGroupHash");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "StatEntries",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "StatsBlockId",
                table: "StatEntries",
                newName: "statsBlockId");

            migrationBuilder.RenameColumn(
                name: "StatHash",
                table: "StatEntries",
                newName: "statHash");

            migrationBuilder.RenameColumn(
                name: "Minimum",
                table: "StatEntries",
                newName: "minimum");

            migrationBuilder.RenameColumn(
                name: "Maximum",
                table: "StatEntries",
                newName: "maximum");

            migrationBuilder.RenameColumn(
                name: "DisplayMaximum",
                table: "StatEntries",
                newName: "displayMaximum");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StatEntries",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_StatEntries_StatsBlockId",
                table: "StatEntries",
                newName: "IX_StatEntries_statsBlockId");

            migrationBuilder.RenameColumn(
                name: "PlugItemHash",
                table: "SocketEntryPlugItems",
                newName: "plugItemHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SocketEntryPlugItems",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SocketTypeHash",
                table: "SocketEntries",
                newName: "socketTypeHash");

            migrationBuilder.RenameColumn(
                name: "SingleInitialItemHash",
                table: "SocketEntries",
                newName: "singleInitialItemHash");

            migrationBuilder.RenameColumn(
                name: "ReusablePlugSetHash",
                table: "SocketEntries",
                newName: "reusablePlugSetHash");

            migrationBuilder.RenameColumn(
                name: "RandomizedPlugSetHash",
                table: "SocketEntries",
                newName: "randomizedPlugSetHash");

            migrationBuilder.RenameColumn(
                name: "PreventInitializationOnVendorPurchase",
                table: "SocketEntries",
                newName: "preventInitializationOnVendorPurchase");

            migrationBuilder.RenameColumn(
                name: "PlugSources",
                table: "SocketEntries",
                newName: "plugSources");

            migrationBuilder.RenameColumn(
                name: "HidePerksInItemTooltip",
                table: "SocketEntries",
                newName: "hidePerksInItemTooltip");

            migrationBuilder.RenameColumn(
                name: "DefaultVisible",
                table: "SocketEntries",
                newName: "defaultVisible");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SocketEntries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SocketIndexes",
                table: "SocketCategories",
                newName: "socketIndexes");

            migrationBuilder.RenameColumn(
                name: "SocketCategoryHash",
                table: "SocketCategories",
                newName: "socketCategoryHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SocketCategories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Detail",
                table: "SocketBlocks",
                newName: "detail");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SocketBlocks",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SetType",
                table: "SetBlocks",
                newName: "setType");

            migrationBuilder.RenameColumn(
                name: "SetIsFeatured",
                table: "SetBlocks",
                newName: "setIsFeatured");

            migrationBuilder.RenameColumn(
                name: "RequireOrderedSetItemAdd",
                table: "SetBlocks",
                newName: "requireOrderedSetItemAdd");

            migrationBuilder.RenameColumn(
                name: "QuestStepSummary",
                table: "SetBlocks",
                newName: "questStepSummary");

            migrationBuilder.RenameColumn(
                name: "QuestLineName",
                table: "SetBlocks",
                newName: "questLineName");

            migrationBuilder.RenameColumn(
                name: "QuestLineDescription",
                table: "SetBlocks",
                newName: "questLineDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SetBlocks",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TrackingValue",
                table: "SetBlockEntries",
                newName: "trackingValue");

            migrationBuilder.RenameColumn(
                name: "ItemHash",
                table: "SetBlockEntries",
                newName: "itemHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SetBlockEntries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ItemHash",
                table: "RequiredItems",
                newName: "itemHash");

            migrationBuilder.RenameColumn(
                name: "DeleteOnAction",
                table: "RequiredItems",
                newName: "deleteOnAction");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "RequiredItems",
                newName: "count");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RequiredItems",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "QualityLevel",
                table: "Qualities",
                newName: "qualityLevel");

            migrationBuilder.RenameColumn(
                name: "ProgressionLevelRequirementHash",
                table: "Qualities",
                newName: "progressionLevelRequirementHash");

            migrationBuilder.RenameColumn(
                name: "ItemLevels",
                table: "Qualities",
                newName: "itemLevels");

            migrationBuilder.RenameColumn(
                name: "InfusionCategoryName",
                table: "Qualities",
                newName: "infusionCategoryName");

            migrationBuilder.RenameColumn(
                name: "InfusionCategoryHashes",
                table: "Qualities",
                newName: "infusionCategoryHashes");

            migrationBuilder.RenameColumn(
                name: "InfusionCategoryHash",
                table: "Qualities",
                newName: "infusionCategoryHash");

            migrationBuilder.RenameColumn(
                name: "DisplayVersionWatermarkIcons",
                table: "Qualities",
                newName: "displayVersionWatermarkIcons");

            migrationBuilder.RenameColumn(
                name: "CurrentVersion",
                table: "Qualities",
                newName: "currentVersion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Qualities",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProgressionMappingHash",
                table: "ProgressionRewards",
                newName: "progressionMappingHash");

            migrationBuilder.RenameColumn(
                name: "ApplyThrottles",
                table: "ProgressionRewards",
                newName: "applyThrottles");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ProgressionRewards",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProgressionRewards",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RequirementDisplayString",
                table: "PerkEntries",
                newName: "requirementDisplayString");

            migrationBuilder.RenameColumn(
                name: "PerkVisibility",
                table: "PerkEntries",
                newName: "perkVisibility");

            migrationBuilder.RenameColumn(
                name: "PerkHash",
                table: "PerkEntries",
                newName: "perkHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PerkEntries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UiItemDisplayStyle",
                table: "Items",
                newName: "uiItemDisplayStyle");

            migrationBuilder.RenameColumn(
                name: "TraitIds",
                table: "Items",
                newName: "traitIds");

            migrationBuilder.RenameColumn(
                name: "TraitHashes",
                table: "Items",
                newName: "traitHashes");

            migrationBuilder.RenameColumn(
                name: "SummaryItemHash",
                table: "Items",
                newName: "summaryItemHash");

            migrationBuilder.RenameColumn(
                name: "SpecialItemType",
                table: "Items",
                newName: "specialItemType");

            migrationBuilder.RenameColumn(
                name: "SeasonHash",
                table: "Items",
                newName: "seasonHash");

            migrationBuilder.RenameColumn(
                name: "Screenshot",
                table: "Items",
                newName: "screenshot");

            migrationBuilder.RenameColumn(
                name: "Redacted",
                table: "Items",
                newName: "redacted");

            migrationBuilder.RenameColumn(
                name: "NonTransferrable",
                table: "Items",
                newName: "nonTransferrable");

            migrationBuilder.RenameColumn(
                name: "LoreHash",
                table: "Items",
                newName: "loreHash");

            migrationBuilder.RenameColumn(
                name: "ItemTypeDisplayName",
                table: "Items",
                newName: "itemTypeDisplayName");

            migrationBuilder.RenameColumn(
                name: "ItemTypeAndTierDisplayName",
                table: "Items",
                newName: "itemTypeAndTierDisplayName");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Items",
                newName: "itemType");

            migrationBuilder.RenameColumn(
                name: "ItemSubType",
                table: "Items",
                newName: "itemSubType");

            migrationBuilder.RenameColumn(
                name: "ItemCategoryHashes",
                table: "Items",
                newName: "itemCategoryHashes");

            migrationBuilder.RenameColumn(
                name: "IsWrapper",
                table: "Items",
                newName: "isWrapper");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "Items",
                newName: "index");

            migrationBuilder.RenameColumn(
                name: "IconWatermarkShelved",
                table: "Items",
                newName: "iconWatermarkShelved");

            migrationBuilder.RenameColumn(
                name: "IconWatermark",
                table: "Items",
                newName: "iconWatermark");

            migrationBuilder.RenameColumn(
                name: "FlavorText",
                table: "Items",
                newName: "flavorText");

            migrationBuilder.RenameColumn(
                name: "Equippable",
                table: "Items",
                newName: "equippable");

            migrationBuilder.RenameColumn(
                name: "EmblemObjectiveHash",
                table: "Items",
                newName: "emblemObjectiveHash");

            migrationBuilder.RenameColumn(
                name: "DoesPostmasterPullHaveSideEffects",
                table: "Items",
                newName: "doesPostmasterPullHaveSideEffects");

            migrationBuilder.RenameColumn(
                name: "DisplaySource",
                table: "Items",
                newName: "displaySource");

            migrationBuilder.RenameColumn(
                name: "DefaultDamageTypeHash",
                table: "Items",
                newName: "defaultDamageTypeHash");

            migrationBuilder.RenameColumn(
                name: "DefaultDamageType",
                table: "Items",
                newName: "defaultDamageType");

            migrationBuilder.RenameColumn(
                name: "DamageTypes",
                table: "Items",
                newName: "damageTypes");

            migrationBuilder.RenameColumn(
                name: "DamageTypeHashes",
                table: "Items",
                newName: "damageTypeHashes");

            migrationBuilder.RenameColumn(
                name: "CollectibleHash",
                table: "Items",
                newName: "collectibleHash");

            migrationBuilder.RenameColumn(
                name: "ClassType",
                table: "Items",
                newName: "classType");

            migrationBuilder.RenameColumn(
                name: "BreakerTypeHash",
                table: "Items",
                newName: "breakerTypeHash");

            migrationBuilder.RenameColumn(
                name: "BreakerType",
                table: "Items",
                newName: "breakerType");

            migrationBuilder.RenameColumn(
                name: "Blacklisted",
                table: "Items",
                newName: "blacklisted");

            migrationBuilder.RenameColumn(
                name: "AllowActions",
                table: "Items",
                newName: "allowActions");

            migrationBuilder.RenameColumn(
                name: "AcquireUnlockHash",
                table: "Items",
                newName: "acquireUnlockHash");

            migrationBuilder.RenameColumn(
                name: "AcquireRewardSiteHash",
                table: "Items",
                newName: "acquireRewardSiteHash");

            migrationBuilder.RenameColumn(
                name: "Hash",
                table: "Items",
                newName: "hash");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ItemQuantities",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ItemInstanceId",
                table: "ItemQuantities",
                newName: "itemInstanceId");

            migrationBuilder.RenameColumn(
                name: "ItemHash",
                table: "ItemQuantities",
                newName: "itemHash");

            migrationBuilder.RenameColumn(
                name: "HasConditionalVisibility",
                table: "ItemQuantities",
                newName: "hasConditionalVisibility");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemQuantities",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "InvestmentStats",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "StatTypeHash",
                table: "InvestmentStats",
                newName: "statTypeHash");

            migrationBuilder.RenameColumn(
                name: "IsConditionallyActive",
                table: "InvestmentStats",
                newName: "isConditionallyActive");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InvestmentStats",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TierTypeName",
                table: "Inventories",
                newName: "tierTypeName");

            migrationBuilder.RenameColumn(
                name: "TierTypeHash",
                table: "Inventories",
                newName: "tierTypeHash");

            migrationBuilder.RenameColumn(
                name: "TierType",
                table: "Inventories",
                newName: "tierType");

            migrationBuilder.RenameColumn(
                name: "SuppressExpirationWhenObjectivesComplete",
                table: "Inventories",
                newName: "suppressExpirationWhenObjectivesComplete");

            migrationBuilder.RenameColumn(
                name: "StackUniqueLabel",
                table: "Inventories",
                newName: "stackUniqueLabel");

            migrationBuilder.RenameColumn(
                name: "RecoveryBucketTypeHash",
                table: "Inventories",
                newName: "recoveryBucketTypeHash");

            migrationBuilder.RenameColumn(
                name: "RecipeItemHash",
                table: "Inventories",
                newName: "recipeItemHash");

            migrationBuilder.RenameColumn(
                name: "NonTransferrableOriginal",
                table: "Inventories",
                newName: "nonTransferrableOriginal");

            migrationBuilder.RenameColumn(
                name: "MaxStackSize",
                table: "Inventories",
                newName: "maxStackSize");

            migrationBuilder.RenameColumn(
                name: "IsInstanceItem",
                table: "Inventories",
                newName: "isInstanceItem");

            migrationBuilder.RenameColumn(
                name: "ExpiredInOrbitMessage",
                table: "Inventories",
                newName: "expiredInOrbitMessage");

            migrationBuilder.RenameColumn(
                name: "ExpiredInActivityMessage",
                table: "Inventories",
                newName: "expiredInActivityMessage");

            migrationBuilder.RenameColumn(
                name: "ExpirationTooltip",
                table: "Inventories",
                newName: "expirationTooltip");

            migrationBuilder.RenameColumn(
                name: "BucketTypeHash",
                table: "Inventories",
                newName: "bucketTypeHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Inventories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SocketTypeHash",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "socketTypeHash");

            migrationBuilder.RenameColumn(
                name: "PlugItemHash",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "plugItemHash");

            migrationBuilder.RenameColumn(
                name: "DefaultVisible",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "defaultVisible");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UniqueLabelHash",
                table: "EquippingBlocks",
                newName: "uniqueLabelHash");

            migrationBuilder.RenameColumn(
                name: "UniqueLabel",
                table: "EquippingBlocks",
                newName: "uniqueLabel");

            migrationBuilder.RenameColumn(
                name: "HornSoundHash",
                table: "EquippingBlocks",
                newName: "hornSoundHash");

            migrationBuilder.RenameColumn(
                name: "GearsetItemHash",
                table: "EquippingBlocks",
                newName: "gearsetItemHash");

            migrationBuilder.RenameColumn(
                name: "EquippingSoundHash",
                table: "EquippingBlocks",
                newName: "equippingSoundHash");

            migrationBuilder.RenameColumn(
                name: "EquipmentSlotTypeHash",
                table: "EquippingBlocks",
                newName: "equipmentSlotTypeHash");

            migrationBuilder.RenameColumn(
                name: "DisplayStrings",
                table: "EquippingBlocks",
                newName: "displayStrings");

            migrationBuilder.RenameColumn(
                name: "Attributes",
                table: "EquippingBlocks",
                newName: "attributes");

            migrationBuilder.RenameColumn(
                name: "AmmoType",
                table: "EquippingBlocks",
                newName: "ammoType");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EquippingBlocks",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TranslationBlockId",
                table: "DyeReferences",
                newName: "translationBlockId");

            migrationBuilder.RenameColumn(
                name: "DyeHash",
                table: "DyeReferences",
                newName: "dyeHash");

            migrationBuilder.RenameColumn(
                name: "ChannelHash",
                table: "DyeReferences",
                newName: "channelHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DyeReferences",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_DyeReferences_TranslationBlockId",
                table: "DyeReferences",
                newName: "IX_DyeReferences_translationBlockId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DisplayProperties",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "DisplayProperties",
                newName: "icon");

            migrationBuilder.RenameColumn(
                name: "HasIcon",
                table: "DisplayProperties",
                newName: "hasIcon");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DisplayProperties",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DisplayProperties",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Red",
                table: "DestinyColors",
                newName: "red");

            migrationBuilder.RenameColumn(
                name: "Green",
                table: "DestinyColors",
                newName: "green");

            migrationBuilder.RenameColumn(
                name: "ColorHash",
                table: "DestinyColors",
                newName: "colorHash");

            migrationBuilder.RenameColumn(
                name: "Blue",
                table: "DestinyColors",
                newName: "blue");

            migrationBuilder.RenameColumn(
                name: "Alpha",
                table: "DestinyColors",
                newName: "alpha");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DestinyColors",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RequiredSocketTypeHashes",
                table: "Craftings",
                newName: "requiredSocketTypeHashes");

            migrationBuilder.RenameColumn(
                name: "OutputItemHash",
                table: "Craftings",
                newName: "outputItemHash");

            migrationBuilder.RenameColumn(
                name: "FailedRequirementStrings",
                table: "Craftings",
                newName: "failedRequirementStrings");

            migrationBuilder.RenameColumn(
                name: "BaseMaterialRequirements",
                table: "Craftings",
                newName: "baseMaterialRequirements");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Craftings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SocketTypeHash",
                table: "CraftingBlockBonusPlugs",
                newName: "socketTypeHash");

            migrationBuilder.RenameColumn(
                name: "PlugItemHash",
                table: "CraftingBlockBonusPlugs",
                newName: "plugItemHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CraftingBlockBonusPlugs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TranslationBlockWeaponPatternHash",
                table: "Arrangements",
                newName: "TranslationBlockweaponPatternHash");

            migrationBuilder.RenameColumn(
                name: "ClassHash",
                table: "Arrangements",
                newName: "classHash");

            migrationBuilder.RenameColumn(
                name: "ArtArrangementHash",
                table: "Arrangements",
                newName: "artArrangementHash");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Arrangements",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Arrangements_TranslationBlockWeaponPatternHash",
                table: "Arrangements",
                newName: "IX_Arrangements_TranslationBlockweaponPatternHash");

            migrationBuilder.RenameColumn(
                name: "VerbName",
                table: "Actions",
                newName: "verbName");

            migrationBuilder.RenameColumn(
                name: "VerbDescription",
                table: "Actions",
                newName: "verbDescription");

            migrationBuilder.RenameColumn(
                name: "UseOnAcquire",
                table: "Actions",
                newName: "useOnAcquire");

            migrationBuilder.RenameColumn(
                name: "RewardSiteHash",
                table: "Actions",
                newName: "rewardSiteHash");

            migrationBuilder.RenameColumn(
                name: "RewardSheetHash",
                table: "Actions",
                newName: "rewardSheetHash");

            migrationBuilder.RenameColumn(
                name: "RewardItemHash",
                table: "Actions",
                newName: "rewardItemHash");

            migrationBuilder.RenameColumn(
                name: "RequiredLocation",
                table: "Actions",
                newName: "requiredLocation");

            migrationBuilder.RenameColumn(
                name: "RequiredCooldownSeconds",
                table: "Actions",
                newName: "requiredCooldownSeconds");

            migrationBuilder.RenameColumn(
                name: "RequiredCooldownHash",
                table: "Actions",
                newName: "requiredCooldownHash");

            migrationBuilder.RenameColumn(
                name: "OverlayScreenName",
                table: "Actions",
                newName: "overlayScreenName");

            migrationBuilder.RenameColumn(
                name: "OverlayIcon",
                table: "Actions",
                newName: "overlayIcon");

            migrationBuilder.RenameColumn(
                name: "IsPositive",
                table: "Actions",
                newName: "isPositive");

            migrationBuilder.RenameColumn(
                name: "DeleteOnAction",
                table: "Actions",
                newName: "deleteOnAction");

            migrationBuilder.RenameColumn(
                name: "ConsumeEntireStack",
                table: "Actions",
                newName: "consumeEntireStack");

            migrationBuilder.RenameColumn(
                name: "ActionTypeLabel",
                table: "Actions",
                newName: "actionTypeLabel");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Actions",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrangements_TranslationBlocks_TranslationBlockweaponPatter~",
                table: "Arrangements",
                column: "TranslationBlockweaponPatternHash",
                principalTable: "TranslationBlocks",
                principalColumn: "weaponPatternHash");

            migrationBuilder.AddForeignKey(
                name: "FK_DyeReferences_TranslationBlocks_translationBlockId",
                table: "DyeReferences",
                column: "translationBlockId",
                principalTable: "TranslationBlocks",
                principalColumn: "weaponPatternHash",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StatEntries_StatsBlocks_statsBlockId",
                table: "StatEntries",
                column: "statsBlockId",
                principalTable: "StatsBlocks",
                principalColumn: "statGroupHash",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrangements_TranslationBlocks_TranslationBlockweaponPatter~",
                table: "Arrangements");

            migrationBuilder.DropForeignKey(
                name: "FK_DyeReferences_TranslationBlocks_translationBlockId",
                table: "DyeReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_StatEntries_StatsBlocks_statsBlockId",
                table: "StatEntries");

            migrationBuilder.RenameColumn(
                name: "powerCapHash",
                table: "Versions",
                newName: "PowerCapHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Versions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "valueDescription",
                table: "ValueBlocks",
                newName: "ValueDescription");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ValueBlocks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "weaponPatternIdentifier",
                table: "TranslationBlocks",
                newName: "WeaponPatternIdentifier");

            migrationBuilder.RenameColumn(
                name: "hasGeometry",
                table: "TranslationBlocks",
                newName: "HasGeometry");

            migrationBuilder.RenameColumn(
                name: "weaponPatternHash",
                table: "TranslationBlocks",
                newName: "WeaponPatternHash");

            migrationBuilder.RenameColumn(
                name: "displayStyle",
                table: "TooltipNotifications",
                newName: "DisplayStyle");

            migrationBuilder.RenameColumn(
                name: "displayString",
                table: "TooltipNotifications",
                newName: "DisplayString");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TooltipNotifications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "talentGridHash",
                table: "TalentGrids",
                newName: "TalentGridHash");

            migrationBuilder.RenameColumn(
                name: "itemDetailString",
                table: "TalentGrids",
                newName: "ItemDetailString");

            migrationBuilder.RenameColumn(
                name: "hudIcon",
                table: "TalentGrids",
                newName: "HudIcon");

            migrationBuilder.RenameColumn(
                name: "hudDamageType",
                table: "TalentGrids",
                newName: "HudDamageType");

            migrationBuilder.RenameColumn(
                name: "buildName",
                table: "TalentGrids",
                newName: "BuildName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TalentGrids",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "primaryBaseStatHash",
                table: "StatsBlocks",
                newName: "PrimaryBaseStatHash");

            migrationBuilder.RenameColumn(
                name: "hasDisplayableStats",
                table: "StatsBlocks",
                newName: "HasDisplayableStats");

            migrationBuilder.RenameColumn(
                name: "disablePrimaryStatDisplay",
                table: "StatsBlocks",
                newName: "DisablePrimaryStatDisplay");

            migrationBuilder.RenameColumn(
                name: "statGroupHash",
                table: "StatsBlocks",
                newName: "StatGroupHash");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "StatEntries",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "statsBlockId",
                table: "StatEntries",
                newName: "StatsBlockId");

            migrationBuilder.RenameColumn(
                name: "statHash",
                table: "StatEntries",
                newName: "StatHash");

            migrationBuilder.RenameColumn(
                name: "minimum",
                table: "StatEntries",
                newName: "Minimum");

            migrationBuilder.RenameColumn(
                name: "maximum",
                table: "StatEntries",
                newName: "Maximum");

            migrationBuilder.RenameColumn(
                name: "displayMaximum",
                table: "StatEntries",
                newName: "DisplayMaximum");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StatEntries",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_StatEntries_statsBlockId",
                table: "StatEntries",
                newName: "IX_StatEntries_StatsBlockId");

            migrationBuilder.RenameColumn(
                name: "plugItemHash",
                table: "SocketEntryPlugItems",
                newName: "PlugItemHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SocketEntryPlugItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "socketTypeHash",
                table: "SocketEntries",
                newName: "SocketTypeHash");

            migrationBuilder.RenameColumn(
                name: "singleInitialItemHash",
                table: "SocketEntries",
                newName: "SingleInitialItemHash");

            migrationBuilder.RenameColumn(
                name: "reusablePlugSetHash",
                table: "SocketEntries",
                newName: "ReusablePlugSetHash");

            migrationBuilder.RenameColumn(
                name: "randomizedPlugSetHash",
                table: "SocketEntries",
                newName: "RandomizedPlugSetHash");

            migrationBuilder.RenameColumn(
                name: "preventInitializationOnVendorPurchase",
                table: "SocketEntries",
                newName: "PreventInitializationOnVendorPurchase");

            migrationBuilder.RenameColumn(
                name: "plugSources",
                table: "SocketEntries",
                newName: "PlugSources");

            migrationBuilder.RenameColumn(
                name: "hidePerksInItemTooltip",
                table: "SocketEntries",
                newName: "HidePerksInItemTooltip");

            migrationBuilder.RenameColumn(
                name: "defaultVisible",
                table: "SocketEntries",
                newName: "DefaultVisible");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SocketEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "socketIndexes",
                table: "SocketCategories",
                newName: "SocketIndexes");

            migrationBuilder.RenameColumn(
                name: "socketCategoryHash",
                table: "SocketCategories",
                newName: "SocketCategoryHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SocketCategories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "detail",
                table: "SocketBlocks",
                newName: "Detail");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SocketBlocks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "setType",
                table: "SetBlocks",
                newName: "SetType");

            migrationBuilder.RenameColumn(
                name: "setIsFeatured",
                table: "SetBlocks",
                newName: "SetIsFeatured");

            migrationBuilder.RenameColumn(
                name: "requireOrderedSetItemAdd",
                table: "SetBlocks",
                newName: "RequireOrderedSetItemAdd");

            migrationBuilder.RenameColumn(
                name: "questStepSummary",
                table: "SetBlocks",
                newName: "QuestStepSummary");

            migrationBuilder.RenameColumn(
                name: "questLineName",
                table: "SetBlocks",
                newName: "QuestLineName");

            migrationBuilder.RenameColumn(
                name: "questLineDescription",
                table: "SetBlocks",
                newName: "QuestLineDescription");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SetBlocks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "trackingValue",
                table: "SetBlockEntries",
                newName: "TrackingValue");

            migrationBuilder.RenameColumn(
                name: "itemHash",
                table: "SetBlockEntries",
                newName: "ItemHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SetBlockEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "itemHash",
                table: "RequiredItems",
                newName: "ItemHash");

            migrationBuilder.RenameColumn(
                name: "deleteOnAction",
                table: "RequiredItems",
                newName: "DeleteOnAction");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "RequiredItems",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RequiredItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "qualityLevel",
                table: "Qualities",
                newName: "QualityLevel");

            migrationBuilder.RenameColumn(
                name: "progressionLevelRequirementHash",
                table: "Qualities",
                newName: "ProgressionLevelRequirementHash");

            migrationBuilder.RenameColumn(
                name: "itemLevels",
                table: "Qualities",
                newName: "ItemLevels");

            migrationBuilder.RenameColumn(
                name: "infusionCategoryName",
                table: "Qualities",
                newName: "InfusionCategoryName");

            migrationBuilder.RenameColumn(
                name: "infusionCategoryHashes",
                table: "Qualities",
                newName: "InfusionCategoryHashes");

            migrationBuilder.RenameColumn(
                name: "infusionCategoryHash",
                table: "Qualities",
                newName: "InfusionCategoryHash");

            migrationBuilder.RenameColumn(
                name: "displayVersionWatermarkIcons",
                table: "Qualities",
                newName: "DisplayVersionWatermarkIcons");

            migrationBuilder.RenameColumn(
                name: "currentVersion",
                table: "Qualities",
                newName: "CurrentVersion");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Qualities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "progressionMappingHash",
                table: "ProgressionRewards",
                newName: "ProgressionMappingHash");

            migrationBuilder.RenameColumn(
                name: "applyThrottles",
                table: "ProgressionRewards",
                newName: "ApplyThrottles");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "ProgressionRewards",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProgressionRewards",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "requirementDisplayString",
                table: "PerkEntries",
                newName: "RequirementDisplayString");

            migrationBuilder.RenameColumn(
                name: "perkVisibility",
                table: "PerkEntries",
                newName: "PerkVisibility");

            migrationBuilder.RenameColumn(
                name: "perkHash",
                table: "PerkEntries",
                newName: "PerkHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PerkEntries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "uiItemDisplayStyle",
                table: "Items",
                newName: "UiItemDisplayStyle");

            migrationBuilder.RenameColumn(
                name: "traitIds",
                table: "Items",
                newName: "TraitIds");

            migrationBuilder.RenameColumn(
                name: "traitHashes",
                table: "Items",
                newName: "TraitHashes");

            migrationBuilder.RenameColumn(
                name: "summaryItemHash",
                table: "Items",
                newName: "SummaryItemHash");

            migrationBuilder.RenameColumn(
                name: "specialItemType",
                table: "Items",
                newName: "SpecialItemType");

            migrationBuilder.RenameColumn(
                name: "seasonHash",
                table: "Items",
                newName: "SeasonHash");

            migrationBuilder.RenameColumn(
                name: "screenshot",
                table: "Items",
                newName: "Screenshot");

            migrationBuilder.RenameColumn(
                name: "redacted",
                table: "Items",
                newName: "Redacted");

            migrationBuilder.RenameColumn(
                name: "nonTransferrable",
                table: "Items",
                newName: "NonTransferrable");

            migrationBuilder.RenameColumn(
                name: "loreHash",
                table: "Items",
                newName: "LoreHash");

            migrationBuilder.RenameColumn(
                name: "itemTypeDisplayName",
                table: "Items",
                newName: "ItemTypeDisplayName");

            migrationBuilder.RenameColumn(
                name: "itemTypeAndTierDisplayName",
                table: "Items",
                newName: "ItemTypeAndTierDisplayName");

            migrationBuilder.RenameColumn(
                name: "itemType",
                table: "Items",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "itemSubType",
                table: "Items",
                newName: "ItemSubType");

            migrationBuilder.RenameColumn(
                name: "itemCategoryHashes",
                table: "Items",
                newName: "ItemCategoryHashes");

            migrationBuilder.RenameColumn(
                name: "isWrapper",
                table: "Items",
                newName: "IsWrapper");

            migrationBuilder.RenameColumn(
                name: "index",
                table: "Items",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "iconWatermarkShelved",
                table: "Items",
                newName: "IconWatermarkShelved");

            migrationBuilder.RenameColumn(
                name: "iconWatermark",
                table: "Items",
                newName: "IconWatermark");

            migrationBuilder.RenameColumn(
                name: "flavorText",
                table: "Items",
                newName: "FlavorText");

            migrationBuilder.RenameColumn(
                name: "equippable",
                table: "Items",
                newName: "Equippable");

            migrationBuilder.RenameColumn(
                name: "emblemObjectiveHash",
                table: "Items",
                newName: "EmblemObjectiveHash");

            migrationBuilder.RenameColumn(
                name: "doesPostmasterPullHaveSideEffects",
                table: "Items",
                newName: "DoesPostmasterPullHaveSideEffects");

            migrationBuilder.RenameColumn(
                name: "displaySource",
                table: "Items",
                newName: "DisplaySource");

            migrationBuilder.RenameColumn(
                name: "defaultDamageTypeHash",
                table: "Items",
                newName: "DefaultDamageTypeHash");

            migrationBuilder.RenameColumn(
                name: "defaultDamageType",
                table: "Items",
                newName: "DefaultDamageType");

            migrationBuilder.RenameColumn(
                name: "damageTypes",
                table: "Items",
                newName: "DamageTypes");

            migrationBuilder.RenameColumn(
                name: "damageTypeHashes",
                table: "Items",
                newName: "DamageTypeHashes");

            migrationBuilder.RenameColumn(
                name: "collectibleHash",
                table: "Items",
                newName: "CollectibleHash");

            migrationBuilder.RenameColumn(
                name: "classType",
                table: "Items",
                newName: "ClassType");

            migrationBuilder.RenameColumn(
                name: "breakerTypeHash",
                table: "Items",
                newName: "BreakerTypeHash");

            migrationBuilder.RenameColumn(
                name: "breakerType",
                table: "Items",
                newName: "BreakerType");

            migrationBuilder.RenameColumn(
                name: "blacklisted",
                table: "Items",
                newName: "Blacklisted");

            migrationBuilder.RenameColumn(
                name: "allowActions",
                table: "Items",
                newName: "AllowActions");

            migrationBuilder.RenameColumn(
                name: "acquireUnlockHash",
                table: "Items",
                newName: "AcquireUnlockHash");

            migrationBuilder.RenameColumn(
                name: "acquireRewardSiteHash",
                table: "Items",
                newName: "AcquireRewardSiteHash");

            migrationBuilder.RenameColumn(
                name: "hash",
                table: "Items",
                newName: "Hash");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "ItemQuantities",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "itemInstanceId",
                table: "ItemQuantities",
                newName: "ItemInstanceId");

            migrationBuilder.RenameColumn(
                name: "itemHash",
                table: "ItemQuantities",
                newName: "ItemHash");

            migrationBuilder.RenameColumn(
                name: "hasConditionalVisibility",
                table: "ItemQuantities",
                newName: "HasConditionalVisibility");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ItemQuantities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "InvestmentStats",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "statTypeHash",
                table: "InvestmentStats",
                newName: "StatTypeHash");

            migrationBuilder.RenameColumn(
                name: "isConditionallyActive",
                table: "InvestmentStats",
                newName: "IsConditionallyActive");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "InvestmentStats",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tierTypeName",
                table: "Inventories",
                newName: "TierTypeName");

            migrationBuilder.RenameColumn(
                name: "tierTypeHash",
                table: "Inventories",
                newName: "TierTypeHash");

            migrationBuilder.RenameColumn(
                name: "tierType",
                table: "Inventories",
                newName: "TierType");

            migrationBuilder.RenameColumn(
                name: "suppressExpirationWhenObjectivesComplete",
                table: "Inventories",
                newName: "SuppressExpirationWhenObjectivesComplete");

            migrationBuilder.RenameColumn(
                name: "stackUniqueLabel",
                table: "Inventories",
                newName: "StackUniqueLabel");

            migrationBuilder.RenameColumn(
                name: "recoveryBucketTypeHash",
                table: "Inventories",
                newName: "RecoveryBucketTypeHash");

            migrationBuilder.RenameColumn(
                name: "recipeItemHash",
                table: "Inventories",
                newName: "RecipeItemHash");

            migrationBuilder.RenameColumn(
                name: "nonTransferrableOriginal",
                table: "Inventories",
                newName: "NonTransferrableOriginal");

            migrationBuilder.RenameColumn(
                name: "maxStackSize",
                table: "Inventories",
                newName: "MaxStackSize");

            migrationBuilder.RenameColumn(
                name: "isInstanceItem",
                table: "Inventories",
                newName: "IsInstanceItem");

            migrationBuilder.RenameColumn(
                name: "expiredInOrbitMessage",
                table: "Inventories",
                newName: "ExpiredInOrbitMessage");

            migrationBuilder.RenameColumn(
                name: "expiredInActivityMessage",
                table: "Inventories",
                newName: "ExpiredInActivityMessage");

            migrationBuilder.RenameColumn(
                name: "expirationTooltip",
                table: "Inventories",
                newName: "ExpirationTooltip");

            migrationBuilder.RenameColumn(
                name: "bucketTypeHash",
                table: "Inventories",
                newName: "BucketTypeHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Inventories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "socketTypeHash",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "SocketTypeHash");

            migrationBuilder.RenameColumn(
                name: "plugItemHash",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "PlugItemHash");

            migrationBuilder.RenameColumn(
                name: "defaultVisible",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "DefaultVisible");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "IntrinsicSocketEntryDefinitions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "uniqueLabelHash",
                table: "EquippingBlocks",
                newName: "UniqueLabelHash");

            migrationBuilder.RenameColumn(
                name: "uniqueLabel",
                table: "EquippingBlocks",
                newName: "UniqueLabel");

            migrationBuilder.RenameColumn(
                name: "hornSoundHash",
                table: "EquippingBlocks",
                newName: "HornSoundHash");

            migrationBuilder.RenameColumn(
                name: "gearsetItemHash",
                table: "EquippingBlocks",
                newName: "GearsetItemHash");

            migrationBuilder.RenameColumn(
                name: "equippingSoundHash",
                table: "EquippingBlocks",
                newName: "EquippingSoundHash");

            migrationBuilder.RenameColumn(
                name: "equipmentSlotTypeHash",
                table: "EquippingBlocks",
                newName: "EquipmentSlotTypeHash");

            migrationBuilder.RenameColumn(
                name: "displayStrings",
                table: "EquippingBlocks",
                newName: "DisplayStrings");

            migrationBuilder.RenameColumn(
                name: "attributes",
                table: "EquippingBlocks",
                newName: "Attributes");

            migrationBuilder.RenameColumn(
                name: "ammoType",
                table: "EquippingBlocks",
                newName: "AmmoType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "EquippingBlocks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "translationBlockId",
                table: "DyeReferences",
                newName: "TranslationBlockId");

            migrationBuilder.RenameColumn(
                name: "dyeHash",
                table: "DyeReferences",
                newName: "DyeHash");

            migrationBuilder.RenameColumn(
                name: "channelHash",
                table: "DyeReferences",
                newName: "ChannelHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DyeReferences",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DyeReferences_translationBlockId",
                table: "DyeReferences",
                newName: "IX_DyeReferences_TranslationBlockId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "DisplayProperties",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "icon",
                table: "DisplayProperties",
                newName: "Icon");

            migrationBuilder.RenameColumn(
                name: "hasIcon",
                table: "DisplayProperties",
                newName: "HasIcon");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "DisplayProperties",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DisplayProperties",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "red",
                table: "DestinyColors",
                newName: "Red");

            migrationBuilder.RenameColumn(
                name: "green",
                table: "DestinyColors",
                newName: "Green");

            migrationBuilder.RenameColumn(
                name: "colorHash",
                table: "DestinyColors",
                newName: "ColorHash");

            migrationBuilder.RenameColumn(
                name: "blue",
                table: "DestinyColors",
                newName: "Blue");

            migrationBuilder.RenameColumn(
                name: "alpha",
                table: "DestinyColors",
                newName: "Alpha");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DestinyColors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "requiredSocketTypeHashes",
                table: "Craftings",
                newName: "RequiredSocketTypeHashes");

            migrationBuilder.RenameColumn(
                name: "outputItemHash",
                table: "Craftings",
                newName: "OutputItemHash");

            migrationBuilder.RenameColumn(
                name: "failedRequirementStrings",
                table: "Craftings",
                newName: "FailedRequirementStrings");

            migrationBuilder.RenameColumn(
                name: "baseMaterialRequirements",
                table: "Craftings",
                newName: "BaseMaterialRequirements");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Craftings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "socketTypeHash",
                table: "CraftingBlockBonusPlugs",
                newName: "SocketTypeHash");

            migrationBuilder.RenameColumn(
                name: "plugItemHash",
                table: "CraftingBlockBonusPlugs",
                newName: "PlugItemHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CraftingBlockBonusPlugs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "classHash",
                table: "Arrangements",
                newName: "ClassHash");

            migrationBuilder.RenameColumn(
                name: "artArrangementHash",
                table: "Arrangements",
                newName: "ArtArrangementHash");

            migrationBuilder.RenameColumn(
                name: "TranslationBlockweaponPatternHash",
                table: "Arrangements",
                newName: "TranslationBlockWeaponPatternHash");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Arrangements",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Arrangements_TranslationBlockweaponPatternHash",
                table: "Arrangements",
                newName: "IX_Arrangements_TranslationBlockWeaponPatternHash");

            migrationBuilder.RenameColumn(
                name: "verbName",
                table: "Actions",
                newName: "VerbName");

            migrationBuilder.RenameColumn(
                name: "verbDescription",
                table: "Actions",
                newName: "VerbDescription");

            migrationBuilder.RenameColumn(
                name: "useOnAcquire",
                table: "Actions",
                newName: "UseOnAcquire");

            migrationBuilder.RenameColumn(
                name: "rewardSiteHash",
                table: "Actions",
                newName: "RewardSiteHash");

            migrationBuilder.RenameColumn(
                name: "rewardSheetHash",
                table: "Actions",
                newName: "RewardSheetHash");

            migrationBuilder.RenameColumn(
                name: "rewardItemHash",
                table: "Actions",
                newName: "RewardItemHash");

            migrationBuilder.RenameColumn(
                name: "requiredLocation",
                table: "Actions",
                newName: "RequiredLocation");

            migrationBuilder.RenameColumn(
                name: "requiredCooldownSeconds",
                table: "Actions",
                newName: "RequiredCooldownSeconds");

            migrationBuilder.RenameColumn(
                name: "requiredCooldownHash",
                table: "Actions",
                newName: "RequiredCooldownHash");

            migrationBuilder.RenameColumn(
                name: "overlayScreenName",
                table: "Actions",
                newName: "OverlayScreenName");

            migrationBuilder.RenameColumn(
                name: "overlayIcon",
                table: "Actions",
                newName: "OverlayIcon");

            migrationBuilder.RenameColumn(
                name: "isPositive",
                table: "Actions",
                newName: "IsPositive");

            migrationBuilder.RenameColumn(
                name: "deleteOnAction",
                table: "Actions",
                newName: "DeleteOnAction");

            migrationBuilder.RenameColumn(
                name: "consumeEntireStack",
                table: "Actions",
                newName: "ConsumeEntireStack");

            migrationBuilder.RenameColumn(
                name: "actionTypeLabel",
                table: "Actions",
                newName: "ActionTypeLabel");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Actions",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrangements_TranslationBlocks_TranslationBlockWeaponPatter~",
                table: "Arrangements",
                column: "TranslationBlockWeaponPatternHash",
                principalTable: "TranslationBlocks",
                principalColumn: "WeaponPatternHash");

            migrationBuilder.AddForeignKey(
                name: "FK_DyeReferences_TranslationBlocks_TranslationBlockId",
                table: "DyeReferences",
                column: "TranslationBlockId",
                principalTable: "TranslationBlocks",
                principalColumn: "WeaponPatternHash",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StatEntries_StatsBlocks_StatsBlockId",
                table: "StatEntries",
                column: "StatsBlockId",
                principalTable: "StatsBlocks",
                principalColumn: "StatGroupHash",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
