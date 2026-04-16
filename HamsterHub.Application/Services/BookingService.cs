using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Interfaces;

namespace HamsterHub.Application.Services;

public class BookingService(IBookingRepository repo) : IBookingService
{
    // TANKEBANA: Implementerar simpla CRUD metoder
    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await repo.GetAllAsync();
    
    public async Task<Booking?> GetByIdAsync(int id) => 
        await repo.GetByIdAsync(id);

    public async Task AddAsync(Booking booking) =>
        await repo.AddAsync(booking);
    
    public async Task UpdateAsync(Booking booking) =>
        await repo.UpdateAsync(booking);

    public async Task DeleteAsync(int id) =>
        await repo.DeleteAsync(id);
    
    // TANKEBANA: Skapa möjligheten att filtrera fram booking genom att söka på kundnamn.
    public async Task<IEnumerable<Booking>> GetBookingByCustomerNameAsync(string customerName) =>
        await repo.GetBookingByCustomerNameAsync(customerName);
    
    // TANKEBANA: Skapa möjligheten att filtrera bokningar för ett specifikt datum.
    public async Task<IEnumerable<Booking>> GetBookingByDateAsync(DateTime date) =>
        await repo.GetBookingByDateAsync(date);

    // TANKEBANA: Skapa möjligheten att filtrera fram alla bokningar för angiven hamster.
    public async Task<IEnumerable<Booking>> GetBookingsByHamsterIdAsync(int hamsterId) =>
        await repo.GetBookingsByHamsterIdAsync(hamsterId);


}