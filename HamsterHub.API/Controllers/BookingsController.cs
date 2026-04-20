using HamsterHub.API.DTOs;
using HamsterHub.Domain.Entities;
using HamsterHub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HamsterHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await service.GetAllAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await service.GetByIdAsync(id));

    [HttpGet("customer/{customerName}")]
    public async Task<IActionResult> GetByCustomerName(string customerName) =>
        Ok(await service.GetBookingByCustomerNameAsync(customerName));
    
    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetByDate(DateTime date) =>
        Ok(await service.GetBookingByDateAsync(date));

    [HttpGet("hamster/{hamsterId}")]
    public async Task<IActionResult> GetByHamsterId(int hamsterId) =>
        Ok(await service.GetBookingsByHamsterIdAsync(hamsterId));

    [HttpPost]
    public async Task<IActionResult> Add(CreateBookingDto dto)
    {
        var newBooking = new Booking
        {
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            CustomerAddress = dto.CustomerAddress,
            HamsterId = dto.HamsterId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Purpose = dto.Purpose,
        };

        try
        {
            await service.AddAsync(newBooking);
            return Created();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message); 
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateBookingDto dto)
    {
        var booking = await service.GetByIdAsync(id);
        if(booking is null) return NotFound();
        
        booking.StartDate = dto.StartDate;
        booking.EndDate = dto.EndDate;
        booking.HamsterId = dto.HamsterId;

        try
        {
            await service.UpdateAsync(booking);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}