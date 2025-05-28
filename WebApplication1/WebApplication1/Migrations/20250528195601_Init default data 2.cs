using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Initdefaultdata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1,
                column: "Description",
                value: "Lek 1 opis");

            migrationBuilder.InsertData(
                table: "Perscription",
                columns: new[] { "IdPersciption", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 1, new DateTime(2025, 5, 28, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 28, 14, 30, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[] { 1, 1, "Details ", 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Perscription",
                keyColumn: "IdPersciption",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1,
                column: "Description",
                value: "Lek 1 tralalala");
        }
    }
}
