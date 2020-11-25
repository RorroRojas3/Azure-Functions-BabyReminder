using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rodrigo.Tech.Repository.Rodrigo.Tech.Repository.Migrations
{
    public partial class LanguagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Email");

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "Email",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailBody",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Html = table.Column<string>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailBody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailBody_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("84e1db2e-3ccf-49cb-a660-e46a5e8dfc0e"), "English" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("3bfa7ae0-934e-4c2e-b9b0-7429b04470a0"), "Spanish" });

            migrationBuilder.CreateIndex(
                name: "IX_Email_LanguageId",
                table: "Email",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailBody_LanguageId",
                table: "EmailBody",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Languages_LanguageId",
                table: "Email",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_Languages_LanguageId",
                table: "Email");

            migrationBuilder.DropTable(
                name: "EmailBody");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Email_LanguageId",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Email",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
