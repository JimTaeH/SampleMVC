using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Data.Migrations
{
    public partial class FoodImageLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodImg",
                table: "Food",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodImg",
                table: "Food");
        }
    }
}
