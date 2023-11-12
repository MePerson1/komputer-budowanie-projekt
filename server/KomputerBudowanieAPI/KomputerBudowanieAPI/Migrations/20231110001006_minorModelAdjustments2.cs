using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class minorModelAdjustments2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationStorage_Memories_MemoriesId",
                table: "PcConfigurationStorage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSupplys",
                table: "PowerSupplys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memories",
                table: "Memories");

            migrationBuilder.RenameTable(
                name: "PowerSupplys",
                newName: "PowerSupplies");

            migrationBuilder.RenameTable(
                name: "Memories",
                newName: "Storages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSupplies",
                table: "PowerSupplies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storages",
                table: "Storages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_PowerSupplies_PowerSupplyId",
                table: "PcConfigurations",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurationStorage_Storages_MemoriesId",
                table: "PcConfigurationStorage",
                column: "MemoriesId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_PowerSupplies_PowerSupplyId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationStorage_Storages_MemoriesId",
                table: "PcConfigurationStorage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storages",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PowerSupplies",
                table: "PowerSupplies");

            migrationBuilder.RenameTable(
                name: "Storages",
                newName: "Memories");

            migrationBuilder.RenameTable(
                name: "PowerSupplies",
                newName: "PowerSupplys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memories",
                table: "Memories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PowerSupplys",
                table: "PowerSupplys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations",
                column: "PowerSupplyId",
                principalTable: "PowerSupplys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
