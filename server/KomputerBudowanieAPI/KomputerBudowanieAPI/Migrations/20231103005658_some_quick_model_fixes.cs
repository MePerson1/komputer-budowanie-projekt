using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class some_quick_model_fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DLSS3Supported",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "HasDSub",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "HasDVI",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "HasMiniDisplayPort",
                table: "GraphicCards");

            migrationBuilder.RenameColumn(
                name: "LEDLighting",
                table: "GraphicCards",
                newName: "HasLEDLighting");

            migrationBuilder.RenameColumn(
                name: "HasUSBC",
                table: "GraphicCards",
                newName: "HasDLSS3Support");

            migrationBuilder.RenameColumn(
                name: "HDMICount",
                table: "GraphicCards",
                newName: "USBC");

            migrationBuilder.AddColumn<int>(
                name: "DSub",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DVI",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HDMI",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MiniDisplayPort",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SupportedMemoryTypes",
                table: "Cpus",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "IntegratedGraphics",
                table: "Cpus",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<float>(
                name: "PowerSupplyPower",
                table: "Cases",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "PowerSupply",
                table: "Cases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PanelTop",
                table: "Cases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PanelSide",
                table: "Cases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PanelRear",
                table: "Cases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PanelFront",
                table: "Cases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PanelBottom",
                table: "Cases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DSub",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "DVI",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "HDMI",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MiniDisplayPort",
                table: "GraphicCards");

            migrationBuilder.RenameColumn(
                name: "USBC",
                table: "GraphicCards",
                newName: "HDMICount");

            migrationBuilder.RenameColumn(
                name: "HasLEDLighting",
                table: "GraphicCards",
                newName: "LEDLighting");

            migrationBuilder.RenameColumn(
                name: "HasDLSS3Support",
                table: "GraphicCards",
                newName: "HasUSBC");

            migrationBuilder.AddColumn<bool>(
                name: "DLSS3Supported",
                table: "GraphicCards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDSub",
                table: "GraphicCards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasDVI",
                table: "GraphicCards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMiniDisplayPort",
                table: "GraphicCards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "SupportedMemoryTypes",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IntegratedGraphics",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PowerSupplyPower",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PowerSupply",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelTop",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelSide",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelRear",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelFront",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanelBottom",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
