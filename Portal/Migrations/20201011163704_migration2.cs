using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CalendarInfos",
                table: "CalendarInfos");

            migrationBuilder.DropColumn(
                name: "Idv",
                table: "CalendarInfos");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "CalendarInfos",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalendarInfos",
                table: "CalendarInfos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CalendarInfos",
                table: "CalendarInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CalendarInfos");

            migrationBuilder.AddColumn<long>(
                name: "Idv",
                table: "CalendarInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalendarInfos",
                table: "CalendarInfos",
                column: "Idv");
        }
    }
}
