using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class priceISBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "WaterCoolings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Storages",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Rams",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "PowerSupplies",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Motherboards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "GraphicCards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Cpus",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "CpuCoolings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "WaterCoolings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cases");
        }
    }
}
