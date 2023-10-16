using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class addidtomanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Users_UserId",
                table: "PcConfigurations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PowerSupplyId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MotherboardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GraphicCardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "FanId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CpuId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CPU_CoolingId",
                table: "PcConfigurations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PcConfigurationRams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PcConfigurationMemories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations",
                column: "CPU_CoolingId",
                principalTable: "CpuCoolings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations",
                column: "FanId",
                principalTable: "Fans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations",
                column: "GraphicCardId",
                principalTable: "GraphicCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations",
                column: "MotherboardId",
                principalTable: "Motherboards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations",
                column: "PowerSupplyId",
                principalTable: "PowerSupplys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Users_UserId",
                table: "PcConfigurations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Users_UserId",
                table: "PcConfigurations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PcConfigurationRams");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PcConfigurationMemories");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PowerSupplyId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MotherboardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GraphicCardId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FanId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CpuId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CPU_CoolingId",
                table: "PcConfigurations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cases_CaseId",
                table: "PcConfigurations",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations",
                column: "CPU_CoolingId",
                principalTable: "CpuCoolings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Cpus_CpuId",
                table: "PcConfigurations",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations",
                column: "FanId",
                principalTable: "Fans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_GraphicCards_GraphicCardId",
                table: "PcConfigurations",
                column: "GraphicCardId",
                principalTable: "GraphicCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Motherboards_MotherboardId",
                table: "PcConfigurations",
                column: "MotherboardId",
                principalTable: "Motherboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_PowerSupplys_PowerSupplyId",
                table: "PcConfigurations",
                column: "PowerSupplyId",
                principalTable: "PowerSupplys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Users_UserId",
                table: "PcConfigurations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
