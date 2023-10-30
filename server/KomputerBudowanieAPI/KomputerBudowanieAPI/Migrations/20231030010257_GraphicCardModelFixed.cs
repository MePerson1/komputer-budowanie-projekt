using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class GraphicCardModelFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "PackagingVersion",
                table: "Cpus");

            migrationBuilder.RenameColumn(
                name: "Slots",
                table: "GraphicCards",
                newName: "TextureUnits");

            migrationBuilder.RenameColumn(
                name: "Series",
                table: "GraphicCards",
                newName: "Resolution");

            migrationBuilder.RenameColumn(
                name: "RayTraycing",
                table: "GraphicCards",
                newName: "LEDLighting");

            migrationBuilder.RenameColumn(
                name: "PsuPower",
                table: "GraphicCards",
                newName: "TensorCores");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "GraphicCards",
                newName: "PowerConnectors");

            migrationBuilder.RenameColumn(
                name: "MemorySize",
                table: "GraphicCards",
                newName: "StreamProcessors");

            migrationBuilder.RenameColumn(
                name: "MemoryClockspeed",
                table: "GraphicCards",
                newName: "RecommendedPSUCapacityW");

            migrationBuilder.RenameColumn(
                name: "MemoryBus",
                table: "GraphicCards",
                newName: "RTCores");

            migrationBuilder.RenameColumn(
                name: "MemoryBandwith",
                table: "GraphicCards",
                newName: "ROPUnits");

            migrationBuilder.RenameColumn(
                name: "CoreClockspeed",
                table: "GraphicCards",
                newName: "MemorySizeGB");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "GraphicCards",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GraphicCards",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "BoostClockMHz",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardLengthMM",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChipsetProducer",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChipsetType",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConnectorType",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CoreClockMHz",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DLSS3Supported",
                table: "GraphicCards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DisplayPortCount",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HDMICount",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<bool>(
                name: "HasUSBC",
                table: "GraphicCards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MemoryBusWidthBits",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemoryClockMHz",
                table: "GraphicCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoostClockMHz",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "CardLengthMM",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "ChipsetProducer",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "ChipsetType",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "ConnectorType",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "CoreClockMHz",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "DLSS3Supported",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "DisplayPortCount",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "HDMICount",
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

            migrationBuilder.DropColumn(
                name: "HasUSBC",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MemoryBusWidthBits",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "MemoryClockMHz",
                table: "GraphicCards");

            migrationBuilder.RenameColumn(
                name: "TextureUnits",
                table: "GraphicCards",
                newName: "Slots");

            migrationBuilder.RenameColumn(
                name: "TensorCores",
                table: "GraphicCards",
                newName: "PsuPower");

            migrationBuilder.RenameColumn(
                name: "StreamProcessors",
                table: "GraphicCards",
                newName: "MemorySize");

            migrationBuilder.RenameColumn(
                name: "Resolution",
                table: "GraphicCards",
                newName: "Series");

            migrationBuilder.RenameColumn(
                name: "RecommendedPSUCapacityW",
                table: "GraphicCards",
                newName: "MemoryClockspeed");

            migrationBuilder.RenameColumn(
                name: "RTCores",
                table: "GraphicCards",
                newName: "MemoryBus");

            migrationBuilder.RenameColumn(
                name: "ROPUnits",
                table: "GraphicCards",
                newName: "MemoryBandwith");

            migrationBuilder.RenameColumn(
                name: "PowerConnectors",
                table: "GraphicCards",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "MemorySizeGB",
                table: "GraphicCards",
                newName: "CoreClockspeed");

            migrationBuilder.RenameColumn(
                name: "LEDLighting",
                table: "GraphicCards",
                newName: "RayTraycing");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "GraphicCards",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GraphicCards",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "GraphicCards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "PackagingVersion",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
