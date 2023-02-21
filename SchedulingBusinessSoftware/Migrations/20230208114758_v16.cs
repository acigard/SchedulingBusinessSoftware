using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulingBusinessSoftware.Migrations
{
    /// <inheritdoc />
    public partial class v16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "CreatedDate", "Description", "ScheduledAt", "Title", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description Appointment2", new DateTime(2023, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Appointment2", new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
