using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class CaseModelFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Cases",
                newName: "WidthCM");

            migrationBuilder.RenameColumn(
                name: "WaterCoolingSupport",
                table: "Cases",
                newName: "IsMuted");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Cases",
                newName: "PanelTop");

            migrationBuilder.RenameColumn(
                name: "PowerSupplyType",
                table: "Cases",
                newName: "PanelSide");

            migrationBuilder.RenameColumn(
                name: "MotherboardType",
                table: "Cases",
                newName: "PanelRear");

            migrationBuilder.RenameColumn(
                name: "MaxGPUSize",
                table: "Cases",
                newName: "USBTypeCCount");

            migrationBuilder.RenameColumn(
                name: "Lightning",
                table: "Cases",
                newName: "HasWindow");

            migrationBuilder.RenameColumn(
                name: "Lenght",
                table: "Cases",
                newName: "WeightKG");

            migrationBuilder.RenameColumn(
                name: "Internal3_5Bays",
                table: "Cases",
                newName: "USBTwoCount");

            migrationBuilder.RenameColumn(
                name: "Internal2_5Bays",
                table: "Cases",
                newName: "USBTurboChargingCount");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Cases",
                newName: "PowerSupplyPower");

            migrationBuilder.RenameColumn(
                name: "FormFactor",
                table: "Cases",
                newName: "PanelFront");

            migrationBuilder.RenameColumn(
                name: "External5_25Bays",
                table: "Cases",
                newName: "USBThreePointTwoCount");

            migrationBuilder.AlterColumn<string>(
                name: "PowerSupply",
                table: "Cases",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cases",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CaseType",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Compatibility",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExternalBaysFivePointTwoFiveInch",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExternalBaysThreePointFiveInch",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasAudioPort",
                table: "Cases",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLightning",
                table: "Cases",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMemoryCardReader",
                table: "Cases",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMicrophonePort",
                table: "Cases",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "HeightCM",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "InternalBaysThreePointFiveInch",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InternalBaysTwoPointFiveInch",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "LengthCM",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxCoolingSystemHeightCM",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxGPULengthCM",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "PanelBottom",
                table: "Cases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "USBThreeCount",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "USBThreePointOneCount",
                table: "Cases",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseType",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Compatibility",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ExternalBaysFivePointTwoFiveInch",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ExternalBaysThreePointFiveInch",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "HasAudioPort",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "HasLightning",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "HasMemoryCardReader",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "HasMicrophonePort",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "HeightCM",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "InternalBaysThreePointFiveInch",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "InternalBaysTwoPointFiveInch",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "LengthCM",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "MaxCoolingSystemHeightCM",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "MaxGPULengthCM",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "PanelBottom",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "USBThreeCount",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "USBThreePointOneCount",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "WidthCM",
                table: "Cases",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "WeightKG",
                table: "Cases",
                newName: "Lenght");

            migrationBuilder.RenameColumn(
                name: "USBTypeCCount",
                table: "Cases",
                newName: "MaxGPUSize");

            migrationBuilder.RenameColumn(
                name: "USBTwoCount",
                table: "Cases",
                newName: "Internal3_5Bays");

            migrationBuilder.RenameColumn(
                name: "USBTurboChargingCount",
                table: "Cases",
                newName: "Internal2_5Bays");

            migrationBuilder.RenameColumn(
                name: "USBThreePointTwoCount",
                table: "Cases",
                newName: "External5_25Bays");

            migrationBuilder.RenameColumn(
                name: "PowerSupplyPower",
                table: "Cases",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "PanelTop",
                table: "Cases",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "PanelSide",
                table: "Cases",
                newName: "PowerSupplyType");

            migrationBuilder.RenameColumn(
                name: "PanelRear",
                table: "Cases",
                newName: "MotherboardType");

            migrationBuilder.RenameColumn(
                name: "PanelFront",
                table: "Cases",
                newName: "FormFactor");

            migrationBuilder.RenameColumn(
                name: "IsMuted",
                table: "Cases",
                newName: "WaterCoolingSupport");

            migrationBuilder.RenameColumn(
                name: "HasWindow",
                table: "Cases",
                newName: "Lightning");

            migrationBuilder.AlterColumn<bool>(
                name: "PowerSupply",
                table: "Cases",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
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
