using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartZone.Entities.Migrations
{
    public partial class modifyentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressInfos_SmartZones_SmartZoneId",
                table: "AddressInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartZoneEmployees_AddressInfos_StroreId",
                table: "SmartZoneEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressInfos",
                table: "AddressInfos");

            migrationBuilder.RenameTable(
                name: "AddressInfos",
                newName: "Stores");

            migrationBuilder.RenameColumn(
                name: "StroreId",
                table: "SmartZoneEmployees",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_SmartZoneEmployees_StroreId",
                table: "SmartZoneEmployees",
                newName: "IX_SmartZoneEmployees_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressInfos_SmartZoneId",
                table: "Stores",
                newName: "IX_Stores_SmartZoneId");

            migrationBuilder.AddColumn<int>(
                name: "FiveStarRating",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FourStarRating",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Stores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notation",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OneStarRating",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreeStarRating",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TwoStarRating",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StoreId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<int>(type: "int", nullable: true),
                    PrepareTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_StoreId",
                table: "Foods",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_SmartZoneEmployees_Stores_StoreId",
                table: "SmartZoneEmployees",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_SmartZones_SmartZoneId",
                table: "Stores",
                column: "SmartZoneId",
                principalTable: "SmartZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmartZoneEmployees_Stores_StoreId",
                table: "SmartZoneEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_SmartZones_SmartZoneId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "FiveStarRating",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "FourStarRating",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Notation",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "OneStarRating",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ThreeStarRating",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "TwoStarRating",
                table: "Stores");

            migrationBuilder.RenameTable(
                name: "Stores",
                newName: "AddressInfos");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "SmartZoneEmployees",
                newName: "StroreId");

            migrationBuilder.RenameIndex(
                name: "IX_SmartZoneEmployees_StoreId",
                table: "SmartZoneEmployees",
                newName: "IX_SmartZoneEmployees_StroreId");

            migrationBuilder.RenameIndex(
                name: "IX_Stores_SmartZoneId",
                table: "AddressInfos",
                newName: "IX_AddressInfos_SmartZoneId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressInfos",
                table: "AddressInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressInfos_SmartZones_SmartZoneId",
                table: "AddressInfos",
                column: "SmartZoneId",
                principalTable: "SmartZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmartZoneEmployees_AddressInfos_StroreId",
                table: "SmartZoneEmployees",
                column: "StroreId",
                principalTable: "AddressInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
