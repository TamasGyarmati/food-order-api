using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrder.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderModelCreatorUserName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserName",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
