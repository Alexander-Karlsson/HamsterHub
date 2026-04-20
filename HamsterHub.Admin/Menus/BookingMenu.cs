using HamsterHub.Admin.Models;
using HamsterHub.Admin.Services;
using HamsterHub.Admin.UI;

namespace HamsterHub.Admin.Menus;

public class BookingMenu(ApiService api) : BaseMenu(api)
{
    protected override void PrintOptions()
    {
        Console.WriteLine("\n  BOKNING \n");
        Console.WriteLine("  [1] Visa alla hamstrar");
        Console.WriteLine("  [2] Visa kalender");
        Console.WriteLine("  [3] Boka en hamster");
        Console.WriteLine("  [4] Avboka en hamster");
        Console.WriteLine("  [5] Sök bokningar");
        Console.WriteLine("\n  [0] Tillbaka");
    }

    protected override async Task HandleInputAsync(string input)
    {
        switch (input)
        {
            case "1": 
                await ShowHamstersAsync(); 
                break;
            case "2": 
                await ShowCalendarAsync(); 
                break;
            case "3": 
                await CreateBookingAsync(); 
                break;
            case "4": 
                await CancelBookingAsync(); 
                break;
            case "5": 
                await SearchByCustomerAsync(); 
                break;
            default:  
                PrintError("Ogiltigt val."); 
                Pause(); 
                break;
        }
    }
    
    private async Task ShowHamstersAsync()
    {
        var hamsters = await api.GetAllHamstersAsync();

        Console.WriteLine("\n  TILLGÄNGLIGA HAMSTRAR\n");
        Console.WriteLine($"  {"ID",-5}{"Namn",-22}{"Personlighet",-18}{"Pris/dag"}");
        Console.WriteLine(new string('─', 55));

        foreach (var h in hamsters)
            Console.WriteLine($"  {h.Id,-5}{h.Name,-22}{h.Personality,-18}{h.PricePerDay} kr");

        Pause();
    }
    
    private async Task ShowCalendarAsync()
    {
        var hamsters = await api.GetAllHamstersAsync();
        var bookings = await api.GetAllBookingsAsync();
        var currentWeek = DateTime.Today;

        while (true)
        {
            Console.Clear();
            CalendarDisplay.ShowWeekCalendar(hamsters, bookings, currentWeek);

            Console.WriteLine("  [N] Nästa vecka   [B] Förra veckan   [0] Tillbaka");
            Console.Write("\n  > Val: ");
            var nav = Console.ReadLine()?.Trim().ToUpper();

            if (nav == "N")      
                currentWeek = currentWeek.AddDays(7);
            
            else if (nav == "B")
                currentWeek = currentWeek.AddDays(-7);
            
            else break;
        }
    }
    
    // Skapa bokning i konsol.
    private async Task CreateBookingAsync()
    {
        var hamsters = await api.GetAllHamstersAsync();

        Console.WriteLine("\n  TILLGÄNGLIGA HAMSTRAR\n");
        foreach (var h in hamsters)
            Console.WriteLine($"  [{h.Id}] {h.Name} – {h.Personality}, {h.PricePerDay} kr/dag");

        Console.WriteLine();
        var name    = PromptLine("Ditt namn (För & efternamn)");
        var email   = PromptLine("E-postadress");
        var address = PromptLine("Adress");
        var id      = PromptInt("Hamster ID");
        var start   = PromptDate("Startdatum");
        var end     = PromptDate("Slutdatum");
        var purpose = PromptLine("Syfte");

        if (end <= start)
        {
            PrintError("Slutdatumet måste vara efter startdatumet.");
            Pause();
            return;
        }

        // TANKEBANA: Jag visar totalpris innan användaren bekräftar bokningen för bekvämlighet.
        var hamster = hamsters.FirstOrDefault(h => h.Id == id);
        if (hamster is not null)
        {
            int days = (end - start).Days;
            PrintInfo($"Pris: {days} dagar × {hamster.PricePerDay} kr = {days * hamster.PricePerDay} kr");
        }

        Console.Write("\n  Bekräfta bokning? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "y")
        {
            PrintInfo("Bokning avbruten.");
            Pause();
            return;
        }

        var request = new CreateBookingRequest(name, email, address, id, start, end, purpose);
        var (success, error) = await api.CreateBookingAsync(request);

        if (success) PrintSuccess("Bokning skapad!");
        else         PrintError($"Bokning misslyckades: {error}");

        Pause();
    }
    
    // Avboka i konsol.
    private async Task CancelBookingAsync()
    {
        var name = PromptLine("Ditt namn");
        var bookings = await api.GetBookingByCustomerAsync(name);

        if (bookings.Count == 0)
        {
            PrintError("Inga bokningar hittades.");
            Pause();
            return;
        }

        foreach (var b in bookings)
            Console.WriteLine($"  [{b.Id}] {b.Hamster?.Name ?? "?"} | {b.StartDate:dd MMM} – {b.EndDate:dd MMM yyyy}");

        var bookingId = PromptInt("\n  Boknings-ID att avboka");

        Console.Write("  Är du säker? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "y")
        {
            PrintInfo("Avbruten."); 
            Pause(); 
            return;
        }

        var success = await api.DeleteBookingAsync(bookingId);
        
        if (success) 
            PrintSuccess("Bokning avbokad.");
        else         
            PrintError("Bokningen kunde inte hittas.");

        Pause();
    }
    
    // Sök bokning efter kundnanm i kosnol.
    private async Task SearchByCustomerAsync()
    {
        var name = PromptLine("Kundnamn");
        var bookings = await api.GetBookingByCustomerAsync(name);

        if (bookings.Count == 0)
        {
            PrintError("Inga bokningar hittades."); 
            Pause(); 
            return;
        }

        foreach (var b in bookings)
        {
            Console.WriteLine($"\n  #{b.Id} | {b.Hamster?.Name ?? "?"} | {b.StartDate:dd MMM} – {b.EndDate:dd MMM yyyy} | {b.TotalPrice} kr");
            Console.WriteLine($"  Syfte: {b.Purpose}");
        }

        Pause();
    }
}