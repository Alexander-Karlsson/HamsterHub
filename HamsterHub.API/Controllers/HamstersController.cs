using HamsterHub.API.DTOs;
using HamsterHub.Domain.Enums;
using HamsterHub.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HamsterHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HamstersController(IHamsterService service, IReviewService reviewService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hamsters = await service.GetAllAsync();
        var reviews = await reviewService.GetAllAsync();
        
        var dtos = hamsters
            .Select(h => new HamsterDto(h.Id, h.Name, h.Description, 
                h.WeightInGrams, h.AgeInMonths, h.Personality.ToString(), 
                h.PricePerDay, h.IsAvailable,
            
            reviews
                .Where(r => r.HamsterId == h.Id)
                .Select(r => (double)r.Score)
                .DefaultIfEmpty(0)
                .Average()
        ));
        
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await service.GetByIdAsync(id));
    
    [HttpGet("personality/{personality}")]
    public async Task<IActionResult> GetByPersonality(Personality personality) =>
        Ok(await service.GetByPersonalityAsync(personality));

    [HttpGet("cheapest")]
    public async Task<IActionResult> GetCheapest() =>
        Ok(await service.GetCheapestAvailableAsync());

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
    
    // TANKEBANA: Struntat i POST och PUT då jag endast skapar hamstrar genom seeddata.
    
}