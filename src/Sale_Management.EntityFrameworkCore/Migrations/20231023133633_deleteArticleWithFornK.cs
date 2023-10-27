using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleManagement.Migrations
{
    /// <inheritdoc />
    public partial class deleteArticleWithFornK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventelines_Articles_articleId",
                table: "Ventelines");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Ventelines_articleId",
                table: "Ventelines");

            migrationBuilder.DropColumn(
                name: "articleId",
                table: "Ventelines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "articleId",
                table: "Ventelines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    QuantityinStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventelines_articleId",
                table: "Ventelines",
                column: "articleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventelines_Articles_articleId",
                table: "Ventelines",
                column: "articleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
