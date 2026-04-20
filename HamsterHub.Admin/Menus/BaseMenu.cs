using System;
using HamsterHub.Admin.Services;


namespace HamsterHub.Admin.Menus;

public abstract class BaseMenu(ApiService api)
{

    // ORSAK: Min huvudloopen för console Admin
    
    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();
            PrintOptions();

            Console.Write("\n > val: ");
            var input = System.Console.ReadLine()?.Trim();

            if (input == "0")
                break;

            await HandleInputAsync(input ?? "");
        }
    }

    protected abstract void PrintOptions();
    protected abstract Task HandleInputAsync(string input);
    
    // ORSAK: Hjälpmetoder som Adminmenyerna kan använda 

    protected static void Pause()
    {
        Console.WriteLine("\n Tryck på valfri tangent...");
        Console.ReadKey(true);
    }

    protected static string PromptLine(string header)
    {
        Console.Write($"  {header}:  ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    protected static int PromptInt(string header)
    {
        while (true)
        {
            Console.Write($"  {header}:  ");
            if (int.TryParse(Console.ReadLine(), out int num))
                return num;
            
            PrintError("Ange ett giltigt heltal.");
        }
    }

    protected static decimal PromptDecimal(string header)
    {
        while (true)
        {
            Console.WriteLine($"  {header}: ");
            if (decimal.TryParse(Console.ReadLine(), out var num))
                return num;
            
            PrintError("Ogiltigt tal. Försök igen.");
        }
    }

    protected static DateTime PromptDate(string label)
    {
        while (true)
        {
            Console.Write($"  {label} (åååå-mm-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                return result;
            
            PrintError("Ogiltigt format på datum. Försök igen.");
        }
    }
    
    protected static void PrintSuccess(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  ✓ {text}");
        Console.ResetColor();
    }
    
    protected static void PrintError(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n  ✗ {text}");
        Console.ResetColor();
    }
    
    protected static void PrintInfo(string text)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  {text}");
        Console.ResetColor();
    }
    
    




}