using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedWaterCoolingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Fans",
                newName: "RadiatorWidthMM");

            migrationBuilder.RenameColumn(
                name: "Voltatge",
                table: "Fans",
                newName: "MaxFanSpeedRPM");

            migrationBuilder.RenameColumn(
                name: "Speed",
                table: "Fans",
                newName: "FanDiameterMM");

            migrationBuilder.RenameColumn(
                name: "Lenght",
                table: "Fans",
                newName: "RadiatorSizeMM");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Fans",
                newName: "RadiatorLengthMM");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Fans",
                newName: "PumpConnector");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Fans",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "AMDCompatibility",
                table: "Fans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FanConnector",
                table: "Fans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FanCount",
                table: "Fans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasPWMControl",
                table: "Fans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IntelCompatibility",
                table: "Fans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LEDConnector",
                table: "Fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lighting",
                table: "Fans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "MaxAirflowCFM",
                table: "Fans",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxNoiseLevelDBa",
                table: "Fans",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "RadiatorHeightMM",
                table: "Fans",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "WeightG",
                table: "Fans",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AMDCompatibility",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "FanConnector",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "FanCount",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "HasPWMControl",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "IntelCompatibility",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "LEDConnector",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Lighting",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "MaxAirflowCFM",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "MaxNoiseLevelDBa",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "RadiatorHeightMM",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "WeightG",
                table: "Fans");

            migrationBuilder.RenameColumn(
                name: "RadiatorWidthMM",
                table: "Fans",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "RadiatorSizeMM",
                table: "Fans",
                newName: "Lenght");

            migrationBuilder.RenameColumn(
                name: "RadiatorLengthMM",
                table: "Fans",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "PumpConnector",
                table: "Fans",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "MaxFanSpeedRPM",
                table: "Fans",
                newName: "Voltatge");

            migrationBuilder.RenameColumn(
                name: "FanDiameterMM",
                table: "Fans",
                newName: "Speed");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Fans",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
