using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class addbookdetailstotable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookDetail_id",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookDetail",
                columns: table => new
                {
                    BookDetail_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfChapters = table.Column<int>(nullable: false),
                    NumberOfPages = table.Column<int>(nullable: false),
                    Weigth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetail", x => x.BookDetail_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookDetail_id",
                table: "Books",
                column: "BookDetail_id",
                unique: true,
                filter: "[BookDetail_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetail_BookDetail_id",
                table: "Books",
                column: "BookDetail_id",
                principalTable: "BookDetail",
                principalColumn: "BookDetail_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetail_BookDetail_id",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookDetail");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookDetail_id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookDetail_id",
                table: "Books");
        }
    }
}
