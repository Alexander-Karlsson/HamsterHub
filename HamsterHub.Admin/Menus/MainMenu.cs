using System.Diagnostics;
using HamsterHub.Admin.Services;

namespace HamsterHub.Admin.Menus;

public class MainMenu(ApiService api) : BaseMenu(api)
{
    protected override void PrintOptions()
    {
        Console.WriteLine("\n  VÄLKOMMEN TILL HAMSTERHUB! \n");
        Console.WriteLine("  [1] Bokning");
        Console.WriteLine("  [2] Admin");
        Console.WriteLine("\n  [0] Avsluta");
    }

    protected override async Task HandleInputAsync(string input)
    {
        switch (input)
        {
            case "1":
                var bokningMenu = new BookingMenu(api);
                await bokningMenu.ShowAsync();
                break;
            case "2":
                var adminMenu = new AdminMenu(api);
                await adminMenu.ShowAsync();
                break;
            default:
                PrintError("Ogiltigt val...");
                Pause();
                break;
        }
    }
}