namespace HamsterHub.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = String.Empty;
    public string CustomerEmail { get; set; } = String.Empty;
    public string CustomerAddress { get; set; } = String.Empty;
    public int HamsterId { get; set; }
    public Hamster Hamster { get; set; } = null!;
    public DateTime StartDate  { get; set; }
    public DateTime EndDate { get; set; }
    public string Purpose { get; set; } = String.Empty;

    public int DaysBooked => (EndDate - StartDate).Days;
    public decimal TotalPrice => DaysBooked * Hamster.PricePerDay;
}