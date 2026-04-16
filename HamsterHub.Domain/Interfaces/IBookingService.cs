using HamsterHub.Domain.Entities;

namespace HamsterHub.Domain.Interfaces;

public interface IBookingService
{
    // CRUD --------------------------------
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task DeleteAsync(int id);
    
    // Övrig hantering -----------------
    Task<IEnumerable<Booking>> GetBookingByCustomerNameAsync(string customerName);
    Task<IEnumerable<Booking>> GetBookingByDateAsync(DateTime date);
    Task<IEnumerable<Booking>> GetBookingsByHamsterIdAsync(int hamsterId);
}