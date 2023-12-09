using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomputerBudowanieAPI.Migrations
{
    /// <inheritdoc />
    public partial class QuantityManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FanPcConfiguration");

            migrationBuilder.DropTable(
                name: "Fans");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PcConfigurationStorage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PcConfigurationRam",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PcConfigurationStorage");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PcConfigurationRam");

            migrationBuilder.CreateTable(
                name: "Fans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Lenght = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Producer = table.Column<string>(type: "text", nullable: false),
                    ProducerCode = table.Column<string>(type: "text", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Voltatge = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FanPcConfiguration",
                columns: table => new
                {
                    FansId = table.Column<int>(type: "integer", nullable: false),
                    PcConfigurationsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FanPcConfiguration", x => new { x.FansId, x.PcConfigurationsId });
                    table.ForeignKey(
                        name: "FK_FanPcConfiguration_Fans_FansId",
                        column: x => x.FansId,
                        principalTable: "Fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FanPcConfiguration_PcConfigurations_PcConfigurationsId",
                        column: x => x.PcConfigurationsId,
                        principalTable: "PcConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FanPcConfiguration_PcConfigurationsId",
                table: "FanPcConfiguration",
                column: "PcConfigurationsId");
        }
    }
}
