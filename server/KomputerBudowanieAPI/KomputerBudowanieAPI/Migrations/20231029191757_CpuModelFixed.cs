using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class CpuModelFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockFrequency",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "TurboClockFrequency",
                table: "Cpus");

            migrationBuilder.RenameColumn(
                name: "ThreadsCount",
                table: "Cpus",
                newName: "TDPinW");

            migrationBuilder.RenameColumn(
                name: "TDP",
                table: "Cpus",
                newName: "ProcessorBaseFrequencyGHz");

            migrationBuilder.RenameColumn(
                name: "OverclockingSupport",
                table: "Cpus",
                newName: "HasUnlockedMultiplier");

            migrationBuilder.RenameColumn(
                name: "MemoryType",
                table: "Cpus",
                newName: "SupportedMemoryTypes");

            migrationBuilder.RenameColumn(
                name: "MTP",
                table: "Cpus",
                newName: "NumberOfThreads");

            migrationBuilder.RenameColumn(
                name: "HyperThreading",
                table: "Cpus",
                newName: "HasIncludedCooling");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Cpus",
                newName: "ProcessorMicroarchitecture");

            migrationBuilder.RenameColumn(
                name: "CoreCount",
                table: "Cpus",
                newName: "NumberOfCores");

            migrationBuilder.RenameColumn(
                name: "CacheSize",
                table: "Cpus",
                newName: "PackagingVersion");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Cpus",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "IntegratedGraphics",
                table: "Cpus",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "Architecture",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "L1Cache",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "L2Cache",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "L3Cache",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Line",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManufacturingProcess",
                table: "Cpus",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxOperatingTempC",
                table: "Cpus",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxTurboFrequencyGHz",
                table: "Cpus",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Architecture",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "L1Cache",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "L2Cache",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "L3Cache",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "Line",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "ManufacturingProcess",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "MaxOperatingTempC",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "MaxTurboFrequencyGHz",
                table: "Cpus");

            migrationBuilder.RenameColumn(
                name: "TDPinW",
                table: "Cpus",
                newName: "ThreadsCount");

            migrationBuilder.RenameColumn(
                name: "SupportedMemoryTypes",
                table: "Cpus",
                newName: "MemoryType");

            migrationBuilder.RenameColumn(
                name: "ProcessorMicroarchitecture",
                table: "Cpus",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ProcessorBaseFrequencyGHz",
                table: "Cpus",
                newName: "TDP");

            migrationBuilder.RenameColumn(
                name: "PackagingVersion",
                table: "Cpus",
                newName: "CacheSize");

            migrationBuilder.RenameColumn(
                name: "NumberOfThreads",
                table: "Cpus",
                newName: "MTP");

            migrationBuilder.RenameColumn(
                name: "NumberOfCores",
                table: "Cpus",
                newName: "CoreCount");

            migrationBuilder.RenameColumn(
                name: "HasUnlockedMultiplier",
                table: "Cpus",
                newName: "OverclockingSupport");

            migrationBuilder.RenameColumn(
                name: "HasIncludedCooling",
                table: "Cpus",
                newName: "HyperThreading");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Cpus",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<bool>(
                name: "IntegratedGraphics",
                table: "Cpus",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<double>(
                name: "ClockFrequency",
                table: "Cpus",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TurboClockFrequency",
                table: "Cpus",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
