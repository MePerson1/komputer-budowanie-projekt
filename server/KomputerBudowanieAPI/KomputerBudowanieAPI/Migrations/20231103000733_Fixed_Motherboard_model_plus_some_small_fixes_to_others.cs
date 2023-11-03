using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Motherboard_model_plus_some_small_fixes_to_others : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BluetoothSupport",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "EthernetPorts",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "M2Slots",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "MaxRam",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "PCIe_x16Slots",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "PCIe_x1Slots",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "PCIe_x4Slots",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "RamSlotsCount",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "RamType",
                table: "Motherboards");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Motherboards",
                newName: "MemorySlotsCount");

            migrationBuilder.RenameColumn(
                name: "WiFiSupport",
                table: "Motherboards",
                newName: "HasIntegratedGraphicsSupport");

            migrationBuilder.RenameColumn(
                name: "Socket",
                table: "Motherboards",
                newName: "SupportedProcessors");

            migrationBuilder.RenameColumn(
                name: "SATAPorts",
                table: "Motherboards",
                newName: "MaxMemoryGB");

            migrationBuilder.RenameColumn(
                name: "FormFactor",
                table: "Motherboards",
                newName: "SupportedMemoryFreq");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Motherboards",
                newName: "SoundChipset");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "PowerSupplys",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "ModularCabling",
                table: "PowerSupplys",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Motherboards",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "AudioChannels",
                table: "Motherboards",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "BoardStandard",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPUSocket",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardLinking",
                table: "Motherboards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChannelArchitecture",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "DepthMM",
                table: "Motherboards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "DriveConnectors",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpansionSlots",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GraphicsChipset",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IncludedAccessories",
                table: "Motherboards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IntegratedNetworkCard",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalConnectors",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemoryConnectorType",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemoryStandard",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NetworkChipset",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RAIDController",
                table: "Motherboards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RearPanelConnectors",
                table: "Motherboards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "WidthMM",
                table: "Motherboards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "WirelessSupport",
                table: "Motherboards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardLinking",
                table: "GraphicCards",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Cpus",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Cases",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardStandard",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "CPUSocket",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "CardLinking",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "ChannelArchitecture",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "DepthMM",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "DriveConnectors",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "ExpansionSlots",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "GraphicsChipset",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "IncludedAccessories",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "IntegratedNetworkCard",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "InternalConnectors",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "MemoryConnectorType",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "MemoryStandard",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "NetworkChipset",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "RAIDController",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "RearPanelConnectors",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "WidthMM",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "WirelessSupport",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "CardLinking",
                table: "GraphicCards");

            migrationBuilder.RenameColumn(
                name: "SupportedProcessors",
                table: "Motherboards",
                newName: "Socket");

            migrationBuilder.RenameColumn(
                name: "SupportedMemoryFreq",
                table: "Motherboards",
                newName: "FormFactor");

            migrationBuilder.RenameColumn(
                name: "SoundChipset",
                table: "Motherboards",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "MemorySlotsCount",
                table: "Motherboards",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "MaxMemoryGB",
                table: "Motherboards",
                newName: "SATAPorts");

            migrationBuilder.RenameColumn(
                name: "HasIntegratedGraphicsSupport",
                table: "Motherboards",
                newName: "WiFiSupport");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "PowerSupplys",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "ModularCabling",
                table: "PowerSupplys",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Motherboards",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "AudioChannels",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "BluetoothSupport",
                table: "Motherboards",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EthernetPorts",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "M2Slots",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxRam",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PCIe_x16Slots",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PCIe_x1Slots",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PCIe_x4Slots",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RamSlotsCount",
                table: "Motherboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string[]>(
                name: "RamType",
                table: "Motherboards",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Cpus",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Cases",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
