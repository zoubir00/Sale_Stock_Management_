using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleManagement.Migrations
{
    /// <inheritdoc />
    public partial class addIsValidProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Ventes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Ventes");
        }
    }
}
