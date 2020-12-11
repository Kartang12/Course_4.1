using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Migrations
{
    public partial class soFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SMMUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SMMUserId",
                table: "AspNetUsers",
                column: "SMMUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SMMUserId",
                table: "AspNetUsers",
                column: "SMMUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_SMMUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SMMUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SMMUserId",
                table: "AspNetUsers");
        }
    }
}
