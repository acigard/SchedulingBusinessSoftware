using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingBusinessSoftware.Migrations
{
    /// <inheritdoc />
    public partial class v33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Interviewer_Id",
                table: "Appointment");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Interviewer_TempId",
                table: "Interviewer");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Interviewer");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Interviewer_Id",
                table: "Appointment",
                column: "Id",
                principalTable: "Interviewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Interviewer_Id",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "TempId",
                table: "Interviewer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Interviewer_TempId",
                table: "Interviewer",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Interviewer_Id",
                table: "Appointment",
                column: "Id",
                principalTable: "Interviewer",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
