using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingBusinessSoftware.Migrations
{
    /// <inheritdoc />
    public partial class v38 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FistName",
                table: "Interviewer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FistName",
                table: "Interviewer");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Interviewer");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Interviewer");
        }
    }
}
