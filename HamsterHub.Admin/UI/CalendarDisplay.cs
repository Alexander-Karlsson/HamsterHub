using HamsterHub.Admin.Models;

namespace HamsterHub.Admin.UI;

public static class CalendarDisplay
{
    
    /*
     ÄRLIGHET: Inte jobbat mycket med att få snygga gränssnitt i konsolen tidigare
     så här använde jag mig faktiskt av AI för att snabba på 
     processen lite med formatering av kolumnerna.
     
     Förstår vad koden gör, men vill ändå vara transparant då det trots allt är en skoluppgift.
    */
    
    
    private static readonly string[] Days =
        [
            "Måndag",
            "Tisdag", 
            "Onsdag", 
            "Torsdag", 
            "Fredag", 
            "Lördag", 
            "Söndag"
        ];

    public static void ShowWeekCalendar(List<HamsterDto> hamsters, List<BookingDto> bookings, DateTime weekStart)
{
    var monday = weekStart.DayOfWeek == DayOfWeek.Sunday
        ? weekStart.AddDays(-6)
        : weekStart.AddDays(-(int)weekStart.DayOfWeek + (int)DayOfWeek.Monday);

    int weekNumber = System.Globalization.ISOWeek.GetWeekOfYear(monday);
    
    Console.WriteLine($"\n  Visar: Vecka {weekNumber}  ({monday:dd MMM} – {monday.AddDays(6):dd MMM yyyy})");
    Console.WriteLine();

    int colWidth = 12;
    string colFormat = $"{{0,-{colWidth}}}";

    // Kolumnbredd baseras på längsta namnet 
    int nameWidth = Math.Max(22, hamsters.Max(h => h.Name.Length) + 2);
    string nameFormat = $"  {{0,-{nameWidth}}}";

    // veckodagar
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(nameFormat, "");
    foreach (var day in Days)
        Console.Write(colFormat, day);
    Console.WriteLine();
    Console.ResetColor();

    Console.WriteLine(('─', nameWidth + colWidth * 7));

    // En rad per hamster
    foreach (var hamster in hamsters)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(nameFormat, hamster.Name);
        Console.ResetColor();

        for (int d = 0; d < 7; d++)
        {
            var date = monday.AddDays(d);

            var booking = bookings.FirstOrDefault(b =>
                b.HamsterId == hamster.Id &&
                b.StartDate.Date <= date.Date &&
                b.EndDate.Date >= date.Date);

            if (booking is null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(colFormat, "[ledig]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(colFormat, "[Bokad]");
            }

            Console.ResetColor();
        }

        Console.WriteLine();
    }

    Console.WriteLine(new string('─', nameWidth + colWidth * 7));
    Console.WriteLine();
}
}