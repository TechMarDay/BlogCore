using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Migrations
{
    public partial class AddCategoryType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "CategoryModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CategoryTypeDisplay",
                table: "CategoryModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "Categories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "CategoryModel");

            migrationBuilder.DropColumn(
                name: "CategoryTypeDisplay",
                table: "CategoryModel");

            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "Categories");
        }
    }
}
