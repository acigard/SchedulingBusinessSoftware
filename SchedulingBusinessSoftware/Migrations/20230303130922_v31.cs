using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingBusinessSoftware.Migrations
{
    /// <inheritdoc />
    public partial class v31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Account_Candidate",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_Candidate",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "Candidate",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Candidate",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Candidate",
                table: "Appointment",
                column: "Candidate");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Account_Candidate",
                table: "Appointment",
                column: "Candidate",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
