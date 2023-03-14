using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingBusinessSoftware.Migrations
{
    /// <inheritdoc />
    public partial class v34 : Migration
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

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Interviewer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FistName",
                table: "Interviewer",
                newName: "Email");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Appointment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)");

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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Interviewer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Interviewer",
                newName: "FistName");

            migrationBuilder.AddColumn<string>(
                name: "TempId",
                table: "Interviewer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Appointment",
                type: "nvarchar(64)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
