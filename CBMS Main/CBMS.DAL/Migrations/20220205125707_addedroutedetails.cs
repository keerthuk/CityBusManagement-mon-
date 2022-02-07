using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBMS.DAL.Migrations
{
    public partial class addedroutedetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "routedetails",
                columns: table => new
                {
                    routeNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destination = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routedetails", x => x.routeNo);
                });

            migrationBuilder.CreateTable(
                name: "busdetails",
                columns: table => new
                {
                    busNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    busName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arrival = table.Column<TimeSpan>(type: "time", nullable: false),
                    routeDetailsrouteNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_busdetails", x => x.busNo);
                    table.ForeignKey(
                        name: "FK_busdetails_routedetails_routeDetailsrouteNo",
                        column: x => x.routeDetailsrouteNo,
                        principalTable: "routedetails",
                        principalColumn: "routeNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_busdetails_routeDetailsrouteNo",
                table: "busdetails",
                column: "routeDetailsrouteNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "busdetails");

            migrationBuilder.DropTable(
                name: "routedetails");
        }
    }
}
