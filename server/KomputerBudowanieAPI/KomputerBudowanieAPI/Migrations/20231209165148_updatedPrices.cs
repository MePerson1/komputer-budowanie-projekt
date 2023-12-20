using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopPrices_Rams_RamId",
                table: "ShopPrices");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "WaterCoolings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GraphicCards");

            migrationBuilder.RenameColumn(
                name: "RamId",
                table: "ShopPrices",
                newName: "WaterCoolingId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPrices_RamId",
                table: "ShopPrices",
                newName: "IX_ShopPrices_WaterCoolingId");

            migrationBuilder.AddColumn<int>(
                name: "GraphicCardId",
                table: "ShopPrices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemoryId",
                table: "ShopPrices",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_GraphicCardId",
                table: "ShopPrices",
                column: "GraphicCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrices_MemoryId",
                table: "ShopPrices",
                column: "MemoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPrices_GraphicCards_GraphicCardId",
                table: "ShopPrices",
                column: "GraphicCardId",
                principalTable: "GraphicCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPrices_Rams_MemoryId",
                table: "ShopPrices",
                column: "MemoryId",
                principalTable: "Rams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPrices_WaterCoolings_WaterCoolingId",
                table: "ShopPrices",
                column: "WaterCoolingId",
                principalTable: "WaterCoolings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopPrices_GraphicCards_GraphicCardId",
                table: "ShopPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPrices_Rams_MemoryId",
                table: "ShopPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopPrices_WaterCoolings_WaterCoolingId",
                table: "ShopPrices");

            migrationBuilder.DropIndex(
                name: "IX_ShopPrices_GraphicCardId",
                table: "ShopPrices");

            migrationBuilder.DropIndex(
                name: "IX_ShopPrices_MemoryId",
                table: "ShopPrices");

            migrationBuilder.DropColumn(
                name: "GraphicCardId",
                table: "ShopPrices");

            migrationBuilder.DropColumn(
                name: "MemoryId",
                table: "ShopPrices");

            migrationBuilder.RenameColumn(
                name: "WaterCoolingId",
                table: "ShopPrices",
                newName: "RamId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopPrices_WaterCoolingId",
                table: "ShopPrices",
                newName: "IX_ShopPrices_RamId");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "WaterCoolings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "GraphicCards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPrices_Rams_RamId",
                table: "ShopPrices",
                column: "RamId",
                principalTable: "Rams",
                principalColumn: "Id");
        }
    }
}
