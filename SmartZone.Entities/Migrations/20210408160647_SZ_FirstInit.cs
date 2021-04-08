using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartZone.Entities.Migrations
{
    public partial class SZ_FirstInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartZones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartZoneUsers",
                columns: table => new
                {
                    Guid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Policies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartZoneUsers", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "AddressInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wifis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CloseTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SmartZoneId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressInfos_SmartZones_SmartZoneId",
                        column: x => x.SmartZoneId,
                        principalTable: "SmartZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressInfos_SmartZoneId",
                table: "AddressInfos",
                column: "SmartZoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressInfos");

            migrationBuilder.DropTable(
                name: "SmartZoneUsers");

            migrationBuilder.DropTable(
                name: "SmartZones");
        }
    }
}
