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

        builder.Property(h => h.PricePerDay)
            .HasPrecision(18, 2);

        // ORSAK: HasConversion sparar enumvärdet som string isället för siffra i databasen.
        builder.Property(h => h.Personality)
            .HasConversion<string>(); 
        
        // ORSAK: En hamster per personlighet (enum) som seeddata så att jag kan få visuella resultat när jag skapar front end
        builder.HasData(
            new Hamster { Id = 1, Name = "Mårten", Personality = Personality.Kelig, PricePerDay = 99, IsAvailable = true, WeightInGrams = 180, AgeInMonths = 8, Description = "Världens mjukaste hamster." },
            new Hamster { Id = 2, Name = "Kleffe", Personality = Personality.Lat, PricePerDay = 75, IsAvailable = true, WeightInGrams = 95, AgeInMonths = 14, Description = "Är det ens en hamster..?" },
            new Hamster { Id = 3, Name = "Blixten",  Personality = Personality.Hyperaktiv, PricePerDay = 89, IsAvailable = true, WeightInGrams = 78, AgeInMonths = 5, Description = "Springer 12 km per natt." },
            new Hamster { Id = 4, Name = "Ragnar",  Personality = Personality.Aggresiv, PricePerDay = 49, IsAvailable = true, WeightInGrams = 32, AgeInMonths = 18, Description = "Liten men farlig." },
            new Hamster { Id = 5, Name = "Kajsa",  Personality = Personality.Oförutsägbar, PricePerDay = 120, IsAvailable = true, WeightInGrams = 42, AgeInMonths = 10, Description = "Ingen vet vad hon tänker härnäst." },
            new Hamster { Id = 6, Name = "Valentina", Personality = Personality.Kärleksfull, PricePerDay = 110, IsAvailable = true, WeightInGrams = 120, AgeInMonths = 6, Description = "Förälskar sig i alla hon möter. Även brevlådor." },
            new Hamster { Id = 7, Name = "Glenn", Personality = Personality.Glad, PricePerDay = 95, IsAvailable = true, WeightInGrams = 145, AgeInMonths = 9, Description = "Har aldrig haft en dålig dag i hela sitt liv." },
            new Hamster { Id = 8, Name = "Chilli", Personality = Personality.Chill, PricePerDay = 80, IsAvailable = true, WeightInGrams = 110, AgeInMonths = 12, Description = "Tar det lugnt. Extremt lugnt. Ibland för lugnt." },
            new Hamster { Id = 9, Name = "Skurt", Personality = Personality.Skeptisk, PricePerDay = 85, IsAvailable = true, WeightInGrams = 98, AgeInMonths = 20, Description = "Litar inte på någon. Inte ens dig. Speciellt inte dig." },
            new Hamster { Id = 10, Name = "Doris", Personality = Personality.Kärleksfull, PricePerDay = 105, IsAvailable = true, WeightInGrams = 132, AgeInMonths = 7, Description = "Kramar allt som rör sig. Och en del som inte gör det." },
            new Hamster { Id = 11, Name = "Turbo-Tobias", Personality = Personality.Hyperaktiv, PricePerDay = 92, IsAvailable = true, WeightInGrams = 67, AgeInMonths = 4, Description = "Har inte suttit still en enda sekund sedan födseln." },
            new Hamster { Id = 12, Name = "Birgitta", Personality = Personality.Skeptisk, PricePerDay = 78, IsAvailable = true, WeightInGrams = 115, AgeInMonths = 22, Description = "Har sett saker. Vill inte prata om det." },
            new Hamster { Id = 13, Name = "Smygaren", Personality = Personality.Oförutsägbar, PricePerDay = 115, IsAvailable = true, WeightInGrams = 54, AgeInMonths = 11, Description = "Dyker upp precis där du minst anar det." },
            new Hamster { Id = 14, Name = "Roffe", Personality = Personality.Glad, PricePerDay = 88, IsAvailable = true, WeightInGrams = 158, AgeInMonths = 16, Description = "Skrattar åt sina egna skämt. Skämten är faktiskt ganska bra." },
            new Hamster { Id = 15, Name = "Magnusson", Personality = Personality.Aggresiv, PricePerDay = 45, IsAvailable = true, WeightInGrams = 28, AgeInMonths = 24, Description = "Liten. Arg. En Bitter jäkel." },
            new Hamster { Id = 16, Name = "Zara", Personality = Personality.Aggresiv, PricePerDay = 95, IsAvailable = true, WeightInGrams = 103, AgeInMonths = 13, Description = "Gnäller hela dagen, varje dag..." },
            new Hamster { Id = 17, Name = "Nani", Personality = Personality.Lat, PricePerDay = 70, IsAvailable = true, WeightInGrams = 188, AgeInMonths = 19, Description = "Nani gör absolut ingenting.." },
            new Hamster { Id = 18, Name = "Fröken Pippy", Personality = Personality.Kelig, PricePerDay = 102, IsAvailable = true, WeightInGrams = 91, AgeInMonths = 6, Description = "Vill bara ligga i knät. Helst ditt." },
            new Hamster { Id = 19, Name = "Kaoz", Personality = Personality.Oförutsägbar, PricePerDay = 130, IsAvailable = true, WeightInGrams = 61, AgeInMonths = 9, Description = "Ingen vet hur han tar sig ut ur buren. Inte ens han själv." }
            );
    }
}