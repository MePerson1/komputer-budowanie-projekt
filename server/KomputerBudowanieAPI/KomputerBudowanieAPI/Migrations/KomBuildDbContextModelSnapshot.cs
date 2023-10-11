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

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExpansionSlots")
                        .HasColumnType("integer");

                    b.Property<int>("External5_25Bays")
                        .HasColumnType("integer");

                    b.Property<string>("FormFactor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<int>("Internal2_5Bays")
                        .HasColumnType("integer");

                    b.Property<int>("Internal3_5Bays")
                        .HasColumnType("integer");

                    b.Property<float>("Lenght")
                        .HasColumnType("real");

                    b.Property<bool>("Lightning")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxGPUSize")
                        .HasColumnType("integer");

                    b.Property<string>("MotherboardType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("PowerSupply")
                        .HasColumnType("boolean");

                    b.Property<string>("PowerSupplyType")
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

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("WaterCoolingSupport")
                        .HasColumnType("boolean");

                    b.Property<float>("Width")
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

                    b.Property<string>("CacheSize")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ClockFrequency")
                        .HasColumnType("double precision");

                    b.Property<int>("CoreCount")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HyperThreading")
                        .HasColumnType("boolean");

                    b.Property<bool>("IntegratedGraphics")
                        .HasColumnType("boolean");

                    b.Property<int>("MTP")
                        .HasColumnType("integer");

                    b.Property<string>("MemoryType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("OverclockingSupport")
                        .HasColumnType("boolean");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProducerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SocketType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TDP")
                        .HasColumnType("integer");

                    b.Property<int>("ThreadsCount")
                        .HasColumnType("integer");

                    b.Property<double>("TurboClockFrequency")
                        .HasColumnType("double precision");

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

                    b.Property<string>("Description")
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

                    b.HasKey("Id");

                    b.ToTable("GraphicCards");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Memory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
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
                        .IsRequired()
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

                    b.Property<int>("UserId")
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

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PcConfigurationMemory", b =>
                {
                    b.Property<Guid>("PcConfigurationId")
                        .HasColumnType("uuid");

                    b.Property<int>("MemoryId")
                        .HasColumnType("integer");

                    b.HasKey("PcConfigurationId", "MemoryId");

                    b.HasIndex("MemoryId");

                    b.ToTable("PcConfigurationMemories");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PcConfigurationRam", b =>
                {
                    b.Property<Guid>("PcConfigurationId")
                        .HasColumnType("uuid");

                    b.Property<int>("RamId")
                        .HasColumnType("integer");

                    b.HasKey("PcConfigurationId", "RamId");

                    b.HasIndex("RamId");

                    b.ToTable("PcConfigurationRams");
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

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CPU_Cooling");

                    b.Navigation("Case");

                    b.Navigation("Cpu");

                    b.Navigation("Fan");

                    b.Navigation("GraphicCard");

                    b.Navigation("Motherboard");

                    b.Navigation("PowerSupply");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PcConfigurationMemory", b =>
                {
                    b.HasOne("KomputerBudowanieAPI.Models.Memory", "Memory")
                        .WithMany("PcConfigurationMemories")
                        .HasForeignKey("MemoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.PcConfiguration", "PcConfiguration")
                        .WithMany("PcConfigurationMemories")
                        .HasForeignKey("PcConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Memory");

                    b.Navigation("PcConfiguration");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PcConfigurationRam", b =>
                {
                    b.HasOne("KomputerBudowanieAPI.Models.PcConfiguration", "PcConfiguration")
                        .WithMany("PcConfigurationRams")
                        .HasForeignKey("PcConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KomputerBudowanieAPI.Models.Ram", "Ram")
                        .WithMany("PcConfigurationRams")
                        .HasForeignKey("RamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PcConfiguration");

                    b.Navigation("Ram");
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

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Memory", b =>
                {
                    b.Navigation("PcConfigurationMemories");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Motherboard", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PcConfiguration", b =>
                {
                    b.Navigation("PcConfigurationMemories");

                    b.Navigation("PcConfigurationRams");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.PowerSupply", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.Ram", b =>
                {
                    b.Navigation("PcConfigurationRams");
                });

            modelBuilder.Entity("KomputerBudowanieAPI.Models.User", b =>
                {
                    b.Navigation("Configurations");
                });
#pragma warning restore 612, 618
        }
    }
}
