using HamsterHub.Domain.Enums;

namespace HamsterHub.Domain.Entities;

public class Hamster
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int WeightInGrams { get; set; }
    public double AgeInMonths { get; set; }
    public Personality Personality { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;
}