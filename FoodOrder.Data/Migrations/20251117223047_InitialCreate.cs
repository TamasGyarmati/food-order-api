using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodOrder.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CaloriePer100Gramms = table.Column<double>(type: "float", nullable: false),
                    Gramms = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { "1e7c8b8e-4a9d-4bc4-9f18-1cc6f30edc11", "Pizza", 10.0 },
                    { "3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e", "Hamburger", 12.0 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CaloriePer100Gramms", "FoodId", "Gramms", "Name" },
                values: new object[,]
                {
                    { "0ffb5dc5-6a51-4ed7-94a5-41b15b5e0e8b", 280.0, "3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e", 80, "Bun" },
                    { "8b32f9a0-59ae-417a-9c28-f6fa1f2e55ca", 20.0, "1e7c8b8e-4a9d-4bc4-9f18-1cc6f30edc11", 30, "Tomato" },
                    { "b5a1c94d-19d4-4e16-a60b-d093c8bcd451", 350.0, "1e7c8b8e-4a9d-4bc4-9f18-1cc6f30edc11", 50, "Cheese" },
                    { "c27479a1-07ad-4a63-8c4b-0b7035e8dcc8", 250.0, "1e7c8b8e-4a9d-4bc4-9f18-1cc6f30edc11", 100, "Dough" },
                    { "e2f1a3cd-11ea-4c92-8461-6053b9a9f9c2", 250.0, "3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e", 120, "Beef Patty" },
                    { "f9482b0d-3ebe-434e-8bd0-44c446a7a5b0", 330.0, "3d47c7a1-8f0f-4cb1-9b92-f5235eb3f83e", 40, "Cheese Slice" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_FoodId",
                table: "Ingredients",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FoodId",
                table: "Orders",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Foods");
        }
    }
}
