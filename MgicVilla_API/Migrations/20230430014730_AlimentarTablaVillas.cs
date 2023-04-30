using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MgicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de villa", new DateTime(2023, 4, 29, 20, 47, 29, 910, DateTimeKind.Local).AddTicks(3387), new DateTime(2023, 4, 29, 20, 47, 29, 910, DateTimeKind.Local).AddTicks(3376), "", 1000, "Villa Real", 5, 200.0 },
                    { 2, "", "Detalle de villa 2", new DateTime(2023, 4, 29, 20, 47, 29, 910, DateTimeKind.Local).AddTicks(3391), new DateTime(2023, 4, 29, 20, 47, 29, 910, DateTimeKind.Local).AddTicks(3391), "", 2000, "Villa Real 2", 4, 400.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
