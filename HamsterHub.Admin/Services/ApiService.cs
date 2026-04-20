using System.Net.Http.Json;
using HamsterHub.Admin.Models;

namespace HamsterHub.Admin.Services;

public class ApiService(HttpClient http)
{
    
    // Hamster end points ------------
    public async Task<List<HamsterDto>> GetAllHamstersAsync()
    {
        var response = await http.GetAsync("api/hamsters");
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<List<HamsterDto>>() ?? [];
    }

    public async Task<HamsterDto?> GetByIdAsync(int id)
    {
        var response = await http.GetAsync($"api/hamsters/{id}");
        if(!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<HamsterDto?>();
    }
    
    public async Task<List<HamsterDto>> GetHamstersByPersonalityAsync(string personality)
    {
        var response = await http.GetAsync($"api/hamsters/personality/{personality}");
        if (!response.IsSuccessStatusCode) return [];
        
        return await response.Content.ReadFromJsonAsync<List<HamsterDto>>() ?? [];
    }
    
    public async Task<HamsterDto?> GetCheapestHamsterAsync()
    {
        var response = await http.GetAsync("api/hamsters/cheapest");
        if (!response.IsSuccessStatusCode) return null;
        
        return await response.Content.ReadFromJsonAsync<HamsterDto>();
    }
    
    public async Task<bool> AddHamsterAsync(AddHamsterRequest request)
    {
        var response = await http.PostAsJsonAsync("api/hamsters", request);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<bool> DeleteHamsterAsync(int id)
    {
        var response = await http.DeleteAsync($"api/hamsters/{id}");
        return response.IsSuccessStatusCode;
    }
    
    // Booking end points ---------------------------

    public async Task<List<BookingDto>> GetAllBookingsAsync()
    {
        var response = await http.GetAsync("api/bookings");
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<List<BookingDto>>() ?? [];
    }

    public async Task<BookingDto?> GetBookingByIdAsync(int id)
    {
        var response = await http.GetAsync($"api/bookings/{id}");
        if(!response.IsSuccessStatusCode) return null;
        
        return await response.Content.ReadFromJsonAsync<BookingDto?>();
    }

    public async Task<List<BookingDto>> GetBookingByCustomerAsync(string customerName)
    {
        var response = await http.GetAsync($"api/bookings/customer/{Uri.EscapeDataString(customerName)}");
        if (!response.IsSuccessStatusCode) return [];
        
        return await response.Content.ReadFromJsonAsync<List<BookingDto>>() ?? [];
    }

    public async Task<(bool Success, string? errorMsg)> CreateBookingAsync(CreateBookingRequest request)
    {
        var response = await http.PostAsJsonAsync("api/bookings", request);
        if (response.IsSuccessStatusCode) return (true, null);
        
        var errorMsg = await response.Content.ReadAsStringAsync();
        return (false, errorMsg);
    }

    public async Task<bool> DeleteBookingAsync(int id)
    {
        var response = await http.DeleteAsync($"api/bookings/{id}");
        return response.IsSuccessStatusCode;
    }
}