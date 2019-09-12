using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentApp.Data.EntityFramework.Migrations
{
    public partial class withoutCascadingDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Shipments_ShipmentId",
                table: "ActivityLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Shipments_ShipmentId",
                table: "ActivityLogs",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Shipments_ShipmentId",
                table: "ActivityLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Shipments_ShipmentId",
                table: "ActivityLogs",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
