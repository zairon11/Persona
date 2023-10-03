using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "Id", "Description", "Name", "Registrationdate", "State" },
                values: new object[,]
                {
                    { 1, "DESD", "Natural", new DateTime(2023, 10, 3, 9, 9, 23, 29, DateTimeKind.Local).AddTicks(3563), 1 },
                    { 2, "DEX", "Juridica", new DateTime(2023, 10, 3, 9, 9, 23, 29, DateTimeKind.Local).AddTicks(3576), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
