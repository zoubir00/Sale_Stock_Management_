using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleManagement.Migrations
{
    /// <inheritdoc />
    public partial class venteandVenteline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ventelines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ventelines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    articleId = table.Column<int>(type: "int", nullable: false),
                    QtySold = table.Column<int>(type: "int", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    VenteCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventelines_articleId",
                table: "Ventelines",
                column: "articleId");
        }
    }
}
