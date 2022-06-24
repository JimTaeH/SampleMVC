using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Data.Migrations
{
    public partial class foodcalories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FoodCal",
                table: "Food",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodCal",
                table: "Food");
        }
    }
}
