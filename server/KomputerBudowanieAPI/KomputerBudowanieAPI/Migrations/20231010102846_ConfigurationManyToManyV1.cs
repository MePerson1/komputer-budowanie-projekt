using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationManyToManyV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Memories_MemoryId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Rams_RamId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_MemoryId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_RamId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "MemoryId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "RamId",
                table: "PC_Configurations");

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
                        name: "FK_PcConfigurationMemories_PC_Configurations_PcConfigurationId",
                        column: x => x.PcConfigurationId,
                        principalTable: "PC_Configurations",
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
                        name: "FK_PcConfigurationRams_PC_Configurations_PcConfigurationId",
                        column: x => x.PcConfigurationId,
                        principalTable: "PC_Configurations",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PcConfigurationMemories");

            migrationBuilder.DropTable(
                name: "PcConfigurationRams");

            migrationBuilder.AddColumn<int>(
                name: "MemoryId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RamId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_MemoryId",
                table: "PC_Configurations",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_RamId",
                table: "PC_Configurations",
                column: "RamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_Memories_MemoryId",
                table: "PC_Configurations",
                column: "MemoryId",
                principalTable: "Memories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_Rams_RamId",
                table: "PC_Configurations",
                column: "RamId",
                principalTable: "Rams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
