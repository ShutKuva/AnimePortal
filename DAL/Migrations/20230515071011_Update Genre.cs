using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_AnimeDescription_AnimeDescriptionId",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_AnimeDescriptionId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "AnimeDescriptionId",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AnimeDescriptionGenre",
                columns: table => new
                {
                    AnimeDescriptionsId = table.Column<int>(type: "integer", nullable: false),
                    GenresId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDescriptionGenre", x => new { x.AnimeDescriptionsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_AnimeDescriptionGenre_AnimeDescription_AnimeDescriptionsId",
                        column: x => x.AnimeDescriptionsId,
                        principalTable: "AnimeDescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDescriptionGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDescriptionGenre_GenresId",
                table: "AnimeDescriptionGenre",
                column: "GenresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeDescriptionGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.AddColumn<int>(
                name: "AnimeDescriptionId",
                table: "Genre",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_AnimeDescriptionId",
                table: "Genre",
                column: "AnimeDescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_AnimeDescription_AnimeDescriptionId",
                table: "Genre",
                column: "AnimeDescriptionId",
                principalTable: "AnimeDescription",
                principalColumn: "Id");
        }
    }
}
