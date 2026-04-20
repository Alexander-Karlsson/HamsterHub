using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HamsterHub.Infrastructure.Repositories;

public class BookingRepository(HamsterDbContext db) : IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await db.Bookings.AsNoTracking().ToListAsync();

    public async Task<Booking?> GetByIdAsync(int id) =>
        await db.Bookings.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id)
        ?? throw new KeyNotFoundException($"Bokning med referens: {id} hittades inte...");

    public async Task AddAsync(Booking booking)
    {
        await db.Bookings.AddAsync(booking);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Booking booking)
    {
        db.Bookings.Update(booking);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var booking = await db.Bookings.FindAsync(id);
        if (booking is null)
            throw new KeyNotFoundException($"Bokning med referens: {id} hittades inte...");
        
        db.Bookings.Remove(booking);
        await db.SaveChangesAsync();
    }

    // Övrig hantering -----------------
    public async Task<IEnumerable<Booking>> GetBookingByCustomerNameAsync(string customerName)
    {
        var bookings = db.Bookings.Where(b => b.CustomerName.Contains(customerName));
        var result = await bookings.ToListAsync();
        if (result.Count == 0)
            throw new KeyNotFoundException($"Bokning för {customerName} hittades inte...");

        return result;
    }

    public async Task<IEnumerable<Booking>> GetBookingByDateAsync(DateTime date)
    {
        var bookings = db.Bookings.Where(b => b.StartDate <= date && b.EndDate >= date);
        var result = await bookings.ToListAsync();
        if(result.Count == 0)
            throw new KeyNotFoundException($"Hittade inga bokningar på {date}...");

        return result;
    }
    
    public async Task<IEnumerable<Booking>> GetBookingsByHamsterIdAsync(int hamsterId)
    {
        var bookings = db.Bookings.Where(b => b.HamsterId == hamsterId);
        var result = await bookings.ToListAsync();
        if (result.Count == 0)
            throw new KeyNotFoundException("Hittade inga bokingar för den angivna hamstern...");

        return result;
    }
}