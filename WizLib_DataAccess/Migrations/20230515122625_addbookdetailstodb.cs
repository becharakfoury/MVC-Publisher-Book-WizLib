﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class addbookdetailstodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetail_BookDetail_id",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookDetail",
                table: "BookDetail");

            migrationBuilder.RenameTable(
                name: "BookDetail",
                newName: "BookDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookDetails",
                table: "BookDetails",
                column: "BookDetail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetails_BookDetail_id",
                table: "Books",
                column: "BookDetail_id",
                principalTable: "BookDetails",
                principalColumn: "BookDetail_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetails_BookDetail_id",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookDetails",
                table: "BookDetails");

            migrationBuilder.RenameTable(
                name: "BookDetails",
                newName: "BookDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookDetail",
                table: "BookDetail",
                column: "BookDetail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetail_BookDetail_id",
                table: "Books",
                column: "BookDetail_id",
                principalTable: "BookDetail",
                principalColumn: "BookDetail_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
