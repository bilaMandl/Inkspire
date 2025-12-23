using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_side.Migrations
{
    /// <inheritdoc />
    public partial class improveChallengeTBL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Shape_ShapeId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Topic_TopicId",
                table: "Challenges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topic",
                table: "Topic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shape",
                table: "Shape");

            migrationBuilder.RenameTable(
                name: "Topic",
                newName: "Topics");

            migrationBuilder.RenameTable(
                name: "Shape",
                newName: "Shapes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shapes",
                table: "Shapes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Drawings_ChallengeId",
                table: "Drawings",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Drawings_UserId",
                table: "Drawings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Shapes_ShapeId",
                table: "Challenges",
                column: "ShapeId",
                principalTable: "Shapes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Topics_TopicId",
                table: "Challenges",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drawings_Challenges_ChallengeId",
                table: "Drawings",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drawings_Users_UserId",
                table: "Drawings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Shapes_ShapeId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Topics_TopicId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_Drawings_Challenges_ChallengeId",
                table: "Drawings");

            migrationBuilder.DropForeignKey(
                name: "FK_Drawings_Users_UserId",
                table: "Drawings");

            migrationBuilder.DropIndex(
                name: "IX_Drawings_ChallengeId",
                table: "Drawings");

            migrationBuilder.DropIndex(
                name: "IX_Drawings_UserId",
                table: "Drawings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shapes",
                table: "Shapes");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "Topic");

            migrationBuilder.RenameTable(
                name: "Shapes",
                newName: "Shape");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topic",
                table: "Topic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shape",
                table: "Shape",
                column: "Id");

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
        }
    }
}
