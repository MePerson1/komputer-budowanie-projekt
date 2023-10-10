using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CPUId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPU_CoolingId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CaseId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PC_Configurations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FanId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GraphicCardId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemoryId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MotherboardId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PC_Configurations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PowerSupplyId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RamId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PC_Configurations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_CaseId",
                table: "PC_Configurations",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_CPU_CoolingId",
                table: "PC_Configurations",
                column: "CPU_CoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_CPUId",
                table: "PC_Configurations",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_FanId",
                table: "PC_Configurations",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_GraphicCardId",
                table: "PC_Configurations",
                column: "GraphicCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_MemoryId",
                table: "PC_Configurations",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_MotherboardId",
                table: "PC_Configurations",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_PowerSupplyId",
                table: "PC_Configurations",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_RamId",
                table: "PC_Configurations",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_PC_Configurations_UserId",
                table: "PC_Configurations",
                column: "UserId");

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
                name: "FK_PC_Configurations_Memories_MemoryId",
                table: "PC_Configurations",
                column: "MemoryId",
                principalTable: "Memories",
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
                name: "FK_PC_Configurations_Rams_RamId",
                table: "PC_Configurations",
                column: "RamId",
                principalTable: "Rams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PC_Configurations_Users_UserId",
                table: "PC_Configurations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_PC_Configurations_Memories_MemoryId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Motherboards_MotherboardId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_PowerSupplys_PowerSupplyId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Rams_RamId",
                table: "PC_Configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_PC_Configurations_Users_UserId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_CaseId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_CPU_CoolingId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_CPUId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_FanId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_GraphicCardId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_MemoryId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_MotherboardId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_PowerSupplyId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_RamId",
                table: "PC_Configurations");

            migrationBuilder.DropIndex(
                name: "IX_PC_Configurations_UserId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CPUId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "CPU_CoolingId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "CaseId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "FanId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "GraphicCardId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "MemoryId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "MotherboardId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "PowerSupplyId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "RamId",
                table: "PC_Configurations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PC_Configurations");
        }
    }
}
