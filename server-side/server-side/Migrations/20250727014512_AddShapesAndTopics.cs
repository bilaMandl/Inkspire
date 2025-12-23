using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_side.Migrations
{
    /// <inheritdoc />
    public partial class AddShapesAndTopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shape",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Challenges");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShapeId",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Challenges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shape",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shape", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_CreatorId",
                table: "Challenges",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_ShapeId",
                table: "Challenges",
                column: "ShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_TopicId",
                table: "Challenges",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Shape_ShapeId",
                table: "Challenges",
                column: "ShapeId",
                principalTable: "Shape",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Topic_TopicId",
                table: "Challenges",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Users_CreatorId",
                table: "Challenges",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Shape_ShapeId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Topic_TopicId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Users_CreatorId",
                table: "Challenges");

            migrationBuilder.DropTable(
                name: "Shape");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Challenges_CreatorId",
                table: "Challenges");

            migrationBuilder.DropIndex(
                name: "IX_Challenges_ShapeId",
                table: "Challenges");

            migrationBuilder.DropIndex(
                name: "IX_Challenges_TopicId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "ShapeId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Challenges");

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "Challenges",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Challenges",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
