using Microsoft.EntityFrameworkCore.Migrations;

namespace WriteModel.Infrastructure.Migrations
{
    public partial class IpAndUserAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IPv4",
                table: "AuditRequests",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "AuditRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_IPv4",
                table: "AuditRequests",
                column: "IPv4");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRequests_UserAgent",
                table: "AuditRequests",
                column: "UserAgent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuditRequests_IPv4",
                table: "AuditRequests");

            migrationBuilder.DropIndex(
                name: "IX_AuditRequests_UserAgent",
                table: "AuditRequests");

            migrationBuilder.DropColumn(
                name: "IPv4",
                table: "AuditRequests");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "AuditRequests");
        }
    }
}
