using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class buildonetomanyrelationshipbookandpublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Publisher_id",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Publisher_id",
                table: "Books",
                column: "Publisher_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_Publisher_id",
                table: "Books",
                column: "Publisher_id",
                principalTable: "Publishers",
                principalColumn: "Publisher_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_Publisher_id",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Publisher_id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Publisher_id",
                table: "Books");
        }
    }
}
