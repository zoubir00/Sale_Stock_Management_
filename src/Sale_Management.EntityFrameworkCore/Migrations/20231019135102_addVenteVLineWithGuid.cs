using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleManagement.Migrations
{
    /// <inheritdoc />
    public partial class addVenteVLineWithGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ventes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateVente = table.Column<DateTime>(type: "datetime2", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    QtyTotal = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Ventelines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenteCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    articleId = table.Column<int>(type: "int", nullable: false),
                    QtySold = table.Column<int>(type: "int", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventelines_Articles_articleId",
                        column: x => x.articleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventelines_Ventes_VenteCode",
                        column: x => x.VenteCode,
                        principalTable: "Ventes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventelines_articleId",
                table: "Ventelines",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventelines_VenteCode",
                table: "Ventelines",
                column: "VenteCode");

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_clientId",
                table: "Ventes",
                column: "clientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ventelines");

            migrationBuilder.DropTable(
                name: "Ventes");
        }
    }
}
