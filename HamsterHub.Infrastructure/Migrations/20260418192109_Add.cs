using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HamsterHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hamsters",
                columns: new[] { "Id", "AgeInMonths", "Description", "Img", "IsAvailable", "Name", "Personality", "PricePerDay", "WeightInGrams" },
                values: new object[,]
                {
                    { 1, 8.0, "Världens mjukaste hamster.", "", true, "Mysiga Mårten", "Kelig", 99m, 180 },
                    { 2, 14.0, "Kleffe gör absolut ingenting.", "", true, "Kleffe", "Lat", 75m, 95 },
                    { 3, 5.0, "Springer 12 km per natt.", "", true, "Blixten", "Hyperaktiv", 89m, 78 },
                    { 4, 18.0, "Liten men farlig.", "", true, "Ragnar", "Aggresiv", 49m, 32 },
                    { 5, 10.0, "Ingen vet vad hon tänker härnäst.", "", true, "Kajsa", "Oförutsägbar", 120m, 42 },
                    { 6, 6.0, "Förälskar sig i alla hon möter. Även brevlådor.", "", true, "Valentina", "Kärleksfull", 110m, 120 },
                    { 7, 9.0, "Har aldrig haft en dålig dag i hela sitt liv.", "", true, "Glenn", "Glad", 95m, 145 },
                    { 8, 12.0, "Tar det lugnt. Extremt lugnt. Ibland för lugnt.", "", true, "Chillen", "Chill", 80m, 110 },
                    { 9, 20.0, "Litar inte på någon. Inte ens dig. Speciellt inte dig.", "", true, "Professor Misstänksam", "Skeptisk", 85m, 98 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
