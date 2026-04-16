using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Enums;
using HamsterHub.Domain.Interfaces;

namespace HamsterHub.Application.Services;

public class HamsterService(IHamsterRepository repo) : IHamsterService
{
    // TANKEBANA: Implementerar simpla CRUD metoder
    public async Task<IEnumerable<Hamster>> GetAllAsync() =>
        await repo.GetAllAsync();

    public async Task<Hamster?> GetByIdAsync(int id) => 
        await repo.GetByIdAsync(id);

    public async Task AddAsync(Hamster hamster) =>
        await repo.AddAsync(hamster);
    
    public async Task UpdateAsync(Hamster hamster) =>
        await repo.UpdateAsync(hamster);

    public async Task DeleteAsync(int id) =>
        await repo.DeleteAsync(id);
    
    // TANKEBANA: Skapa möjligheten att filtrera fram hamstrar efter angiven personlighet.
    public async Task<IEnumerable<Hamster>> GetByPersonalityAsync(Personality personality) =>
        await repo.GetByPersonalityAsync(personality);
    
    // TANKEBANA: Skapa möjligheten att filtrera fram det billigaste tillgängliga alternativet.
    public async Task<Hamster?> GetCheapestAvailableAsync() =>
        await repo.GetCheapestAvailableAsync();
    
    
    
}