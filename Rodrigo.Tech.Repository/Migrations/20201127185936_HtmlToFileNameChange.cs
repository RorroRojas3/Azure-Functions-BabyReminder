using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rodrigo.Tech.Repository.Migrations
{
    public partial class HtmlToFileNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Html",
                table: "EmailBody");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "EmailBody",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "EmailBody");

            migrationBuilder.AddColumn<byte[]>(
                name: "Html",
                table: "EmailBody",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
