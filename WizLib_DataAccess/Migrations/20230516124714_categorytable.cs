using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class categorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert tb_Category values ('cat 1')");
            migrationBuilder.Sql("insert tb_Category values ('cat 2')");
            migrationBuilder.Sql("insert tb_Category values ('cat 2')");

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
