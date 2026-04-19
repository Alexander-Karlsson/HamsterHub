using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamsterHub.Infrastructure.Configurations;

public class HamsterConfigurations : IEntityTypeConfiguration<Hamster>
{
    public void Configure(EntityTypeBuilder<Hamster> builder)
    {
        builder.HasKey(h => h.Id);
        
        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.Description)
            .HasMaxLength(500);

        builder.Property(h => h.Img)
            .HasMaxLength(500);

        builder.Property(h => h.PricePerDay)
            .HasPrecision(18, 2);

        // ORSAK: HasConversion sparar enumvärdet som string isället för siffra i databasen.
        builder.Property(h => h.Personality)
            .HasConversion<string>(); 
        
        // ORSAK: En hamster per personlighet (Min enum) som seeddata så att jag kan få visuella resultat när jag skapar front end
        builder.HasData(
            new Hamster { Id = 1, Name = "Mysiga Mårten", Personality = Personality.Kelig, PricePerDay = 99, IsAvailable = true, WeightInGrams = 180, AgeInMonths = 8, Img = "", Description = "Världens mjukaste hamster." },
            new Hamster { Id = 2, Name = "Kleffe", Personality = Personality.Lat, PricePerDay = 75, IsAvailable = true, WeightInGrams = 95, AgeInMonths = 14, Img = "", Description = "Kleffe gör absolut ingenting." },
            new Hamster { Id = 3, Name = "Blixten",  Personality = Personality.Hyperaktiv, PricePerDay = 89, IsAvailable = true, WeightInGrams = 78, AgeInMonths = 5, Img = "", Description = "Springer 12 km per natt." },
            new Hamster { Id = 4, Name = "Ragnar",  Personality = Personality.Aggresiv, PricePerDay = 49, IsAvailable = true, WeightInGrams = 32, AgeInMonths = 18, Img = "", Description = "Liten men farlig." },
            new Hamster { Id = 5, Name = "Kajsa",  Personality = Personality.Oförutsägbar, PricePerDay = 120, IsAvailable = true, WeightInGrams = 42, AgeInMonths = 10, Img = "", Description = "Ingen vet vad hon tänker härnäst." },
            new Hamster { Id = 6, Name = "Valentina", Personality = Personality.Kärleksfull, PricePerDay = 110, IsAvailable = true, WeightInGrams = 120, AgeInMonths = 6, Img = "", Description = "Förälskar sig i alla hon möter. Även brevlådor." },
            new Hamster { Id = 7, Name = "Glenn", Personality = Personality.Glad, PricePerDay = 95, IsAvailable = true, WeightInGrams = 145, AgeInMonths = 9, Img = "", Description = "Har aldrig haft en dålig dag i hela sitt liv." },
            new Hamster { Id = 8, Name = "Chillen", Personality = Personality.Chill, PricePerDay = 80, IsAvailable = true, WeightInGrams = 110, AgeInMonths = 12, Img = "", Description = "Tar det lugnt. Extremt lugnt. Ibland för lugnt." },
            new Hamster { Id = 9, Name = "Professor Misstänksam", Personality = Personality.Skeptisk, PricePerDay = 85, IsAvailable = true, WeightInGrams = 98, AgeInMonths = 20, Img = "", Description = "Litar inte på någon. Inte ens dig. Speciellt inte dig." }
        );
    }
}