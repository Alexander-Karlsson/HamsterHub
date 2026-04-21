namespace HamsterHub.API.DTOs;

public record HamsterDto(
    int Id,
    string Name,
    string Description,
    int WeightInGrams,
    double AgeInMonths,
    string Personality,
    decimal PricePerDay,
    bool IsAvailable,
    double AverageScore
);