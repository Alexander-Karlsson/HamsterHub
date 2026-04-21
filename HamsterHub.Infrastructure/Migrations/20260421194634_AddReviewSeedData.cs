using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HamsterHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HamsterId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReviewCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.CheckConstraint("CK_Review_Score", "Score >= 1 AND Score <= 5");
                    table.ForeignKey(
                        name: "FK_Reviews_Hamsters_HamsterId",
                        column: x => x.HamsterId,
                        principalTable: "Hamsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CustomerName", "HamsterId", "ReviewCreatedDate", "Score" },
                values: new object[,]
                {
                    { 1, "Mysiga Mårten levde upp till sitt namn. Kramade honom hela mötet.", "Erik Svensson", 1, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 2, "Väldigt mjuk och snäll. Lite för bekväm för att delta i presentationen.", "Linda Holm", 1, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, "Kleffe sov hela dagen. Inspirerande på sitt sätt.", "Jonas Bergman", 2, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, "Blixten sprang runt på kontoret och höjde energin med 500%.", "Sara Nilsson", 3, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, "Ragnar bet mig två gånger. Respekterar honom ändå.", "Marcus Lind", 4, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, "Kajsa överraskade oss konstant. Perfekt för kreativa möten.", "Anna Karlsson", 5, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 7, "Valentina satt i knät på vår VD hela dagen. Han grät lite.", "Peter Ström", 6, new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 8, "Glenn är en solstråle. Hela teamet mår bättre av honom.", "Maja Eriksson", 7, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 9, "Chillen satt still och mediterade. Exakt vad vi behövde.", "David Johansson", 8, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 10, "Professor Misstänksam lät inte någon komma nära. Passade bra på juridikavdelningen.", "Karin Lundqvist", 9, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HamsterId",
                table: "Reviews",
                column: "HamsterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
