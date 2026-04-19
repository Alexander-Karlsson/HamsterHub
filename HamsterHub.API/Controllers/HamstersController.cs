using HamsterHub.Domain.Enums;
using HamsterHub.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HamsterHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HamstersController(IHamsterService service) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await service.GetAllAsync());
    
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