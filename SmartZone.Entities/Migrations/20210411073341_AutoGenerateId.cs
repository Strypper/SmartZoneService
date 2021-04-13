using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartZone.Entities.Migrations
{
    public partial class AutoGenerateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartZoneUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdPrefix",
                table: "SmartZones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SmartZoneCustomers",
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
                    table.PrimaryKey("PK_SmartZoneCustomers", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "SmartZoneEmployees",
                columns: table => new
                {
                    Guid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StroreId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Policies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartZoneEmployees", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_SmartZoneEmployees_AddressInfos_StroreId",
                        column: x => x.StroreId,
                        principalTable: "AddressInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartZoneEmployees_StroreId",
                table: "SmartZoneEmployees",
                column: "StroreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartZoneCustomers");

            migrationBuilder.DropTable(
                name: "SmartZoneEmployees");

            migrationBuilder.DropColumn(
                name: "IdPrefix",
                table: "SmartZones");

            migrationBuilder.CreateTable(
                name: "SmartZoneUsers",
                columns: table => new
                {
                    Guid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Policies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartZoneUsers", x => x.Guid);
                });
        }
    }
}
