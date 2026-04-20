namespace HamsterHub.Admin.Models;

public record HamsterDto(
    int Id, 
    string Name, 
    string Description,
    string Personality, 
    decimal PricePerDay, 
    bool IsAvailable
    );

public record BookingDto(
    int Id, 
    string CustomerName, 
    int HamsterId, 
    HamsterDto? Hamster, 
    DateTime StartDate, 
    DateTime EndDate, 
    decimal TotalPrice,
    string Purpose
    );

public record AddHamsterRequest(
    string Name,
    string Description,
    int WeightInGrams,
    double AgeInMonths,
    string Personality,
    decimal PricePerDay
);

public record CreateBookingRequest(
    string CustomerName,
    string CustomerEmail,
    string CustomerAddress,
    int HamsterId,
    DateTime StartDate,
    DateTime EndDate,
    string Purpose
);