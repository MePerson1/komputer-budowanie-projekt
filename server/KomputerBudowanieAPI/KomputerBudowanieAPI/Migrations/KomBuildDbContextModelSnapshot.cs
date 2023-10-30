﻿// <auto-generated />
using System;
using KomputerBudowanieAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    [DbContext(typeof(KomBuildDbContext))]
    partial class KomBuildDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CaseType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Compatibility")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ExpansionSlots")
                        .HasColumnType("integer");

                    b.Property<int>("ExternalBaysFivePointTwoFiveInch")
                        .HasColumnType("integer");

                    b.Property<int>("ExternalBaysThreePointFiveInch")
                        .HasColumnType("integer");

                    b.Property<bool>("HasAudioPort")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasLightning")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasMemoryCardReader")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasMicrophonePort")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasWindow")
                        .HasColumnType("boolean");

                    b.Property<float>("HeightCM")
                        .HasColumnType("real");

                    b.Property<int>("InternalBaysThreePointFiveInch")
                        .HasColumnType("integer");

                    b.Property<int>("InternalBaysTwoPointFiveInch")
                        .HasColumnType("integer");

                    b.Property<bool>("IsMuted")
                        .HasColumnType("boolean");

                    b.Property<float>("LengthCM")
                        .HasColumnType("real");

                    b.Property<float>("MaxCoolingSystemHeightCM")
                        .HasColumnType("real");

                    b.Property<float>("MaxGPULengthCM")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PanelBottom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PanelFront")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PanelRear")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PanelSide")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PanelTop")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PowerSupply")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("PowerSupplyPower")
                        .HasColumnType("real");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("USBThreeCount")
                        .HasColumnType("integer");

                    b.Property<int>("USBThreePointOneCount")
                        .HasColumnType("integer");

                    b.Property<int>("USBThreePointTwoCount")
                        .HasColumnType("integer");

                    b.Property<int>("USBTurboChargingCount")
                        .HasColumnType("integer");

                    b.Property<int>("USBTwoCount")
                        .HasColumnType("integer");

                    b.Property<int>("USBTypeCCount")
                        .HasColumnType("integer");

                    b.Property<float>("WeightKG")
                        .HasColumnType("real");

                    b.Property<float>("WidthCM")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Cpu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AddedEquipment")
                        .HasColumnType("text");

                    b.Property<string>("Architecture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HasIncludedCooling")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasUnlockedMultiplier")
                        .HasColumnType("boolean");

                    b.Property<string>("IntegratedGraphics")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("L1Cache")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("L2Cache")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("L3Cache")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Line")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ManufacturingProcess")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("MaxOperatingTempC")
                        .HasColumnType("integer");

                    b.Property<float>("MaxTurboFrequencyGHz")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfCores")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfThreads")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<float>("ProcessorBaseFrequencyGHz")
                        .HasColumnType("real");

                    b.Property<string>("ProcessorMicroarchitecture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SocketType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SupportedMemoryTypes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TDPinW")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Cpus");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.CpuCooling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Connectr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CoolingType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FanCount")
                        .HasColumnType("integer");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<bool>("LiuquidCooling")
                        .HasColumnType("boolean");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("NoiseLevel")
                        .HasColumnType("double precision");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("RGBSupport")
                        .HasColumnType("boolean");

                    b.Property<int>("RPM")
                        .HasColumnType("integer");

                    b.Property<string>("Sockets")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TDP")
                        .HasColumnType("integer");

                    b.Property<int>("TowerCount")
                        .HasColumnType("integer");

                    b.Property<int>("Voltage")
                        .HasColumnType("integer");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("CpuCoolings");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Fan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<float>("Lenght")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<int>("Voltatge")
                        .HasColumnType("integer");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Fans");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.GraphicCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BoostClockMHz")
                        .HasColumnType("integer");

                    b.Property<int>("CardLengthMM")
                        .HasColumnType("integer");

                    b.Property<string>("ChipsetProducer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ChipsetType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConnectorType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CoolingType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CoreClockMHz")
                        .HasColumnType("integer");

                    b.Property<bool>("DLSS3Supported")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DisplayPortCount")
                        .HasColumnType("integer");

                    b.Property<int>("FanCount")
                        .HasColumnType("integer");

                    b.Property<int>("HDMICount")
                        .HasColumnType("integer");

                    b.Property<bool>("HasDSub")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasDVI")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasMiniDisplayPort")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasUSBC")
                        .HasColumnType("boolean");

                    b.Property<bool>("LEDLighting")
                        .HasColumnType("boolean");

                    b.Property<int>("MemoryBusWidthBits")
                        .HasColumnType("integer");

                    b.Property<int>("MemoryClockMHz")
                        .HasColumnType("integer");

                    b.Property<int>("MemorySizeGB")
                        .HasColumnType("integer");

                    b.Property<string>("MemoryType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PowerConnectors")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ROPUnits")
                        .HasColumnType("integer");

                    b.Property<int>("RTCores")
                        .HasColumnType("integer");

                    b.Property<int>("RecommendedPSUCapacityW")
                        .HasColumnType("integer");

                    b.Property<string>("Resolution")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StreamProcessors")
                        .HasColumnType("integer");

                    b.Property<int>("TensorCores")
                        .HasColumnType("integer");

                    b.Property<int>("TextureUnits")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("GraphicCards");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Memory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("Interface")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReadSpeed")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WriteSpeed")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Memories");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Motherboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AudioChannels")
                        .HasColumnType("integer");

                    b.Property<bool>("BluetoothSupport")
                        .HasColumnType("boolean");

                    b.Property<string>("Chipset")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EthernetPorts")
                        .HasColumnType("integer");

                    b.Property<string>("FormFactor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("M2Slots")
                        .HasColumnType("integer");

                    b.Property<int>("MaxRam")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PCIe_x16Slots")
                        .HasColumnType("integer");

                    b.Property<int>("PCIe_x1Slots")
                        .HasColumnType("integer");

                    b.Property<int>("PCIe_x4Slots")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RamSlotsCount")
                        .HasColumnType("integer");

                    b.Property<string[]>("RamType")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("SATAPorts")
                        .HasColumnType("integer");

                    b.Property<string>("Socket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("WiFiSupport")
                        .HasColumnType("boolean");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Motherboards");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PcConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CPU_CoolingId")
                        .HasColumnType("integer");

                    b.Property<int>("CaseId")
                        .HasColumnType("integer");

                    b.Property<int>("CpuId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("FanId")
                        .HasColumnType("integer");

                    b.Property<int>("GraphicCardId")
                        .HasColumnType("integer");

                    b.Property<int>("MotherboardId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PowerSupplyId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CPU_CoolingId");

                    b.HasIndex("CaseId");

                    b.HasIndex("CpuId");

                    b.HasIndex("FanId");

                    b.HasIndex("GraphicCardId");

                    b.HasIndex("MotherboardId");

                    b.HasIndex("PowerSupplyId");

                    b.HasIndex("UserId");

                    b.ToTable("PcConfigurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PowerSupply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ATX24_Pin")
                        .HasColumnType("integer");

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EfficiencyRating")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FormFactor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Modularity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Molex")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PowerFactorCorrection")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PowerOutput")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Protection")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Sata")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PowerSupplys");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Ram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ECC")
                        .HasColumnType("boolean");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<int>("LatencyCL")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Voltage")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Rams");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MemoryPcConfiguration", b =>
                {
                    b.Property<int>("MemoriesId")
                        .HasColumnType("integer");

                    b.Property<Guid>("PcConfigurationsId")
                        .HasColumnType("uuid");

                    b.HasKey("MemoriesId", "PcConfigurationsId");

                    b.HasIndex("PcConfigurationsId");

                    b.ToTable("MemoryPcConfiguration");
                });

            modelBuilder.Entity("PcConfigurationRam", b =>
                {
                    b.Property<Guid>("PcConfigurationsId")
                        .HasColumnType("uuid");

                    b.Property<int>("RamsId")
                        .HasColumnType("integer");

                    b.HasKey("PcConfigurationsId", "RamsId");

                    b.HasIndex("RamsId");

                    b.ToTable("PcConfigurationRam");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PcConfiguration", b =>
                {
                    b.HasOne("KomputerBudowanieAPI.Models.CpuCooling", "CPU_Cooling")
                        .WithMany("Configurations")
                        .HasForeignKey("CPU_CoolingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.Case", "Case")
                        .WithMany("Configurations")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.Cpu", "Cpu")
                        .WithMany("Configurations")
                        .HasForeignKey("CpuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.Fan", "Fan")
                        .WithMany("Configurations")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.GraphicCard", "GraphicCard")
                        .WithMany("Configurations")
                        .HasForeignKey("GraphicCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.Motherboard", "Motherboard")
                        .WithMany("Configurations")
                        .HasForeignKey("MotherboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.PowerSupply", "PowerSupply")
                        .WithMany("Configurations")
                        .HasForeignKey("PowerSupplyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.User", "User")
                        .WithMany("Configurations")
                        .HasForeignKey("UserId");

                    b.Navigation("CPU_Cooling");

                    b.Navigation("Case");

                    b.Navigation("Cpu");

                    b.Navigation("Fan");

                    b.Navigation("GraphicCard");

                    b.Navigation("Motherboard");

                    b.Navigation("PowerSupply");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MemoryPcConfiguration", b =>
                {
                    b.HasOne("KomputerBudowanieAPI.Models.Memory", null)
                        .WithMany()
                        .HasForeignKey("MemoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.PcConfiguration", null)
                        .WithMany()
                        .HasForeignKey("PcConfigurationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PcConfigurationRam", b =>
                {
                    b.HasOne("KomputerBudowanieAPI.Models.PcConfiguration", null)
                        .WithMany()
                        .HasForeignKey("PcConfigurationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.Ram", null)
                        .WithMany()
                        .HasForeignKey("RamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Case", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Cpu", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.CpuCooling", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Fan", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.GraphicCard", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Motherboard", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PowerSupply", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.User", b =>
                {
                    b.Navigation("Configurations");
                });
#pragma warning restore 612, 618
        }
    }
}
