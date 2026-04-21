using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HamsterHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImgFromHamster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Hamsters");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Mårten");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Är det ens en hamster..?");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Chilli");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Skurt");

            migrationBuilder.InsertData(
                table: "Hamsters",
                columns: new[] { "Id", "AgeInMonths", "Description", "IsAvailable", "Name", "Personality", "PricePerDay", "WeightInGrams" },
                values: new object[,]
                {
                    { 10, 7.0, "Kramar allt som rör sig. Och en del som inte gör det.", true, "Doris", "Kärleksfull", 105m, 132 },
                    { 11, 4.0, "Har inte suttit still en enda sekund sedan födseln.", true, "Turbo-Tobias", "Hyperaktiv", 92m, 67 },
                    { 12, 22.0, "Har sett saker. Vill inte prata om det.", true, "Birgitta", "Skeptisk", 78m, 115 },
                    { 13, 11.0, "Dyker upp precis där du minst anar det.", true, "Smygaren", "Oförutsägbar", 115m, 54 },
                    { 14, 16.0, "Skrattar åt sina egna skämt. Skämten är faktiskt ganska bra.", true, "Roffe", "Glad", 88m, 158 },
                    { 15, 24.0, "Liten. Arg. En Bitter jäkel.", true, "Magnusson", "Aggresiv", 45m, 28 },
                    { 16, 13.0, "Gnäller hela dagen, varje dag...", true, "Zara", "Aggresiv", 95m, 103 },
                    { 17, 19.0, "Nani gör absolut ingenting..", true, "Nani", "Lat", 70m, 188 },
                    { 18, 6.0, "Vill bara ligga i knät. Helst ditt.", true, "Fröken Pippy", "Kelig", 102m, 91 },
                    { 19, 9.0, "Ingen vet hur han tar sig ut ur buren. Inte ens han själv.", true, "Kaoz", "Oförutsägbar", 130m, 61 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Hamsters",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Img", "Name" },
                values: new object[] { "", "Mysiga Mårten" });

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Img" },
                values: new object[] { "Kleffe gör absolut ingenting.", "" });

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Img",
                value: "");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 4,
                column: "Img",
                value: "");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 5,
                column: "Img",
                value: "");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 6,
                column: "Img",
                value: "");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 7,
                column: "Img",
                value: "");

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Img", "Name" },
                values: new object[] { "", "Chillen" });

            migrationBuilder.UpdateData(
                table: "Hamsters",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Img", "Name" },
                values: new object[] { "", "Professor Misstänksam" });
        }
    }
}
