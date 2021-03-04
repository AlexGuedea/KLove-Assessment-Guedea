using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KLove.Migrations
{
    public partial class KLoveInitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetFavoriteVerses",
                columns: table => new
                {
                    VerseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VerseText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerseNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chapter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Book = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BibleReferenceLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookShareUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterShareUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinterestShareUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetFavoriteVerses", x => x.VerseId);
                });

            migrationBuilder.CreateTable(
                name: "VersesData",
                columns: table => new
                {
                    VerseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfVerses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetFavoriteVerses");

            migrationBuilder.DropTable(
                name: "VersesData");
        }
    }
}
