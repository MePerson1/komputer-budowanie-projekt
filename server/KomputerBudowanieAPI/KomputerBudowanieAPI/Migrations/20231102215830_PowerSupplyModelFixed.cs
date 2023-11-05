using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class PowerSupplyModelFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "PowerSupplys",
                newName: "WidthMM");

            migrationBuilder.RenameColumn(
                name: "Protection",
                table: "PowerSupplys",
                newName: "Security");

            migrationBuilder.RenameColumn(
                name: "PowerOutput",
                table: "PowerSupplys",
                newName: "PowerW");

            migrationBuilder.RenameColumn(
                name: "Modularity",
                table: "PowerSupplys",
                newName: "ModularCabling");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "PowerSupplys",
                newName: "PCIE8Pin_6Plus4");

            migrationBuilder.RenameColumn(
                name: "Depth",
                table: "PowerSupplys",
                newName: "PCIE8Pin");

            migrationBuilder.RenameColumn(
                name: "ATX24_Pin",
                table: "PowerSupplys",
                newName: "PCIE6Pin");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PowerSupplys",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ATX24Pin_20Plus4",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPU4Pin",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPU8Pin",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPU8Pin_4Plus4",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Certificate",
                table: "PowerSupplys",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cooling",
                table: "PowerSupplys",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DepthMM",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FanDiameterMM",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasLighting",
                table: "PowerSupplys",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HeightMM",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PCIE16Pin",
                table: "PowerSupplys",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATX24Pin_20Plus4",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "CPU4Pin",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "CPU8Pin",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "CPU8Pin_4Plus4",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "Cooling",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "DepthMM",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "FanDiameterMM",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "HasLighting",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "HeightMM",
                table: "PowerSupplys");

            migrationBuilder.DropColumn(
                name: "PCIE16Pin",
                table: "PowerSupplys");

            migrationBuilder.RenameColumn(
                name: "WidthMM",
                table: "PowerSupplys",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Security",
                table: "PowerSupplys",
                newName: "Protection");

            migrationBuilder.RenameColumn(
                name: "PowerW",
                table: "PowerSupplys",
                newName: "PowerOutput");

            migrationBuilder.RenameColumn(
                name: "PCIE8Pin_6Plus4",
                table: "PowerSupplys",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "PCIE8Pin",
                table: "PowerSupplys",
                newName: "Depth");

            migrationBuilder.RenameColumn(
                name: "PCIE6Pin",
                table: "PowerSupplys",
                newName: "ATX24_Pin");

            migrationBuilder.RenameColumn(
                name: "ModularCabling",
                table: "PowerSupplys",
                newName: "Modularity");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PowerSupplys",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
