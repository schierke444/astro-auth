using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Password", "UpdatedAt", "Username" },
                values: new object[] { new Guid("eb0de443-14af-4973-9616-7dd6873fa5d5"), new DateTime(2023, 5, 10, 11, 29, 55, 608, DateTimeKind.Utc).AddTicks(6313), "24C0A769D8746A0A0D5B1F06EDC6A3BE6EFF378004694DC86EB3E4DA7DCF0750C9EA2F5D0BAF2C4C9F350F202A456595707AD4733BC86EC8FAE933221566BBB6", new DateTime(2023, 5, 10, 11, 29, 55, 608, DateTimeKind.Utc).AddTicks(6314), "admin" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("782d9fda-8eea-43d9-b2f7-428e3740defe"), new DateTime(2023, 5, 10, 11, 29, 55, 608, DateTimeKind.Utc).AddTicks(6954), "Item2", new DateTime(2023, 5, 10, 11, 29, 55, 608, DateTimeKind.Utc).AddTicks(6955), new Guid("eb0de443-14af-4973-9616-7dd6873fa5d5") },
                    { new Guid("c4c16563-108b-484f-9a70-30a322395f27"), new DateTime(2023, 5, 10, 11, 29, 55, 608, DateTimeKind.Utc).AddTicks(6933), "Item1", new DateTime(2023, 5, 10, 11, 29, 55, 608, DateTimeKind.Utc).AddTicks(6934), new Guid("eb0de443-14af-4973-9616-7dd6873fa5d5") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
