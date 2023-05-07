using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StocksAssistance.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialStockAssistanceCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Sector = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Last52WeekHigh = table.Column<double>(type: "float", nullable: false),
                    Last52WeekLow = table.Column<double>(type: "float", nullable: false),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    LTM_PE = table.Column<double>(type: "float", nullable: false),
                    NTM_PE = table.Column<double>(type: "float", nullable: false),
                    PriceBookRatio = table.Column<double>(type: "float", nullable: false),
                    DividendYield = table.Column<double>(type: "float", nullable: false),
                    ROE = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAttributes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyLogs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesTags",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesTags", x => new { x.CompaniesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CompaniesTags_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompaniesTags_CompanyTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "CompanyTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesTags_TagsId",
                table: "CompaniesTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAttributes_CompanyId",
                table: "CompanyAttributes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLogs_CompanyId",
                table: "CompanyLogs",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompaniesTags");

            migrationBuilder.DropTable(
                name: "CompanyAttributes");

            migrationBuilder.DropTable(
                name: "CompanyLogs");

            migrationBuilder.DropTable(
                name: "CompanyTags");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
