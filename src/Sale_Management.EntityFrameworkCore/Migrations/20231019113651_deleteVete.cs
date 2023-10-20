using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleManagement.Migrations
{
    /// <inheritdoc />
    public partial class deleteVete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventelines_Ventes_VenteCode",
                table: "Ventelines");

            migrationBuilder.DropTable(
                name: "Ventes");

            migrationBuilder.DropIndex(
                name: "IX_Ventelines_VenteCode",
                table: "Ventelines");

            migrationBuilder.AlterColumn<string>(
                name: "VenteCode",
                table: "Ventelines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VenteCode",
                table: "Ventelines",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Ventes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    DateVente = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtyTotal = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventes_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventelines_VenteCode",
                table: "Ventelines",
                column: "VenteCode");

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_clientId",
                table: "Ventes",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventelines_Ventes_VenteCode",
                table: "Ventelines",
                column: "VenteCode",
                principalTable: "Ventes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
