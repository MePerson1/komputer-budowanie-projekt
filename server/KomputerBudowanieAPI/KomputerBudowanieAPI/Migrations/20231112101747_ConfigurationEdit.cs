using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations");

            migrationBuilder.DropTable(
                name: "MemoryPcConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_PcConfigurations_FanId",
                table: "PcConfigurations");

            migrationBuilder.DropColumn(
                name: "FanId",
                table: "PcConfigurations");

            migrationBuilder.AlterColumn<int>(
                name: "PowerSupplyId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PcConfigurations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "MotherboardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GraphicCardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CpuId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CPU_CoolingId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "FanPcConfiguration",
                columns: table => new
                {
                    FansId = table.Column<int>(type: "integer", nullable: false),
                    PcConfigurationsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FanPcConfiguration", x => new { x.FansId, x.PcConfigurationsId });
                    table.ForeignKey(
                        name: "FK_FanPcConfiguration_Fans_FansId",
                        column: x => x.FansId,
                        principalTable: "Fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FanPcConfiguration_PcConfigurations_PcConfigurationsId",
                        column: x => x.PcConfigurationsId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcConfigurationStorage",
                columns: table => new
                {
                    MemoriesId = table.Column<int>(type: "integer", nullable: false),
                    PcConfigurationsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcConfigurationStorage", x => new { x.MemoriesId, x.PcConfigurationsId });
                    table.ForeignKey(
                        name: "FK_PcConfigurationStorage_Memories_MemoriesId",
                        column: x => x.MemoriesId,
                        principalTable: "Memories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcConfigurationStorage_PcConfigurations_PcConfigurationsId",
                        column: x => x.PcConfigurationsId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FanPcConfiguration_PcConfigurationsId",
                table: "FanPcConfiguration",
                column: "PcConfigurationsId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationStorage_PcConfigurationsId",
                table: "PcConfigurationStorage",
                column: "PcConfigurationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations",
                column: "CPU_CoolingId",
                principalTable: "CpuCoolings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations",
                column: "GraphicCardId",
                principalTable: "GraphicCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations",
                column: "MotherboardId",
                principalTable: "Motherboards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations",
                column: "PowerSupplyId",
                principalTable: "PowerSupplys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations");

            migrationBuilder.DropTable(
                name: "FanPcConfiguration");

            migrationBuilder.DropTable(
                name: "PcConfigurationStorage");

            migrationBuilder.AlterColumn<int>(
                name: "PowerSupplyId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PcConfigurations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MotherboardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GraphicCardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CpuId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CPU_CoolingId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FanId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MemoryPcConfiguration",
                columns: table => new
                {
                    MemoriesId = table.Column<int>(type: "integer", nullable: false),
                    PcConfigurationsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryPcConfiguration", x => new { x.MemoriesId, x.PcConfigurationsId });
                    table.ForeignKey(
                        name: "FK_MemoryPcConfiguration_Memories_MemoriesId",
                        column: x => x.MemoriesId,
                        principalTable: "Memories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemoryPcConfiguration_PcConfigurations_PcConfigurationsId",
                        column: x => x.PcConfigurationsId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurations_FanId",
                table: "PcConfigurations",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoryPcConfiguration_PcConfigurationsId",
                table: "MemoryPcConfiguration",
                column: "PcConfigurationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations",
                column: "CPU_CoolingId",
                principalTable: "CpuCoolings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations",
                column: "FanId",
                principalTable: "Fans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations",
                column: "GraphicCardId",
                principalTable: "GraphicCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations",
                column: "MotherboardId",
                principalTable: "Motherboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations",
                column: "PowerSupplyId",
                principalTable: "PowerSupplys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
