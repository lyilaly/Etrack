using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Etrack.Infrastructure.Data.Migrations
{
    public partial class Update_Location_Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Locations",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                maxLength: 50,
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationName",
                table: "Locations",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Location_LocationName",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Locations",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                maxLength: 25,
                nullable: false);
        }
    }
}
