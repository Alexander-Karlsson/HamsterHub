using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Enums;
using HamsterHub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HamsterHub.Infrastructure.Repositories;

public class HamsterRepository(HamsterDbContext db) : IHamsterRepository
{
    
    // CRUD ------------------------
    public async Task<IEnumerable<Hamster>> GetAllAsync() =>
        await db.Hamsters.AsNoTracking().ToListAsync();

    public async Task<Hamster?> GetByIdAsync(int id) =>
        await db.Hamsters.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id)
        ?? throw new KeyNotFoundException($"Hamster med id: {id} hittades inte...");

    public async Task AddAsync(Hamster hamster)
    {
        await db.Hamsters.AddAsync(hamster);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hamster hamster)
    {
        db.Hamsters.Update(hamster);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var hamster = await db.Hamsters.FindAsync(id);
        if (hamster is null)
            throw new KeyNotFoundException($"Hamster med id: {id} hittades inte...");
        
        db.Hamsters.Remove(hamster);
        await db.SaveChangesAsync();
    }
    
    // Övrig hantering -----------------
    public async Task<IEnumerable<Hamster>> GetByPersonalityAsync(Personality personality)
    {
        var result = await db.Hamsters
            .AsNoTracking()
            .Where(h => h.Personality == personality)
            .ToListAsync();
        
        if (result.Count == 0)
            throw new ArgumentException($"Ingen hamster med personligheten '{personality}' hittades...");

        return result;
    }
    
    public async Task<Hamster?> GetCheapestAvailableAsync()
    {
        var hamsters = await db.Hamsters
            .AsNoTracking()
            .Where(h => h.IsAvailable)
            .OrderBy(h => h.PricePerDay)
            .FirstOrDefaultAsync();
        
        return hamsters;
    }
}