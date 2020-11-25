using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rodrigo.Tech.Repository.Migrations
{
    public partial class EmailAndBodyAndLanguageTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Email_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[] { new Guid("5d5b0fe5-c777-45f8-b6ce-dfeac209db1b"), "English" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("90271cfb-9d12-46c0-97fb-3f9ca0f65b6c"), "Spanish" });

            migrationBuilder.CreateIndex(
                name: "IX_Email_LanguageId",
                table: "Email",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailBody_LanguageId",
                table: "EmailBody",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "EmailBody");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
