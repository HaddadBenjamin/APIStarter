using Microsoft.EntityFrameworkCore.Migrations;

namespace WriteModel.Infrastructure.Migrations
{
    public partial class geopoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "AuditRequests",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "AuditRequests",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AuditRequests");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AuditRequests");
        }
    }
}
