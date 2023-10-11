using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class EditedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_CPU_Coolings_CPU_CoolingId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_CPUs_CPUId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Cases_CaseId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Fans_FanId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_GraphicCards_GraphicCardId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Motherboards_MotherboardId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_PowerSupplys_PowerSupplyId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Users_UserId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationMemories_PC_Configurations_PcConfigurationId",
                table: "PcConfigurationMemories");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationRams_PC_Configurations_PcConfigurationId",
                table: "PcConfigurationRams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CPUs",
                table: "CPUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PC_Configurations",
                table: "PC_Configurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CPU_Coolings",
                table: "CPU_Coolings");

            migrationBuilder.RenameTable(
                name: "CPUs",
                newName: "Cpus");

            migrationBuilder.RenameTable(
                name: "PC_Configurations",
                newName: "PcConfigurations");

            migrationBuilder.RenameTable(
                name: "CPU_Coolings",
                newName: "CpuCoolings");

            migrationBuilder.RenameColumn(
                name: "CPUId",
                table: "PcConfigurations",
                newName: "CpuId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_UserId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_PowerSupplyId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_PowerSupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_MotherboardId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_MotherboardId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_GraphicCardId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_GraphicCardId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_FanId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_FanId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_CPUId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_CpuId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_CPU_CoolingId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_CPU_CoolingId");

            migrationBuilder.RenameIndex(
                name: "IX_PC_Configurations_CaseId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_CaseId");

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Fans",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Lenght",
                table: "Fans",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Fans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Voltatge",
                table: "Fans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Fans",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Lenght",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "Sockets",
                table: "CpuCoolings",
                type: "text",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cpus",
                table: "Cpus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PcConfigurations",
                table: "PcConfigurations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CpuCoolings",
                table: "CpuCoolings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurationMemories_PcConfigurations_PcConfigurationId",
                table: "PcConfigurationMemories",
                column: "PcConfigurationId",
                principalTable: "PcConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurationRams_PcConfigurations_PcConfigurationId",
                table: "PcConfigurationRams",
                column: "PcConfigurationId",
                principalTable: "PcConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationMemories_PcConfigurations_PcConfigurationId",
                table: "PcConfigurationMemories");

            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurationRams_PcConfigurations_PcConfigurationId",
                table: "PcConfigurationRams");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cpus",
                table: "Cpus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PcConfigurations",
                table: "PcConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CpuCoolings",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Lenght",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Voltatge",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Lenght",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Cases");

            migrationBuilder.RenameTable(
                name: "Cpus",
                newName: "CPUs");

            migrationBuilder.RenameTable(
                name: "PcConfigurations",
                newName: "PC_Configurations");

            migrationBuilder.RenameTable(
                name: "CpuCoolings",
                newName: "CPU_Coolings");

            migrationBuilder.RenameColumn(
                name: "CpuId",
                table: "PC_Configurations",
                newName: "CPUId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_UserId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_PowerSupplyId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_PowerSupplyId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_MotherboardId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_MotherboardId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_GraphicCardId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_GraphicCardId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_FanId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_FanId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_CpuId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_CPUId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_CPU_CoolingId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_CPU_CoolingId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_CaseId",
                table: "PC_Configurations",
                newName: "IX_PC_Configurations_CaseId");

            migrationBuilder.AlterColumn<string[]>(
                name: "Sockets",
                table: "CPU_Coolings",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CPUs",
                table: "CPUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PC_Configurations",
                table: "PC_Configurations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CPU_Coolings",
                table: "CPU_Coolings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_CPU_Coolings_CPU_CoolingId",
                table: "PC_Configurations",
                column: "CPU_CoolingId",
                principalTable: "CPU_Coolings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_CPUs_CPUId",
                table: "PC_Configurations",
                column: "CPUId",
                principalTable: "CPUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_Cases_CaseId",
                table: "PC_Configurations",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_Fans_FanId",
                table: "PC_Configurations",
                column: "FanId",
                principalTable: "Fans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_GraphicCards_GraphicCardId",
                table: "PC_Configurations",
                column: "GraphicCardId",
                principalTable: "GraphicCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_Motherboards_MotherboardId",
                table: "PC_Configurations",
                column: "MotherboardId",
                principalTable: "Motherboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_PowerSupplys_PowerSupplyId",
                table: "PC_Configurations",
                column: "PowerSupplyId",
                principalTable: "PowerSupplys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_Users_UserId",
                table: "PC_Configurations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurationMemories_PC_Configurations_PcConfigurationId",
                table: "PcConfigurationMemories",
                column: "PcConfigurationId",
                principalTable: "PC_Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurationRams_PC_Configurations_PcConfigurationId",
                table: "PcConfigurationRams",
                column: "PcConfigurationId",
                principalTable: "PC_Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
