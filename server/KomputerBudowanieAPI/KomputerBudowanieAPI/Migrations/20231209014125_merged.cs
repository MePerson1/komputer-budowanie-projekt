using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class merged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    HasLightning = table.Column<bool>(type: "boolean", nullable: false),
                    HeightCM = table.Column<float>(type: "real", nullable: false),
                    LengthCM = table.Column<float>(type: "real", nullable: false),
                    WidthCM = table.Column<float>(type: "real", nullable: false),
                    WeightKG = table.Column<float>(type: "real", nullable: false),
                    CaseType = table.Column<string>(type: "text", nullable: false),
                    Compatibility = table.Column<string>(type: "text", nullable: false),
                    HasWindow = table.Column<bool>(type: "boolean", nullable: false),
                    IsMuted = table.Column<bool>(type: "boolean", nullable: false),
                    MaxGPULengthCM = table.Column<float>(type: "real", nullable: false),
                    MaxCoolingSystemHeightCM = table.Column<float>(type: "real", nullable: false),
                    USBTwoCount = table.Column<int>(type: "integer", nullable: false),
                    USBThreeCount = table.Column<int>(type: "integer", nullable: false),
                    USBThreePointOneCount = table.Column<int>(type: "integer", nullable: false),
                    USBThreePointTwoCount = table.Column<int>(type: "integer", nullable: false),
                    USBTypeCCount = table.Column<int>(type: "integer", nullable: false),
                    USBTurboChargingCount = table.Column<int>(type: "integer", nullable: false),
                    HasMemoryCardReader = table.Column<bool>(type: "boolean", nullable: false),
                    HasAudioPort = table.Column<bool>(type: "boolean", nullable: false),
                    HasMicrophonePort = table.Column<bool>(type: "boolean", nullable: false),
                    InternalBaysTwoPointFiveInch = table.Column<int>(type: "integer", nullable: false),
                    InternalBaysThreePointFiveInch = table.Column<int>(type: "integer", nullable: false),
                    ExternalBaysThreePointFiveInch = table.Column<int>(type: "integer", nullable: false),
                    ExternalBaysFivePointTwoFiveInch = table.Column<int>(type: "integer", nullable: false),
                    ExpansionSlots = table.Column<int>(type: "integer", nullable: false),
                    PanelFront = table.Column<string>(type: "text", nullable: true),
                    PanelRear = table.Column<string>(type: "text", nullable: true),
                    PanelSide = table.Column<string>(type: "text", nullable: true),
                    PanelBottom = table.Column<string>(type: "text", nullable: true),
                    PanelTop = table.Column<string>(type: "text", nullable: true),
                    PowerSupply = table.Column<string>(type: "text", nullable: true),
                    PowerSupplyPower = table.Column<float>(type: "real", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CpuCoolings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    MountingType = table.Column<string>(type: "text", nullable: false),
                    ColorElement = table.Column<string>(type: "text", nullable: false),
                    HeightMM = table.Column<float>(type: "real", nullable: false),
                    WidthMM = table.Column<float>(type: "real", nullable: false),
                    DepthMM = table.Column<float>(type: "real", nullable: false),
                    WeightGrams = table.Column<int>(type: "integer", nullable: false),
                    ProcessorSocket = table.Column<string>(type: "text", nullable: false),
                    MaxTDPinW = table.Column<int>(type: "integer", nullable: true),
                    BaseMaterial = table.Column<string>(type: "text", nullable: false),
                    HasLighting = table.Column<bool>(type: "boolean", nullable: false),
                    HeatPipesCount = table.Column<int>(type: "integer", nullable: false),
                    HeatPipeDiameterMM = table.Column<int>(type: "integer", nullable: false),
                    FanCount = table.Column<int>(type: "integer", nullable: false),
                    FanDiameterMM = table.Column<int>(type: "integer", nullable: false),
                    MaxFanSpeedPerMin = table.Column<int>(type: "integer", nullable: true),
                    AirflowCFM = table.Column<float>(type: "real", nullable: true),
                    MaxNoiseLevelinDBA = table.Column<float>(type: "real", nullable: true),
                    LifespanHours = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CpuCoolings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cpus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    Line = table.Column<string>(type: "text", nullable: false),
                    HasIncludedCooling = table.Column<bool>(type: "boolean", nullable: false),
                    SocketType = table.Column<string>(type: "text", nullable: false),
                    NumberOfCores = table.Column<int>(type: "integer", nullable: false),
                    NumberOfThreads = table.Column<int>(type: "integer", nullable: false),
                    ProcessorBaseFrequencyGHz = table.Column<float>(type: "real", nullable: false),
                    MaxTurboFrequencyGHz = table.Column<float>(type: "real", nullable: false),
                    IntegratedGraphics = table.Column<string>(type: "text", nullable: true),
                    HasUnlockedMultiplier = table.Column<bool>(type: "boolean", nullable: false),
                    Architecture = table.Column<string>(type: "text", nullable: false),
                    ManufacturingProcess = table.Column<string>(type: "text", nullable: false),
                    ProcessorMicroarchitecture = table.Column<string>(type: "text", nullable: false),
                    TDPinW = table.Column<int>(type: "integer", nullable: false),
                    MaxOperatingTempC = table.Column<int>(type: "integer", nullable: true),
                    SupportedMemoryTypes = table.Column<string>(type: "text", nullable: true),
                    L1Cache = table.Column<string>(type: "text", nullable: false),
                    L2Cache = table.Column<string>(type: "text", nullable: false),
                    L3Cache = table.Column<string>(type: "text", nullable: false),
                    AddedEquipment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cpus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GraphicCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    ChipsetProducer = table.Column<string>(type: "text", nullable: false),
                    ChipsetType = table.Column<string>(type: "text", nullable: false),
                    CoreClockMHz = table.Column<int>(type: "integer", nullable: false),
                    BoostClockMHz = table.Column<int>(type: "integer", nullable: false),
                    StreamProcessors = table.Column<int>(type: "integer", nullable: false),
                    ROPUnits = table.Column<int>(type: "integer", nullable: false),
                    TextureUnits = table.Column<int>(type: "integer", nullable: false),
                    RTCores = table.Column<int>(type: "integer", nullable: false),
                    TensorCores = table.Column<int>(type: "integer", nullable: false),
                    HasDLSS3Support = table.Column<bool>(type: "boolean", nullable: false),
                    ConnectorType = table.Column<string>(type: "text", nullable: false),
                    CardLengthMM = table.Column<int>(type: "integer", nullable: false),
                    CardLinking = table.Column<string>(type: "text", nullable: true),
                    Resolution = table.Column<string>(type: "text", nullable: false),
                    RecommendedPSUCapacityW = table.Column<int>(type: "integer", nullable: false),
                    HasLEDLighting = table.Column<bool>(type: "boolean", nullable: false),
                    MemorySizeGB = table.Column<int>(type: "integer", nullable: false),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    MemoryBusWidthBits = table.Column<int>(type: "integer", nullable: false),
                    MemoryClockMHz = table.Column<int>(type: "integer", nullable: false),
                    CoolingType = table.Column<string>(type: "text", nullable: false),
                    FanCount = table.Column<int>(type: "integer", nullable: false),
                    DSub = table.Column<int>(type: "integer", nullable: false),
                    DisplayPortCount = table.Column<int>(type: "integer", nullable: false),
                    MiniDisplayPort = table.Column<int>(type: "integer", nullable: false),
                    DVI = table.Column<int>(type: "integer", nullable: false),
                    HDMI = table.Column<int>(type: "integer", nullable: false),
                    USBC = table.Column<int>(type: "integer", nullable: false),
                    PowerConnectors = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    BoardStandard = table.Column<string>(type: "text", nullable: false),
                    WidthMM = table.Column<float>(type: "real", nullable: false),
                    DepthMM = table.Column<float>(type: "real", nullable: false),
                    Chipset = table.Column<string>(type: "text", nullable: false),
                    CPUSocket = table.Column<string>(type: "text", nullable: false),
                    SupportedProcessors = table.Column<string>(type: "text", nullable: false),
                    RAIDController = table.Column<string>(type: "text", nullable: true),
                    MemoryStandard = table.Column<string>(type: "text", nullable: false),
                    MemoryConnectorType = table.Column<string>(type: "text", nullable: false),
                    MemorySlotsCount = table.Column<int>(type: "integer", nullable: false),
                    SupportedMemoryFreq = table.Column<string>(type: "text", nullable: false),
                    MaxMemoryGB = table.Column<int>(type: "integer", nullable: false),
                    ChannelArchitecture = table.Column<string>(type: "text", nullable: false),
                    HasIntegratedGraphicsSupport = table.Column<bool>(type: "boolean", nullable: false),
                    GraphicsChipset = table.Column<string>(type: "text", nullable: false),
                    CardLinking = table.Column<string>(type: "text", nullable: true),
                    SoundChipset = table.Column<string>(type: "text", nullable: false),
                    AudioChannels = table.Column<string>(type: "text", nullable: false),
                    IntegratedNetworkCard = table.Column<string>(type: "text", nullable: false),
                    NetworkChipset = table.Column<string>(type: "text", nullable: false),
                    WirelessSupport = table.Column<string>(type: "text", nullable: true),
                    ExpansionSlots = table.Column<string>(type: "text", nullable: false),
                    DriveConnectors = table.Column<string>(type: "text", nullable: false),
                    InternalConnectors = table.Column<string>(type: "text", nullable: false),
                    RearPanelConnectors = table.Column<string>(type: "text", nullable: false),
                    IncludedAccessories = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PowerSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    FormFactor = table.Column<string>(type: "text", nullable: false),
                    PowerW = table.Column<int>(type: "integer", nullable: false),
                    Certificate = table.Column<string>(type: "text", nullable: false),
                    PowerFactorCorrection = table.Column<string>(type: "text", nullable: false),
                    EfficiencyRating = table.Column<string>(type: "text", nullable: false),
                    Cooling = table.Column<string>(type: "text", nullable: false),
                    FanDiameterMM = table.Column<int>(type: "integer", nullable: false),
                    Security = table.Column<string>(type: "text", nullable: false),
                    ModularCabling = table.Column<string>(type: "text", nullable: true),
                    ATX24Pin_20Plus4 = table.Column<int>(type: "integer", nullable: false),
                    PCIE8Pin_6Plus2 = table.Column<int>(type: "integer", nullable: false),
                    PCIE16Pin = table.Column<int>(type: "integer", nullable: false),
                    PCIE8Pin = table.Column<int>(type: "integer", nullable: false),
                    PCIE6Pin = table.Column<int>(type: "integer", nullable: false),
                    CPU8Pin_4Plus4 = table.Column<int>(type: "integer", nullable: false),
                    CPU8Pin = table.Column<int>(type: "integer", nullable: false),
                    CPU4Pin = table.Column<int>(type: "integer", nullable: false),
                    Sata = table.Column<int>(type: "integer", nullable: false),
                    Molex = table.Column<int>(type: "integer", nullable: false),
                    HeightMM = table.Column<int>(type: "integer", nullable: false),
                    WidthMM = table.Column<int>(type: "integer", nullable: false),
                    DepthMM = table.Column<int>(type: "integer", nullable: false),
                    HasLighting = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerSupplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    PinType = table.Column<string>(type: "text", nullable: false),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    LowProfile = table.Column<bool>(type: "boolean", nullable: false),
                    Cooling = table.Column<string>(type: "text", nullable: false),
                    CapacityGB = table.Column<int>(type: "integer", nullable: false),
                    ModuleCount = table.Column<int>(type: "integer", nullable: false),
                    FrequencyMHz = table.Column<int>(type: "integer", nullable: false),
                    LatencyCL = table.Column<int>(type: "integer", nullable: false),
                    VoltageV = table.Column<float>(type: "real", nullable: false),
                    OverclockingProfile = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    HasLighting = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    FormFactor = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<string>(type: "text", nullable: false),
                    Interface = table.Column<string>(type: "text", nullable: false),
                    ThiccnessMM = table.Column<float>(type: "real", nullable: true),
                    CacheMemory = table.Column<string>(type: "text", nullable: true),
                    NoiseLevelDB = table.Column<float>(type: "real", nullable: true),
                    RotatingSpeedRPM = table.Column<int>(type: "integer", nullable: true),
                    WeightG = table.Column<float>(type: "real", nullable: true),
                    Radiator = table.Column<bool>(type: "boolean", nullable: true),
                    MemoryChipType = table.Column<string>(type: "text", nullable: true),
                    ReadSpeedMBs = table.Column<int>(type: "integer", nullable: true),
                    WriteSpeedMBs = table.Column<int>(type: "integer", nullable: true),
                    ReadRandomIOPS = table.Column<int>(type: "integer", nullable: true),
                    WriteRandomIOPS = table.Column<int>(type: "integer", nullable: true),
                    Longevity = table.Column<string>(type: "text", nullable: true),
                    TBW = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Controler = table.Column<string>(type: "text", nullable: true),
                    HardwareEncryption = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NickName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterCoolings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    IntelCompatibility = table.Column<string>(type: "text", nullable: false),
                    AMDCompatibility = table.Column<string>(type: "text", nullable: false),
                    Lighting = table.Column<string>(type: "text", nullable: true),
                    WeightG = table.Column<int>(type: "integer", nullable: true),
                    RadiatorSizeMM = table.Column<float>(type: "real", nullable: false),
                    RadiatorLengthMM = table.Column<float>(type: "real", nullable: false),
                    RadiatorWidthMM = table.Column<float>(type: "real", nullable: false),
                    RadiatorHeightMM = table.Column<float>(type: "real", nullable: false),
                    FanCount = table.Column<int>(type: "integer", nullable: false),
                    FanDiameterMM = table.Column<int>(type: "integer", nullable: false),
                    MaxFanSpeedRPM = table.Column<int>(type: "integer", nullable: false),
                    HasPWMControl = table.Column<bool>(type: "boolean", nullable: false),
                    MaxAirflowCFM = table.Column<float>(type: "real", nullable: true),
                    MaxNoiseLevelDBa = table.Column<float>(type: "real", nullable: true),
                    FanConnector = table.Column<string>(type: "text", nullable: false),
                    PumpConnector = table.Column<string>(type: "text", nullable: true),
                    LEDConnector = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterCoolings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopName = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    CaseId = table.Column<int>(type: "integer", nullable: true),
                    CpuCoolingId = table.Column<int>(type: "integer", nullable: true),
                    CpuId = table.Column<int>(type: "integer", nullable: true),
                    MotherboardId = table.Column<int>(type: "integer", nullable: true),
                    PowerSupplyId = table.Column<int>(type: "integer", nullable: true),
                    RamId = table.Column<int>(type: "integer", nullable: true),
                    StorageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPrices_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrices_CpuCoolings_CpuCoolingId",
                        column: x => x.CpuCoolingId,
                        principalTable: "CpuCoolings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrices_Cpus_CpuId",
                        column: x => x.CpuId,
                        principalTable: "Cpus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrices_Motherboards_MotherboardId",
                        column: x => x.MotherboardId,
                        principalTable: "Motherboards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrices_PowerSupplies_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PowerSupplies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrices_Rams_RamId",
                        column: x => x.RamId,
                        principalTable: "Rams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrices_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PcConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MotherboardId = table.Column<int>(type: "integer", nullable: true),
                    GraphicCardId = table.Column<int>(type: "integer", nullable: true),
                    CpuId = table.Column<int>(type: "integer", nullable: true),
                    CpuCoolingId = table.Column<int>(type: "integer", nullable: true),
                    CaseId = table.Column<int>(type: "integer", nullable: true),
                    PowerSupplyId = table.Column<int>(type: "integer", nullable: true),
                    WaterCoolingId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcConfigurations_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PcConfigurations_CpuCoolings_CpuCoolingId",
                        column: x => x.CpuCoolingId,
                        principalTable: "CpuCoolings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PcConfigurations_Cpus_CpuId",
                        column: x => x.CpuId,
                        principalTable: "Cpus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                        column: x => x.GraphicCardId,
                        principalTable: "GraphicCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PcConfigurations_Motherboards_MotherboardId",
                        column: x => x.MotherboardId,
                        principalTable: "Motherboards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PcConfigurations_PowerSupplies_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PowerSupplies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PcConfigurations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PcConfigurations_WaterCoolings_WaterCoolingId",
                        column: x => x.WaterCoolingId,
                        principalTable: "WaterCoolings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PcConfigurationRam",
                columns: table => new
                {
                    PcConfigurationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RamsId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcConfigurationRam", x => new { x.PcConfigurationsId, x.RamsId });
                    table.ForeignKey(
                        name: "FK_PcConfigurationRam_PcConfigurations_PcConfigurationsId",
                        column: x => x.PcConfigurationsId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcConfigurationRam_Rams_RamsId",
                        column: x => x.RamsId,
                        principalTable: "Rams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcConfigurationStorage",
                columns: table => new
                {
                    PcConfigurationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoragesId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcConfigurationStorage", x => new { x.PcConfigurationsId, x.StoragesId });
                    table.ForeignKey(
                        name: "FK_PcConfigurationStorage_PcConfigurations_PcConfigurationsId",
                        column: x => x.PcConfigurationsId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcConfigurationStorage_Storages_StoragesId",
                        column: x => x.StoragesId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationRam_RamsId",
                table: "PcConfigurationRam",
                column: "RamsId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_CaseId",
                table: "PcConfigurations",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_CpuCoolingId",
                table: "PcConfigurations",
                column: "CpuCoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_CpuId",
                table: "PcConfigurations",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_GraphicCardId",
                table: "PcConfigurations",
                column: "GraphicCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_MotherboardId",
                table: "PcConfigurations",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_PowerSupplyId",
                table: "PcConfigurations",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_UserId",
                table: "PcConfigurations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_WaterCoolingId",
                table: "PcConfigurations",
                column: "WaterCoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationStorage_StoragesId",
                table: "PcConfigurationStorage",
                column: "StoragesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_CaseId",
                table: "ShopPrices",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_CpuCoolingId",
                table: "ShopPrices",
                column: "CpuCoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_CpuId",
                table: "ShopPrices",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_MotherboardId",
                table: "ShopPrices",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_PowerSupplyId",
                table: "ShopPrices",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_RamId",
                table: "ShopPrices",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_StorageId",
                table: "ShopPrices",
                column: "StorageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PcConfigurationRam");

            migrationBuilder.DropTable(
                name: "PcConfigurationStorage");

            migrationBuilder.DropTable(
                name: "ShopPrices");

            migrationBuilder.DropTable(
                name: "PcConfigurations");

            migrationBuilder.DropTable(
                name: "Rams");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "CpuCoolings");

            migrationBuilder.DropTable(
                name: "Cpus");

            migrationBuilder.DropTable(
                name: "GraphicCards");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "PowerSupplies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WaterCoolings");
        }
    }
}
