using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class testingManytoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PcConfigurationMemories");

            migrationBuilder.DropTable(
                name: "PcConfigurationRams");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PcConfigurations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

            migrationBuilder.CreateTable(
                name: "PcConfigurationRam",
                columns: table => new
                {
                    PcConfigurationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RamsId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_MemoryPcConfiguration_PcConfigurationsId",
                table: "MemoryPcConfiguration",
                column: "PcConfigurationsId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationRam_RamsId",
                table: "PcConfigurationRam",
                column: "RamsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemoryPcConfiguration");

            migrationBuilder.DropTable(
                name: "PcConfigurationRam");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PcConfigurations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PcConfigurationMemories",
                columns: table => new
                {
                    PcConfigurationId = table.Column<Guid>(type: "uuid", nullable: false),
                    MemoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcConfigurationMemories", x => new { x.PcConfigurationId, x.MemoryId });
                    table.ForeignKey(
                        name: "FK_PcConfigurationMemories_Memories_MemoryId",
                        column: x => x.MemoryId,
                        principalTable: "Memories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcConfigurationMemories_PcConfigurations_PcConfigurationId",
                        column: x => x.PcConfigurationId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcConfigurationRams",
                columns: table => new
                {
                    PcConfigurationId = table.Column<Guid>(type: "uuid", nullable: false),
                    RamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcConfigurationRams", x => new { x.PcConfigurationId, x.RamId });
                    table.ForeignKey(
                        name: "FK_PcConfigurationRams_PcConfigurations_PcConfigurationId",
                        column: x => x.PcConfigurationId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcConfigurationRams_Rams_RamId",
                        column: x => x.RamId,
                        principalTable: "Rams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationMemories_MemoryId",
                table: "PcConfigurationMemories",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationRams_RamId",
                table: "PcConfigurationRams",
                column: "RamId");
        }
    }
}
