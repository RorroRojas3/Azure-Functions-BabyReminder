using Microsoft.EntityFrameworkCore.Migrations;

namespace Rodrigo.Tech.Repository.Rodrigo.Tech.Repository.Migrations
{
    public partial class EmailBodyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Email");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Email",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Email",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Email",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Email",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
