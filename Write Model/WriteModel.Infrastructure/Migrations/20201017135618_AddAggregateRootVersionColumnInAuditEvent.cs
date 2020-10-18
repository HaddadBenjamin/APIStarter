using Microsoft.EntityFrameworkCore.Migrations;

namespace WriteModel.Infrastructure.Migrations
{
    public partial class AddAggregateRootVersionColumnInAuditEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AggregateRootVersion",
                table: "AuditEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AuditEvents_AggregateRootVersion",
                table: "AuditEvents",
                column: "AggregateRootVersion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuditEvents_AggregateRootVersion",
                table: "AuditEvents");

            migrationBuilder.DropColumn(
                name: "AggregateRootVersion",
                table: "AuditEvents");
        }
    }
}
