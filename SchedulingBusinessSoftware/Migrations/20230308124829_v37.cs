using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingBusinessSoftware.Migrations
{
    /// <inheritdoc />
    public partial class v37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Interviewer");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Interviewer");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Interviewer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Interviewer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Interviewer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Interviewer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
