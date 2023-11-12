using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class CpuCoolingModelFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Connectr",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "LiuquidCooling",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "NoiseLevel",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "CpuCoolings");

            migrationBuilder.RenameColumn(
                name: "Voltage",
                table: "CpuCoolings",
                newName: "WeightGrams");

            migrationBuilder.RenameColumn(
                name: "TowerCount",
                table: "CpuCoolings",
                newName: "HeatPipesCount");

            migrationBuilder.RenameColumn(
                name: "TDP",
                table: "CpuCoolings",
                newName: "HeatPipeDiameterMM");

            migrationBuilder.RenameColumn(
                name: "Sockets",
                table: "CpuCoolings",
                newName: "ProcessorSocket");

            migrationBuilder.RenameColumn(
                name: "RPM",
                table: "CpuCoolings",
                newName: "FanDiameterMM");

            migrationBuilder.RenameColumn(
                name: "RGBSupport",
                table: "CpuCoolings",
                newName: "HasLighting");

            migrationBuilder.RenameColumn(
                name: "Material",
                table: "CpuCoolings",
                newName: "MountingType");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CpuCoolings",
                newName: "ColorElement");

            migrationBuilder.RenameColumn(
                name: "CoolingType",
                table: "CpuCoolings",
                newName: "BaseMaterial");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "CpuCoolings",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<float>(
                name: "AirflowCFM",
                table: "CpuCoolings",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DepthMM",
                table: "CpuCoolings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "HeightMM",
                table: "CpuCoolings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "LifespanHours",
                table: "CpuCoolings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxFanSpeedPerMin",
                table: "CpuCoolings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaxNoiseLevelinDBA",
                table: "CpuCoolings",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxTDPinW",
                table: "CpuCoolings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WidthMM",
                table: "CpuCoolings",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirflowCFM",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "DepthMM",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "HeightMM",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "LifespanHours",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "MaxFanSpeedPerMin",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "MaxNoiseLevelinDBA",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "MaxTDPinW",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "WidthMM",
                table: "CpuCoolings");

            migrationBuilder.RenameColumn(
                name: "WeightGrams",
                table: "CpuCoolings",
                newName: "Voltage");

            migrationBuilder.RenameColumn(
                name: "ProcessorSocket",
                table: "CpuCoolings",
                newName: "Sockets");

            migrationBuilder.RenameColumn(
                name: "MountingType",
                table: "CpuCoolings",
                newName: "Material");

            migrationBuilder.RenameColumn(
                name: "HeatPipesCount",
                table: "CpuCoolings",
                newName: "TowerCount");

            migrationBuilder.RenameColumn(
                name: "HeatPipeDiameterMM",
                table: "CpuCoolings",
                newName: "TDP");

            migrationBuilder.RenameColumn(
                name: "HasLighting",
                table: "CpuCoolings",
                newName: "RGBSupport");

            migrationBuilder.RenameColumn(
                name: "FanDiameterMM",
                table: "CpuCoolings",
                newName: "RPM");

            migrationBuilder.RenameColumn(
                name: "ColorElement",
                table: "CpuCoolings",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "BaseMaterial",
                table: "CpuCoolings",
                newName: "CoolingType");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "CpuCoolings",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "Connectr",
                table: "CpuCoolings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "CpuCoolings",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "LiuquidCooling",
                table: "CpuCoolings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "NoiseLevel",
                table: "CpuCoolings",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "CpuCoolings",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
