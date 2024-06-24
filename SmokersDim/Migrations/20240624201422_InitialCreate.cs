using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmokersDim.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VerbName = table.Column<string>(type: "text", nullable: true),
                    VerbDescription = table.Column<string>(type: "text", nullable: true),
                    IsPositive = table.Column<bool>(type: "boolean", nullable: false),
                    RequiredCooldownSeconds = table.Column<int>(type: "integer", nullable: false),
                    OverlayScreenName = table.Column<string>(type: "text", nullable: true),
                    OverlayIcon = table.Column<string>(type: "text", nullable: true),
                    ActionTypeLabel = table.Column<string>(type: "text", nullable: true),
                    RequiredLocation = table.Column<string>(type: "text", nullable: true),
                    RequiredCooldownHash = table.Column<long>(type: "bigint", nullable: false),
                    RewardSheetHash = table.Column<long>(type: "bigint", nullable: false),
                    RewardItemHash = table.Column<long>(type: "bigint", nullable: false),
                    RewardSiteHash = table.Column<long>(type: "bigint", nullable: false),
                    DeleteOnAction = table.Column<bool>(type: "boolean", nullable: false),
                    ConsumeEntireStack = table.Column<bool>(type: "boolean", nullable: false),
                    UseOnAcquire = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Craftings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutputItemHash = table.Column<long>(type: "bigint", nullable: false),
                    RequiredSocketTypeHashes = table.Column<long[]>(type: "bigint[]", nullable: true),
                    FailedRequirementStrings = table.Column<List<string>>(type: "text[]", nullable: true),
                    BaseMaterialRequirements = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Craftings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinyColors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColorHash = table.Column<long>(type: "bigint", nullable: false),
                    Red = table.Column<int>(type: "integer", nullable: false),
                    Green = table.Column<int>(type: "integer", nullable: false),
                    Blue = table.Column<int>(type: "integer", nullable: false),
                    Alpha = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinyColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisplayProperties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    HasIcon = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquippingBlocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GearsetItemHash = table.Column<long>(type: "bigint", nullable: true),
                    UniqueLabel = table.Column<string>(type: "text", nullable: true),
                    UniqueLabelHash = table.Column<long>(type: "bigint", nullable: false),
                    EquipmentSlotTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    Attributes = table.Column<int>(type: "integer", nullable: false),
                    EquippingSoundHash = table.Column<long>(type: "bigint", nullable: false),
                    HornSoundHash = table.Column<long>(type: "bigint", nullable: false),
                    AmmoType = table.Column<int>(type: "integer", nullable: false),
                    DisplayStrings = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquippingBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StackUniqueLabel = table.Column<string>(type: "text", nullable: true),
                    MaxStackSize = table.Column<int>(type: "integer", nullable: false),
                    BucketTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    RecoveryBucketTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    TierTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    IsInstanceItem = table.Column<bool>(type: "boolean", nullable: false),
                    NonTransferrableOriginal = table.Column<bool>(type: "boolean", nullable: false),
                    TierTypeName = table.Column<string>(type: "text", nullable: true),
                    TierType = table.Column<int>(type: "integer", nullable: false),
                    ExpirationTooltip = table.Column<string>(type: "text", nullable: true),
                    ExpiredInActivityMessage = table.Column<string>(type: "text", nullable: true),
                    ExpiredInOrbitMessage = table.Column<string>(type: "text", nullable: true),
                    SuppressExpirationWhenObjectivesComplete = table.Column<bool>(type: "boolean", nullable: false),
                    RecipeItemHash = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemLevels = table.Column<List<int>>(type: "integer[]", nullable: true),
                    QualityLevel = table.Column<int>(type: "integer", nullable: false),
                    InfusionCategoryName = table.Column<string>(type: "text", nullable: true),
                    InfusionCategoryHash = table.Column<long>(type: "bigint", nullable: false),
                    InfusionCategoryHashes = table.Column<long[]>(type: "bigint[]", nullable: true),
                    ProgressionLevelRequirementHash = table.Column<long>(type: "bigint", nullable: false),
                    CurrentVersion = table.Column<long>(type: "bigint", nullable: false),
                    DisplayVersionWatermarkIcons = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SetBlocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequireOrderedSetItemAdd = table.Column<bool>(type: "boolean", nullable: false),
                    SetIsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    SetType = table.Column<string>(type: "text", nullable: true),
                    QuestLineName = table.Column<string>(type: "text", nullable: true),
                    QuestLineDescription = table.Column<string>(type: "text", nullable: true),
                    QuestStepSummary = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocketBlocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Detail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocketBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatsBlocks",
                columns: table => new
                {
                    StatGroupHash = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisablePrimaryStatDisplay = table.Column<bool>(type: "boolean", nullable: false),
                    HasDisplayableStats = table.Column<bool>(type: "boolean", nullable: false),
                    PrimaryBaseStatHash = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsBlocks", x => x.StatGroupHash);
                });

            migrationBuilder.CreateTable(
                name: "TalentGrids",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TalentGridHash = table.Column<long>(type: "bigint", nullable: false),
                    ItemDetailString = table.Column<string>(type: "text", nullable: true),
                    BuildName = table.Column<string>(type: "text", nullable: true),
                    HudDamageType = table.Column<int>(type: "integer", nullable: false),
                    HudIcon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentGrids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranslationBlocks",
                columns: table => new
                {
                    WeaponPatternHash = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeaponPatternIdentifier = table.Column<string>(type: "text", nullable: true),
                    HasGeometry = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationBlocks", x => x.WeaponPatternHash);
                });

            migrationBuilder.CreateTable(
                name: "ValueBlocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ValueDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressionRewards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgressionMappingHash = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    ApplyThrottles = table.Column<bool>(type: "boolean", nullable: false),
                    ActionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressionRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressionRewards_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequiredItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    ItemHash = table.Column<long>(type: "bigint", nullable: false),
                    DeleteOnAction = table.Column<bool>(type: "boolean", nullable: false),
                    ActionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredItems_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CraftingBlockBonusPlugs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SocketTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    PlugItemHash = table.Column<long>(type: "bigint", nullable: false),
                    CraftingId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraftingBlockBonusPlugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CraftingBlockBonusPlugs_Craftings_CraftingId",
                        column: x => x.CraftingId,
                        principalTable: "Craftings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PowerCapHash = table.Column<long>(type: "bigint", nullable: false),
                    QualityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versions_Qualities_QualityId",
                        column: x => x.QualityId,
                        principalTable: "Qualities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SetBlockEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrackingValue = table.Column<int>(type: "integer", nullable: false),
                    ItemHash = table.Column<long>(type: "bigint", nullable: false),
                    SetBlockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetBlockEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetBlockEntries_SetBlocks_SetBlockId",
                        column: x => x.SetBlockId,
                        principalTable: "SetBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IntrinsicSocketEntryDefinitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlugItemHash = table.Column<long>(type: "bigint", nullable: false),
                    SocketTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    DefaultVisible = table.Column<bool>(type: "boolean", nullable: false),
                    SocketBlockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntrinsicSocketEntryDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntrinsicSocketEntryDefinitions_SocketBlocks_SocketBlockId",
                        column: x => x.SocketBlockId,
                        principalTable: "SocketBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocketCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SocketCategoryHash = table.Column<long>(type: "bigint", nullable: false),
                    SocketIndexes = table.Column<List<int>>(type: "integer[]", nullable: true),
                    SocketBlockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocketCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocketCategories_SocketBlocks_SocketBlockId",
                        column: x => x.SocketBlockId,
                        principalTable: "SocketBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocketEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SocketTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    SingleInitialItemHash = table.Column<long>(type: "bigint", nullable: false),
                    PreventInitializationOnVendorPurchase = table.Column<bool>(type: "boolean", nullable: false),
                    HidePerksInItemTooltip = table.Column<bool>(type: "boolean", nullable: false),
                    PlugSources = table.Column<int>(type: "integer", nullable: false),
                    ReusablePlugSetHash = table.Column<long>(type: "bigint", nullable: true),
                    RandomizedPlugSetHash = table.Column<long>(type: "bigint", nullable: true),
                    DefaultVisible = table.Column<bool>(type: "boolean", nullable: false),
                    SocketBlockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocketEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocketEntries_SocketBlocks_SocketBlockId",
                        column: x => x.SocketBlockId,
                        principalTable: "SocketBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StatEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatHash = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Minimum = table.Column<int>(type: "integer", nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    DisplayMaximum = table.Column<int>(type: "integer", nullable: false),
                    StatsBlockId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatEntries_StatsBlocks_StatsBlockId",
                        column: x => x.StatsBlockId,
                        principalTable: "StatsBlocks",
                        principalColumn: "StatGroupHash",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arrangements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassHash = table.Column<long>(type: "bigint", nullable: false),
                    ArtArrangementHash = table.Column<long>(type: "bigint", nullable: false),
                    TranslationBlockWeaponPatternHash = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrangements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arrangements_TranslationBlocks_TranslationBlockWeaponPatter~",
                        column: x => x.TranslationBlockWeaponPatternHash,
                        principalTable: "TranslationBlocks",
                        principalColumn: "WeaponPatternHash");
                });

            migrationBuilder.CreateTable(
                name: "DyeReferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChannelHash = table.Column<long>(type: "bigint", nullable: false),
                    DyeHash = table.Column<long>(type: "bigint", nullable: false),
                    TranslationBlockId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyeReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyeReferences_TranslationBlocks_TranslationBlockId",
                        column: x => x.TranslationBlockId,
                        principalTable: "TranslationBlocks",
                        principalColumn: "WeaponPatternHash",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemQuantities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemHash = table.Column<long>(type: "bigint", nullable: false),
                    ItemInstanceId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    HasConditionalVisibility = table.Column<bool>(type: "boolean", nullable: false),
                    ValueBlockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemQuantities_ValueBlocks_ValueBlockId",
                        column: x => x.ValueBlockId,
                        principalTable: "ValueBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Hash = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Redacted = table.Column<bool>(type: "boolean", nullable: false),
                    Blacklisted = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayPropertiesId = table.Column<long>(type: "bigint", nullable: true),
                    CollectibleHash = table.Column<long>(type: "bigint", nullable: true),
                    IconWatermark = table.Column<string>(type: "text", nullable: true),
                    IconWatermarkShelved = table.Column<string>(type: "text", nullable: true),
                    BackgroundColorId = table.Column<long>(type: "bigint", nullable: true),
                    Screenshot = table.Column<string>(type: "text", nullable: true),
                    ItemTypeDisplayName = table.Column<string>(type: "text", nullable: true),
                    FlavorText = table.Column<string>(type: "text", nullable: true),
                    UiItemDisplayStyle = table.Column<string>(type: "text", nullable: true),
                    ItemTypeAndTierDisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplaySource = table.Column<string>(type: "text", nullable: true),
                    ActionId = table.Column<long>(type: "bigint", nullable: true),
                    CraftingId = table.Column<long>(type: "bigint", nullable: true),
                    InventoryId = table.Column<long>(type: "bigint", nullable: true),
                    SetDataId = table.Column<long>(type: "bigint", nullable: true),
                    StatsId = table.Column<long>(type: "bigint", nullable: true),
                    EmblemObjectiveHash = table.Column<long>(type: "bigint", nullable: true),
                    EquippingBlockId = table.Column<long>(type: "bigint", nullable: true),
                    TranslationBlockId = table.Column<long>(type: "bigint", nullable: true),
                    QualityId = table.Column<long>(type: "bigint", nullable: true),
                    ValueId = table.Column<long>(type: "bigint", nullable: true),
                    AcquireRewardSiteHash = table.Column<long>(type: "bigint", nullable: false),
                    AcquireUnlockHash = table.Column<long>(type: "bigint", nullable: false),
                    SocketsId = table.Column<long>(type: "bigint", nullable: true),
                    TalentGridId = table.Column<long>(type: "bigint", nullable: true),
                    LoreHash = table.Column<long>(type: "bigint", nullable: true),
                    SummaryItemHash = table.Column<long>(type: "bigint", nullable: true),
                    AllowActions = table.Column<bool>(type: "boolean", nullable: false),
                    DoesPostmasterPullHaveSideEffects = table.Column<bool>(type: "boolean", nullable: false),
                    NonTransferrable = table.Column<bool>(type: "boolean", nullable: false),
                    ItemCategoryHashes = table.Column<long[]>(type: "bigint[]", nullable: true),
                    SpecialItemType = table.Column<int>(type: "integer", nullable: false),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    ItemSubType = table.Column<int>(type: "integer", nullable: false),
                    ClassType = table.Column<int>(type: "integer", nullable: false),
                    BreakerType = table.Column<int>(type: "integer", nullable: false),
                    BreakerTypeHash = table.Column<long>(type: "bigint", nullable: true),
                    Equippable = table.Column<bool>(type: "boolean", nullable: false),
                    DamageTypeHashes = table.Column<long[]>(type: "bigint[]", nullable: true),
                    DamageTypes = table.Column<List<int>>(type: "integer[]", nullable: true),
                    DefaultDamageType = table.Column<int>(type: "integer", nullable: false),
                    DefaultDamageTypeHash = table.Column<long>(type: "bigint", nullable: true),
                    SeasonHash = table.Column<long>(type: "bigint", nullable: true),
                    IsWrapper = table.Column<bool>(type: "boolean", nullable: false),
                    TraitIds = table.Column<List<string>>(type: "text[]", nullable: true),
                    TraitHashes = table.Column<long[]>(type: "bigint[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Hash);
                    table.ForeignKey(
                        name: "FK_Items_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Craftings_CraftingId",
                        column: x => x.CraftingId,
                        principalTable: "Craftings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_DestinyColors_BackgroundColorId",
                        column: x => x.BackgroundColorId,
                        principalTable: "DestinyColors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_DisplayProperties_DisplayPropertiesId",
                        column: x => x.DisplayPropertiesId,
                        principalTable: "DisplayProperties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_EquippingBlocks_EquippingBlockId",
                        column: x => x.EquippingBlockId,
                        principalTable: "EquippingBlocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Qualities_QualityId",
                        column: x => x.QualityId,
                        principalTable: "Qualities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_SetBlocks_SetDataId",
                        column: x => x.SetDataId,
                        principalTable: "SetBlocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_SocketBlocks_SocketsId",
                        column: x => x.SocketsId,
                        principalTable: "SocketBlocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_StatsBlocks_StatsId",
                        column: x => x.StatsId,
                        principalTable: "StatsBlocks",
                        principalColumn: "StatGroupHash");
                    table.ForeignKey(
                        name: "FK_Items_TalentGrids_TalentGridId",
                        column: x => x.TalentGridId,
                        principalTable: "TalentGrids",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_TranslationBlocks_TranslationBlockId",
                        column: x => x.TranslationBlockId,
                        principalTable: "TranslationBlocks",
                        principalColumn: "WeaponPatternHash");
                    table.ForeignKey(
                        name: "FK_Items_ValueBlocks_ValueId",
                        column: x => x.ValueId,
                        principalTable: "ValueBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocketEntryPlugItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlugItemHash = table.Column<long>(type: "bigint", nullable: false),
                    SocketEntryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocketEntryPlugItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocketEntryPlugItems_SocketEntries_SocketEntryId",
                        column: x => x.SocketEntryId,
                        principalTable: "SocketEntries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvestmentStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatTypeHash = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    IsConditionallyActive = table.Column<bool>(type: "boolean", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentStats_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Hash");
                });

            migrationBuilder.CreateTable(
                name: "PerkEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequirementDisplayString = table.Column<string>(type: "text", nullable: true),
                    PerkHash = table.Column<long>(type: "bigint", nullable: false),
                    PerkVisibility = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerkEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerkEntries_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Hash");
                });

            migrationBuilder.CreateTable(
                name: "TooltipNotifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayString = table.Column<string>(type: "text", nullable: true),
                    DisplayStyle = table.Column<string>(type: "text", nullable: true),
                    ItemId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TooltipNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TooltipNotifications_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Hash");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arrangements_TranslationBlockWeaponPatternHash",
                table: "Arrangements",
                column: "TranslationBlockWeaponPatternHash");

            migrationBuilder.CreateIndex(
                name: "IX_CraftingBlockBonusPlugs_CraftingId",
                table: "CraftingBlockBonusPlugs",
                column: "CraftingId");

            migrationBuilder.CreateIndex(
                name: "IX_DyeReferences_TranslationBlockId",
                table: "DyeReferences",
                column: "TranslationBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_IntrinsicSocketEntryDefinitions_SocketBlockId",
                table: "IntrinsicSocketEntryDefinitions",
                column: "SocketBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentStats_ItemId",
                table: "InvestmentStats",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemQuantities_ValueBlockId",
                table: "ItemQuantities",
                column: "ValueBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ActionId",
                table: "Items",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BackgroundColorId",
                table: "Items",
                column: "BackgroundColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CraftingId",
                table: "Items",
                column: "CraftingId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_DisplayPropertiesId",
                table: "Items",
                column: "DisplayPropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_EquippingBlockId",
                table: "Items",
                column: "EquippingBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_InventoryId",
                table: "Items",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_QualityId",
                table: "Items",
                column: "QualityId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SetDataId",
                table: "Items",
                column: "SetDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SocketsId",
                table: "Items",
                column: "SocketsId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_StatsId",
                table: "Items",
                column: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TalentGridId",
                table: "Items",
                column: "TalentGridId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TranslationBlockId",
                table: "Items",
                column: "TranslationBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ValueId",
                table: "Items",
                column: "ValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PerkEntries_ItemId",
                table: "PerkEntries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressionRewards_ActionId",
                table: "ProgressionRewards",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItems_ActionId",
                table: "RequiredItems",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SetBlockEntries_SetBlockId",
                table: "SetBlockEntries",
                column: "SetBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_SocketCategories_SocketBlockId",
                table: "SocketCategories",
                column: "SocketBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_SocketEntries_SocketBlockId",
                table: "SocketEntries",
                column: "SocketBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_SocketEntryPlugItems_SocketEntryId",
                table: "SocketEntryPlugItems",
                column: "SocketEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_StatEntries_StatsBlockId",
                table: "StatEntries",
                column: "StatsBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_TooltipNotifications_ItemId",
                table: "TooltipNotifications",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_QualityId",
                table: "Versions",
                column: "QualityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arrangements");

            migrationBuilder.DropTable(
                name: "CraftingBlockBonusPlugs");

            migrationBuilder.DropTable(
                name: "DyeReferences");

            migrationBuilder.DropTable(
                name: "IntrinsicSocketEntryDefinitions");

            migrationBuilder.DropTable(
                name: "InvestmentStats");

            migrationBuilder.DropTable(
                name: "ItemQuantities");

            migrationBuilder.DropTable(
                name: "PerkEntries");

            migrationBuilder.DropTable(
                name: "ProgressionRewards");

            migrationBuilder.DropTable(
                name: "RequiredItems");

            migrationBuilder.DropTable(
                name: "SetBlockEntries");

            migrationBuilder.DropTable(
                name: "SocketCategories");

            migrationBuilder.DropTable(
                name: "SocketEntryPlugItems");

            migrationBuilder.DropTable(
                name: "StatEntries");

            migrationBuilder.DropTable(
                name: "TooltipNotifications");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "SocketEntries");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Craftings");

            migrationBuilder.DropTable(
                name: "DestinyColors");

            migrationBuilder.DropTable(
                name: "DisplayProperties");

            migrationBuilder.DropTable(
                name: "EquippingBlocks");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Qualities");

            migrationBuilder.DropTable(
                name: "SetBlocks");

            migrationBuilder.DropTable(
                name: "SocketBlocks");

            migrationBuilder.DropTable(
                name: "StatsBlocks");

            migrationBuilder.DropTable(
                name: "TalentGrids");

            migrationBuilder.DropTable(
                name: "TranslationBlocks");

            migrationBuilder.DropTable(
                name: "ValueBlocks");
        }
    }
}
