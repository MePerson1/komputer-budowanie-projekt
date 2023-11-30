using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class newPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CpuCoolings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "CPU_CoolingId",
                table: "PcConfigurations",
                newName: "CpuCoolingId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_CPU_CoolingId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_CpuCoolingId");

            migrationBuilder.CreateTable(
                name: "ShopPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopName = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    CaseId = table.Column<int>(type: "integer", nullable: true),
                    CpuCoolingId = table.Column<int>(type: "integer", nullable: true),
                    CpuId = table.Column<int>(type: "integer", nullable: true),
                    FanId = table.Column<int>(type: "integer", nullable: true),
                    MotherboardId = table.Column<int>(type: "integer", nullable: true),
                    PowerSupplyId = table.Column<int>(type: "integer", nullable: true),
                    RamId = table.Column<int>(type: "integer", nullable: true),
                    StorageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPrice_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrice_CpuCoolings_CpuCoolingId",
                        column: x => x.CpuCoolingId,
                        principalTable: "CpuCoolings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrice_Cpus_CpuId",
                        column: x => x.CpuId,
                        principalTable: "Cpus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrice_Fans_FanId",
                        column: x => x.FanId,
                        principalTable: "Fans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrice_Motherboards_MotherboardId",
                        column: x => x.MotherboardId,
                        principalTable: "Motherboards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrice_PowerSupplies_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PowerSupplies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrice_Rams_RamId",
                        column: x => x.RamId,
                        principalTable: "Rams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopPrice_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_CaseId",
                table: "ShopPrice",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_CpuCoolingId",
                table: "ShopPrice",
                column: "CpuCoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_CpuId",
                table: "ShopPrice",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_FanId",
                table: "ShopPrice",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_MotherboardId",
                table: "ShopPrice",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_PowerSupplyId",
                table: "ShopPrice",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_RamId",
                table: "ShopPrice",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPrice_StorageId",
                table: "ShopPrice",
                column: "StorageId");

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

            migrationBuilder.DropTable(
                name: "ShopPrice");

            migrationBuilder.RenameColumn(
                name: "CpuCoolingId",
                table: "PcConfigurations",
                newName: "CPU_CoolingId");

            migrationBuilder.RenameIndex(
                name: "IX_PcConfigurations_CpuCoolingId",
                table: "PcConfigurations",
                newName: "IX_PcConfigurations_CPU_CoolingId");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Storages",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Rams",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "PowerSupplies",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Motherboards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Fans",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Cpus",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "CpuCoolings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Cases",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_PcConfigurations_CpuCoolings_CPU_CoolingId",
                table: "PcConfigurations",
                column: "CPU_CoolingId",
                principalTable: "CpuCoolings",
                principalColumn: "Id");
        }
    }
}
