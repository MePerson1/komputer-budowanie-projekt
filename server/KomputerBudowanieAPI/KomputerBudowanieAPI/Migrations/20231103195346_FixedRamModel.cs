using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedRamModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Rams");

            migrationBuilder.RenameColumn(
                name: "Voltage",
                table: "Rams",
                newName: "VoltageV");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Rams",
                newName: "PinType");

            migrationBuilder.RenameColumn(
                name: "Speed",
                table: "Rams",
                newName: "ModuleCount");

            migrationBuilder.RenameColumn(
                name: "ECC",
                table: "Rams",
                newName: "LowProfile");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rams",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "CapacityGB",
                table: "Rams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Rams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cooling",
                table: "Rams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FrequencyMHz",
                table: "Rams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasLighting",
                table: "Rams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MemoryType",
                table: "Rams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OverclockingProfile",
                table: "Rams",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacityGB",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Cooling",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "FrequencyMHz",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "HasLighting",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "MemoryType",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "OverclockingProfile",
                table: "Rams");

            migrationBuilder.RenameColumn(
                name: "VoltageV",
                table: "Rams",
                newName: "Voltage");

            migrationBuilder.RenameColumn(
                name: "PinType",
                table: "Rams",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ModuleCount",
                table: "Rams",
                newName: "Speed");

            migrationBuilder.RenameColumn(
                name: "LowProfile",
                table: "Rams",
                newName: "ECC");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rams",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Rams",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
