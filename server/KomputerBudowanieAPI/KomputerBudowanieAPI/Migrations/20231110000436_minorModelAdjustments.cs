using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class minorModelAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemoryPcConfiguration");

            migrationBuilder.RenameColumn(
                name: "PCIE8Pin_6Plus4",
                table: "PowerSupplys",
                newName: "PCIE8Pin_6Plus2");

            migrationBuilder.AlterColumn<float>(
                name: "ThiccnessMM",
                table: "Memories",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

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
                name: "IX_PcConfigurationStorage_PcConfigurationsId",
                table: "PcConfigurationStorage",
                column: "PcConfigurationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PcConfigurationStorage");

            migrationBuilder.RenameColumn(
                name: "PCIE8Pin_6Plus2",
                table: "PowerSupplys",
                newName: "PCIE8Pin_6Plus4");

            migrationBuilder.AlterColumn<float>(
                name: "ThiccnessMM",
                table: "Memories",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

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
                name: "IX_MemoryPcConfiguration_PcConfigurationsId",
                table: "MemoryPcConfiguration",
                column: "PcConfigurationsId");
        }
    }
}
