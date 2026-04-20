using HamsterHub.Admin.Models;
using HamsterHub.Admin.Services;
using HamsterHub.Admin.UI;

namespace HamsterHub.Admin.Menus;

public class AdminMenu(ApiService api) : BaseMenu(api)
{
    private const string AdminPassword = "hamster123";

    // TANKEBANA: Avvaktar med ShowAsync för att lägga till lösenordskontroll
    // innan menyn visas. I ett riktigt system hade detta troligtvis varit JWT.
    // Men tänker att detta duger gott för denna inlämning.
    public new async Task ShowAsync()
    {
        Console.Write("\n  Admin-lösenord: ");
        var password = ReadPassword();

        if (password != AdminPassword)
        {
            PrintError("Fel lösenord... Testa igen om du törs.");
            Pause();
            return;
        }

        PrintSuccess("Inloggad som administratör!");
        Thread.Sleep(800);
        await base.ShowAsync();
    }

    // TANKEBANA: Döljer lösenord med * när det skrivs in.
    private static string ReadPassword()
    {
        var password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[..^1];
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }

    protected override void PrintOptions()
    {
        Console.WriteLine("\n  ADMIN\n");
        Console.WriteLine("  -- Hamstrar -------------------");
        Console.WriteLine("  [1] Visa alla hamstrar");
        Console.WriteLine("  [2] Lägg till ny hamster");
        Console.WriteLine("  [3] Ta bort hamster");
        Console.WriteLine();
        Console.WriteLine("  -- Bokningar ------------------");
        Console.WriteLine("  [4] Visa alla bokningar");
        Console.WriteLine("  [5] Visa kalender");
        Console.WriteLine("  [6] Ta bort en bokning");
        Console.WriteLine();
        Console.WriteLine("  -- Queries --------------------");
        Console.WriteLine("  [7] Filtrera på personlighet");
        Console.WriteLine("  [8] Billigaste tillgängliga");
        Console.WriteLine("  [0] Logga ut");
    }

    protected override async Task HandleInputAsync(string input)
    {
        switch (input)
        {
            case "1": 
                await ListHamstersAsync(); 
                break;
            case "2": 
                await AddHamsterAsync(); 
                break;
            case "3": 
                await DeleteHamsterAsync(); 
                break;
            case "4": 
                await ListAllBookingsAsync(); 
                break;
            case "5": 
                await ShowCalendarAsync(); 
                break;
            case "6": 
                await DeleteBookingAsync(); 
                break;
            case "7": 
                await FilterByPersonalityAsync(); 
                break;
            case "8": 
                await ShowCheapestAsync(); 
                break;
            default:  
                PrintError("Ogiltigt val..."); 
                Pause(); 
                break;
        }
    }
    
    private async Task ListHamstersAsync()
    {
        var hamsters = await api.GetAllHamstersAsync();

        Console.WriteLine("\n  ALLA HAMSTRAR\n");
        Console.WriteLine($"  {"ID",-5}{"Namn",-22}{"Personlighet",-18}{"Pris/dag",-12}");
        Console.WriteLine(new string('─', 65));

        foreach (var h in hamsters)
            Console.WriteLine($"  {h.Id,-5}{h.Name,-22}{h.Personality,-18}{h.PricePerDay + " kr",-12}");

        Pause();
    }
    
    private async Task AddHamsterAsync()
    {
        Console.WriteLine("\n  LÄGG TILL NY HAMSTER\n");

        var name = PromptLine("Namn");
        var description = PromptLine("Beskrivning");
        var weight= PromptInt("Vikt i gram");
        var age = (double)PromptDecimal("Ålder i månader");
        var price = PromptDecimal("Pris per dag (kr)");
        
        Console.WriteLine("\n  Personligheter: Kelig, Aggresiv, Oförutsägbar, Kärleksfull,");
        Console.WriteLine("                  Glad, Chill, Skeptisk, Lat, Hyperaktiv");
        var personality = PromptLine("Personlighet");

        var request = new AddHamsterRequest(name, description, weight, age, personality, price);
        var success = await api.AddHamsterAsync(request);

        if (success) 
            PrintSuccess($"{name} har lagts till i hamstergänget!");
        
        else         
            PrintError("Misslyckades – (kontrollera att API är igång förfan!!).");

        Pause();
    }

    private async Task DeleteHamsterAsync()
    {
        await ListHamstersAsync();
        var id = PromptInt("Hamster-ID att ta bort");

        Console.Write("  Är du säker? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "y") 
        { 
            PrintInfo("Avbruten."); 
            Pause(); 
            return; 
        }

        var success = await api.DeleteHamsterAsync(id);
        
        if (success) 
            PrintSuccess("Hamster borttagen.");
        
        else         
            PrintError("Kunde inte ta bort (kanske aktiva bokningar?).");

        Pause();
    }
    
    // Hantering av bonkign
    private async Task ListAllBookingsAsync()
    {
        var bookings = await api.GetAllBookingsAsync();

        if (bookings.Count == 0)
        {
            PrintInfo("Inga bokningar finns."); 
            Pause(); 
            return;
        }

        Console.WriteLine($"\n  ALLA BOKNINGAR ({bookings.Count} st)\n");
        Console.WriteLine($"  {"ID",-6}{"Kund",-22}{"Hamster",-18}{"Period",-28}{"Totalt"}");
        Console.WriteLine(new string('─', 80));

        foreach (var b in bookings)
        {
            var period = $"{b.StartDate:dd MMM} – {b.EndDate:dd MMM yyyy}";
            Console.WriteLine($"  {b.Id,-6}{b.CustomerName,-22}{b.Hamster?.Name ?? "?",-18} {period,-25} {b.TotalPrice} kr");
        }

        

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

    private async Task DeleteBookingAsync()
    {
        await ListAllBookingsAsync();
        var id = PromptInt("Boknings-ID att ta bort");

        var success = await api.DeleteBookingAsync(id);
        
        if (success) 
            PrintSuccess("Bokning borttagen.");
        
        else 
            PrintError("Boknings-ID hittades inte.");
        

        Pause();
    }
    
    // extra queries
    private async Task FilterByPersonalityAsync()
    {
        Console.WriteLine("\n  Kelig, Aggresiv, Oförutsägbar, Kärleksfull, Glad, Chill, Skeptisk, Lat, Hyperaktiv");
        var personality = PromptLine("Personlighet");
        var result = await api.GetHamstersByPersonalityAsync(personality);

        if (result.Count == 0)
        {
            PrintError($"Ingen hamster med personligheten '{personality}'."); 
            Pause(); 
            return;
        }

        Console.WriteLine();
        foreach (var h in result)
            Console.WriteLine($"  {h.Name} – {h.PricePerDay} kr/dag – {h.Description}");

        Pause();
    }

    private async Task ShowCheapestAsync()
    {
        var h = await api.GetCheapestHamsterAsync();

        if (h is null)
        {
            PrintError("Inga tillgängliga hamstrar."); 
            Pause(); 
            return;
        }

        Console.WriteLine();
        PrintInfo($"Billigaste: {h.Name} – {h.PricePerDay} kr/dag");
        Console.WriteLine($"  {h.Description}");

        Pause();
    }
}