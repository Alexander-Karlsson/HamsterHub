using HamsterHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamsterHub.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.Comment)
            .HasMaxLength(500);

        // ORSAK: Score kan endast vara mellan 1-5.
        builder.ToTable(t => t.HasCheckConstraint("CK_Review_Score", "Score >= 1 AND Score <= 5"));

        builder.HasOne(r => r.Hamster)
            .WithMany(h => h.Reviews)
            .HasForeignKey(r => r.HamsterId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // ORSAK: Seeddata av samma anledning somjag har det i HamsterConfig
        builder.HasData(
            new Review { Id = 1, HamsterId = 1, CustomerName = "Erik Svensson", Score = 5, Comment = "Mysiga Mårten levde upp till sitt namn. Kramade honom hela mötet.", ReviewCreatedDate = new DateTime(2025, 11, 3) },
            new Review { Id = 2, HamsterId = 1, CustomerName = "Linda Holm", Score = 4, Comment = "Väldigt mjuk och snäll. Lite för bekväm för att delta i presentationen.", ReviewCreatedDate = new DateTime(2025, 12, 14) },
            new Review { Id = 3, HamsterId = 2, CustomerName = "Jonas Bergman", Score = 3, Comment = "Kleffe sov hela dagen. Inspirerande på sitt sätt.", ReviewCreatedDate = new DateTime(2026, 1, 8) },
            new Review { Id = 4, HamsterId = 3, CustomerName = "Sara Nilsson", Score = 5, Comment = "Blixten sprang runt på kontoret och höjde energin med 500%.", ReviewCreatedDate = new DateTime(2026, 1, 22) },
            new Review { Id = 5, HamsterId = 4, CustomerName = "Marcus Lind", Score = 2, Comment = "Ragnar bet mig två gånger. Respekterar honom ändå.", ReviewCreatedDate = new DateTime(2026, 2, 5) },
            new Review { Id = 6, HamsterId = 5, CustomerName = "Anna Karlsson", Score = 4, Comment = "Kajsa överraskade oss konstant. Perfekt för kreativa möten.", ReviewCreatedDate = new DateTime(2026, 2, 18) },
            new Review { Id = 7, HamsterId = 6, CustomerName = "Peter Ström", Score = 5, Comment = "Valentina satt i knät på vår VD hela dagen. Han grät lite.", ReviewCreatedDate = new DateTime(2026, 3, 2) },
            new Review { Id = 8, HamsterId = 7, CustomerName = "Maja Eriksson", Score = 5, Comment = "Glenn är en solstråle. Hela teamet mår bättre av honom.", ReviewCreatedDate = new DateTime(2026, 3, 15) },
            new Review { Id = 9, HamsterId = 8, CustomerName = "David Johansson", Score = 4, Comment = "Chillen satt still och mediterade. Exakt vad vi behövde.", ReviewCreatedDate = new DateTime(2026, 3, 28) },
            new Review { Id = 10, HamsterId = 9, CustomerName = "Karin Lundqvist", Score = 3, Comment = "Professor Misstänksam lät inte någon komma nära. Passade bra på juridikavdelningen.", ReviewCreatedDate = new DateTime(2026, 4, 10) }
);
    }
}