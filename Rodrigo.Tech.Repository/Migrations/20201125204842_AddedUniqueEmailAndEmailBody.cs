using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rodrigo.Tech.Repository.Migrations
{
    public partial class AddedUniqueEmailAndEmailBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailBody_LanguageId",
                table: "EmailBody");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("5d5b0fe5-c777-45f8-b6ce-dfeac209db1b"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("90271cfb-9d12-46c0-97fb-3f9ca0f65b6c"));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Email",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a190e05a-c483-41f2-bc90-30e0c5dd5463"), "English" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("12fb15ca-0e72-4701-9ae8-be4de2f4c4ca"), "Spanish" });

            migrationBuilder.CreateIndex(
                name: "IX_EmailBody_LanguageId",
                table: "EmailBody",
                column: "LanguageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email_EmailAddress",
                table: "Email",
                column: "EmailAddress",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailBody_LanguageId",
                table: "EmailBody");

            migrationBuilder.DropIndex(
                name: "IX_Email_EmailAddress",
                table: "Email");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("12fb15ca-0e72-4701-9ae8-be4de2f4c4ca"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("a190e05a-c483-41f2-bc90-30e0c5dd5463"));

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Email",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("5d5b0fe5-c777-45f8-b6ce-dfeac209db1b"), "English" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("90271cfb-9d12-46c0-97fb-3f9ca0f65b6c"), "Spanish" });

            migrationBuilder.CreateIndex(
                name: "IX_EmailBody_LanguageId",
                table: "EmailBody",
                column: "LanguageId");
        }
    }
}
