using HamsterHub.Domain.Entities;

namespace HamsterHub.Domain.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
}