using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedMemoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadSpeed",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "WriteSpeed",
                table: "Memories");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Memories",
                newName: "ThiccnessMM");

            migrationBuilder.RenameColumn(
                name: "Format",
                table: "Memories",
                newName: "Model");

            migrationBuilder.AlterColumn<string>(
                name: "Capacity",
                table: "Memories",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "CacheMemory",
                table: "Memories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Controler",
                table: "Memories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormFactor",
                table: "Memories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HardwareEncryption",
                table: "Memories",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Memories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longevity",
                table: "Memories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemoryChipType",
                table: "Memories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NoiseLevelDB",
                table: "Memories",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Radiator",
                table: "Memories",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReadRandomIOPS",
                table: "Memories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReadSpeedMBs",
                table: "Memories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RotatingSpeedRPM",
                table: "Memories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TBW",
                table: "Memories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "WeightG",
                table: "Memories",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriteRandomIOPS",
                table: "Memories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriteSpeedMBs",
                table: "Memories",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CacheMemory",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Controler",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "FormFactor",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "HardwareEncryption",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Longevity",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "MemoryChipType",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "NoiseLevelDB",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "Radiator",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "ReadRandomIOPS",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "ReadSpeedMBs",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "RotatingSpeedRPM",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "TBW",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "WeightG",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "WriteRandomIOPS",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "WriteSpeedMBs",
                table: "Memories");

            migrationBuilder.RenameColumn(
                name: "ThiccnessMM",
                table: "Memories",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Memories",
                newName: "Format");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Memories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ReadSpeed",
                table: "Memories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WriteSpeed",
                table: "Memories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
