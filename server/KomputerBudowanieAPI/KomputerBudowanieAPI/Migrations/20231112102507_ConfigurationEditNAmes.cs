using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationEditNAmes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationStorage_Memories_MemoriesId",
                table: "PcConfigurationStorage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PcConfigurationStorage",
                table: "PcConfigurationStorage");

            migrationBuilder.DropIndex(
                name: "IX_PcConfigurationStorage_PcConfigurationsId",
                table: "PcConfigurationStorage");

            migrationBuilder.RenameColumn(
                name: "MemoriesId",
                table: "PcConfigurationStorage",
                newName: "StoragesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PcConfigurationStorage",
                table: "PcConfigurationStorage",
                columns: new[] { "PcConfigurationsId", "StoragesId" });

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationStorage_StoragesId",
                table: "PcConfigurationStorage",
                column: "StoragesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurationStorage_Memories_StoragesId",
                table: "PcConfigurationStorage",
                column: "StoragesId",
                principalTable: "Memories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationStorage_Memories_StoragesId",
                table: "PcConfigurationStorage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PcConfigurationStorage",
                table: "PcConfigurationStorage");

            migrationBuilder.DropIndex(
                name: "IX_PcConfigurationStorage_StoragesId",
                table: "PcConfigurationStorage");

            migrationBuilder.RenameColumn(
                name: "StoragesId",
                table: "PcConfigurationStorage",
                newName: "MemoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PcConfigurationStorage",
                table: "PcConfigurationStorage",
                columns: new[] { "MemoriesId", "PcConfigurationsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PcConfigurationStorage_PcConfigurationsId",
                table: "PcConfigurationStorage",
                column: "PcConfigurationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurationStorage_Memories_MemoriesId",
                table: "PcConfigurationStorage",
                column: "MemoriesId",
                principalTable: "Memories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
