using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateFansTableToWaterCooling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fans",
                table: "Fans");

            migrationBuilder.RenameTable(
                name: "Fans",
                newName: "WaterCoolings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WaterCoolings",
                table: "WaterCoolings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_WaterCoolings_FanId",
                table: "PcConfigurations",
                column: "FanId",
                principalTable: "WaterCoolings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_WaterCoolings_FanId",
                table: "PcConfigurations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WaterCoolings",
                table: "WaterCoolings");

            migrationBuilder.RenameTable(
                name: "WaterCoolings",
                newName: "Fans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fans",
                table: "Fans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_Fans_FanId",
                table: "PcConfigurations",
                column: "FanId",
                principalTable: "Fans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
