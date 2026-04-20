namespace HamsterHub.API.DTOs;

public record CreateBookingDto(
    string CustomerName,
    string CustomerEmail,
    string CustomerAddress,
    int HamsterId,
    DateTime StartDate,
    DateTime EndDate,
    string Purpose
);
                
