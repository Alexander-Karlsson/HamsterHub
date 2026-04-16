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
}