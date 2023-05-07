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
                name: "CompanyAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesToAttributes",
                columns: table => new
                {
                    AttributesId = table.Column<int>(type: "int", nullable: false),
                    CompaniesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesToAttributes", x => new { x.AttributesId, x.CompaniesId });
                    table.ForeignKey(
                        name: "FK_CompaniesToAttributes_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompaniesToAttributes_CompanyAttributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "CompanyAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesToTags",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    LogsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesToTags", x => new { x.CompaniesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CompaniesToTags_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompaniesToTags_CompanyLogs_LogsId",
                        column: x => x.LogsId,
                        principalTable: "CompanyLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompaniesToTags_CompanyTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "CompanyTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesToAttributes_CompaniesId",
                table: "CompaniesToAttributes",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesToTags_LogsId",
                table: "CompaniesToTags",
                column: "LogsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesToTags_TagsId",
                table: "CompaniesToTags",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompaniesToAttributes");

            migrationBuilder.DropTable(
                name: "CompaniesToTags");

            migrationBuilder.DropTable(
                name: "CompanyAttributes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyLogs");

            migrationBuilder.DropTable(
                name: "CompanyTags");
        }
    }
}
