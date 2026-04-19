namespace HamsterHub.API.DTOs;

public record CreateBookingDto(
    string CustomerName,
    string CustomerEmail,
    int HamsterId,
    DateTime StartDate,
    DateTime EndDate,
    string Purpose
);
                
