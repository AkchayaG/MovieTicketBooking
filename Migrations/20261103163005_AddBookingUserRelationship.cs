using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Screens_ScreenId1",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Movies_MovieId1",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Screens_ScreenId1",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_MovieId1",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_ScreenId1",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Seats_ScreenId1",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "ScreenId1",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "ScreenId1",
                table: "Seats");

            migrationBuilder.AlterColumn<decimal>(
                name: "TicketPrice",
                table: "Shows",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Shows",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.AlterColumn<decimal>(
                name: "TicketPrice",
                table: "Shows",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Shows",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "Shows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScreenId1",
                table: "Shows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScreenId1",
                table: "Seats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MovieId1",
                table: "Shows",
                column: "MovieId1");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_ScreenId1",
                table: "Shows",
                column: "ScreenId1");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ScreenId1",
                table: "Seats",
                column: "ScreenId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Screens_ScreenId1",
                table: "Seats",
                column: "ScreenId1",
                principalTable: "Screens",
                principalColumn: "ScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Movies_MovieId1",
                table: "Shows",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Screens_ScreenId1",
                table: "Shows",
                column: "ScreenId1",
                principalTable: "Screens",
                principalColumn: "ScreenId");
        }
    }
}
