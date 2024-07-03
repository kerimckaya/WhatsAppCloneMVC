using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsAppCloneMVC.Migrations
{
    /// <inheritdoc />
    public partial class addedConnectionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "Users");
        }
    }
}
