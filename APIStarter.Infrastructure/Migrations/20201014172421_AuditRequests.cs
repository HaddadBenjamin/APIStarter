using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIStarter.Infrastructure.Migrations
{
    public partial class AuditRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Method = table.Column<string>(maxLength: 10, nullable: true),
                    Uri = table.Column<string>(maxLength: 200, nullable: true),
                    Headers = table.Column<string>(nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    CorrelationId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ImpersonatedUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_CorrelationId",
                table: "AuditRequests",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_Date",
                table: "AuditRequests",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_Duration",
                table: "AuditRequests",
                column: "Duration");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_Headers",
                table: "AuditRequests",
                column: "Headers");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_Id",
                table: "AuditRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_ImpersonatedUserId",
                table: "AuditRequests",
                column: "ImpersonatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_Method",
                table: "AuditRequests",
                column: "Method");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_Uri",
                table: "AuditRequests",
                column: "Uri");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_UserId",
                table: "AuditRequests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditRequests");
        }
    }
}
