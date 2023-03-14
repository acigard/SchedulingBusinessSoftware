using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingBusinessSoftware.Migrations
{
    /// <inheritdoc />
    public partial class v23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Candidate",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Candidate",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Appointment");
        }
    }
}
