using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentApp.Data.EntityFramework.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostCode = table.Column<string>(maxLength: 128, nullable: true),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: true),
                    State = table.Column<string>(maxLength: 128, nullable: true),
                    City = table.Column<string>(maxLength: 128, nullable: true),
                    Line1 = table.Column<string>(maxLength: 128, nullable: true),
                    Line2 = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    SenderName = table.Column<string>(maxLength: 256, nullable: true),
                    RecipientName = table.Column<string>(maxLength: 256, nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    SenderAddressId = table.Column<Guid>(nullable: false),
                    RecipientAddressId = table.Column<Guid>(nullable: false),
                    CarrierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Addresses_RecipientAddressId",
                        column: x => x.RecipientAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Addresses_SenderAddressId",
                        column: x => x.SenderAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    CarrierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carriers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Carrier1" },
                    { 2, "Carrier2" },
                    { 3, "Carrier3" },
                    { 4, "Carrier4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_CarrierId",
                table: "ActivityLogs",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ShipmentId",
                table: "ActivityLogs",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CarrierId",
                table: "Shipments",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_RecipientAddressId",
                table: "Shipments",
                column: "RecipientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SenderAddressId",
                table: "Shipments",
                column: "SenderAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Carriers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
