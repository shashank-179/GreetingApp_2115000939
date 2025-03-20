using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloGreetingApplicationn.Migrations
{
    public partial class AddPrimaryKeyToUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserDetails");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "UserDetails",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserDetails",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserDetails",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserDetails",
                newName: "userId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
