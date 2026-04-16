using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Enums;

namespace HamsterHub.Domain.Interfaces;

public interface IHamsterRepository
{
    // CRUD -------------------------------
    Task<IEnumerable<Hamster>> GetAllAsync();
    Task<Hamster?> GetByIdAsync(int id);
    Task AddAsync(Hamster hamster);
    Task UpdateAsync(Hamster hamster);
    Task DeleteAsync(int id);
    
    // Övrig hantering -----------------
    Task<IEnumerable<Hamster>> GetByPersonalityAsync(Personality personality);
    Task<Hamster?> GetCheapestAvailableAsync();
    
}