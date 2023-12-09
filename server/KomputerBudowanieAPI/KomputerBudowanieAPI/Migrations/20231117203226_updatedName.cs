using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations");

            migrationBuilder.RenameColumn(
                name: "CPU_CoolingId",
                table: "PcConfigurations",
                newName: "CpuCoolingId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_CPU_CoolingId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_CpuCoolingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CpuCoolingId",
                table: "PcConfigurations",
                column: "CpuCoolingId",
                principalTable: "CpuCoolings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CpuCoolingId",
                table: "PcConfigurations");

            migrationBuilder.RenameColumn(
                name: "CpuCoolingId",
                table: "PcConfigurations",
                newName: "CPU_CoolingId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_CpuCoolingId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_CPU_CoolingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations",
                column: "CPU_CoolingId",
                principalTable: "CpuCoolings",
                principalColumn: "Id");
        }
    }
}
