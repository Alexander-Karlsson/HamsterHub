using HamsterHub.API.DTOs;
using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HamsterHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController(IReviewService service) : ControllerBase
{

    [HttpGet("hamster/{hamsterId}")]
    public async Task<IActionResult> GetByHamster(int hamsterId) =>
        Ok(await service.GetByHamsterIdAsync(hamsterId));

    [HttpPost]
    public async Task<IActionResult> Add(CreateReviewDto dto)
    {
        var review = new Review
        {
            HamsterId = dto.HamsterId,
            Score = dto.Score,
            CustomerName = dto.CustomerName,
            Comment = dto.Comment
        };

        await service.AddAsync(review);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}