using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedModelsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ECC",
                table: "Rams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Rams",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "LatencyCL",
                table: "Rams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Rams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Voltage",
                table: "Rams",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Memories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Memories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Memories",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Interface",
                table: "Memories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReadSpeed",
                table: "Memories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Memories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WriteSpeed",
                table: "Memories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CoolingType",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CoreClockspeed",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FanCount",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "GraphicCards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Length",
                table: "GraphicCards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "MemoryBandwith",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemoryBus",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemoryClockspeed",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemorySize",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MemoryType",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PsuPower",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RayTraycing",
                table: "GraphicCards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Slots",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "GraphicCards",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ECC",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "LatencyCL",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Interface",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "ReadSpeed",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "WriteSpeed",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "CoolingType",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "CoreClockspeed",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "FanCount",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MemoryBandwith",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MemoryBus",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MemoryClockspeed",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MemorySize",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MemoryType",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "PsuPower",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "RayTraycing",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Slots",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "GraphicCards");
        }
    }
}
