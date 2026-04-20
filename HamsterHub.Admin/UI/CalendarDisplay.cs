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
        // TANKEBANA: tvinga alltid till måndagen i veckan oavsett dag
        
        // vilket datum som skickas in.
        var monday = weekStart.DayOfWeek == DayOfWeek.Sunday
            ? weekStart.AddDays(-6)
            : weekStart.AddDays(-(int)weekStart.DayOfWeek + (int)DayOfWeek.Monday);

        int weekNumber = System.Globalization.ISOWeek.GetWeekOfYear(monday);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n  Vecka {weekNumber}  ({monday:dd MMM} – {monday.AddDays(6):dd MMM yyyy})");
        Console.ResetColor();
        Console.WriteLine();

        // Kolumnbredd baserat på det längsta av hamsternamnen
        int colWidth = Math.Max(12, hamsters.Max(h => h.Name.Length) + 2);
        string colFormat = $"{{0,-{colWidth}}}";

        //hamsternamn
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{"",10}");
        foreach (var h in hamsters)
            Console.Write(string.Format(colFormat, h.Name));
        Console.WriteLine();
        Console.ResetColor();

        Console.WriteLine(new string('─', 10 + colWidth * hamsters.Count));

        // En rad per dag
        for (int d = 0; d < 7; d++)
        {
            var date = monday.AddDays(d);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{Days[d],-10}");
            Console.ResetColor();

            foreach (var hamster in hamsters)
            {
                /*
                 TANKEBANA: Kontrollerar om hamstern har en bokning som
                 finns på detta datum. Samma som i mitt API.
                */
                var booking = bookings.FirstOrDefault(b =>
                    b.HamsterId == hamster.Id &&
                    b.StartDate.Date <= date.Date &&
                    b.EndDate.Date >= date.Date);

                if (booking is null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(string.Format(colFormat, "[ledig]"));
                }
                else
                {
                    // Visar efternamnet (eller förnamn) på kudn och håller kolumnenra snygga
                    var name = booking.CustomerName.Split(' ').Last();
                    if (name.Length > colWidth - 1)
                        name = name[..(colWidth - 1)];

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(string.Format(colFormat, "[Bokad]"));
                }

                Console.ResetColor();
            }

            Console.WriteLine();
        }

        Console.WriteLine(new string('─', 10 + colWidth * hamsters.Count));
        Console.WriteLine();
    }
}