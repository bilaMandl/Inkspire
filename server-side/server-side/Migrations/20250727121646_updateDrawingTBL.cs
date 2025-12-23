using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_side.Migrations
{
    /// <inheritdoc />
    public partial class updateDrawingTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Challenges");

            migrationBuilder.RenameColumn(
                name: "DrawingData",
                table: "Drawings",
                newName: "DrawingName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrawingName",
                table: "Drawings",
                newName: "DrawingData");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Challenges",
                type: "int",
                nullable: true);
        }
    }
}
