using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        try
        {
            var dbRepository = new DatabaseRepository("Server=gondolin667.org;Database=yhstudent93_MyFirstDataBase;User Id=yhstudent93;Password=lzXDn1gKfFXh;TrustServerCertificate=True;");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till vår bilfirma!");
                Console.WriteLine("1: Köpare  2: Säljare  3: Personal  4: Avsluta");
                var choice = Console.ReadLine();

                if (choice == "4") break;

                switch (choice)
                {
                    case "1": await KöpareMeny(dbRepository); break;
                    case "2": await SäljareMeny(dbRepository); break;
                    case "3": await PersonalMeny(dbRepository); break;
                    default: Console.WriteLine("Fel val, försök igen."); break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod: {ex.Message}");
        }
    }

    static async Task KöpareMeny(DatabaseRepository db)
    {
        var köpare = new Köpare("Köpare", db);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1: Visa lager  2: Köp bil  3: Tillbaka");
            var choice = Console.ReadLine();
            if (choice == "3") break;

            if (choice == "1") await köpare.VisaLagerAsync();
            else if (choice == "2") await köpare.KöpBilAsync();
        }
    }

    static async Task SäljareMeny(DatabaseRepository db)
    {
        var säljare = new Säljare("Säljare", db);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1: Visa lager  2: Lägg till bil  3: Tillbaka");
            var choice = Console.ReadLine();
            if (choice == "3") break;

            if (choice == "1") await säljare.VisaLagerAsync();
            else if (choice == "2") await säljare.LäggTillBilAsync();
        }
    }

    static async Task PersonalMeny(DatabaseRepository db)
    {
        var personal = new Personal("Personal", db);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1: Visa lager  2: Uppdatera bilstatus  3: Visa försäljningshistorik");
            Console.WriteLine("4: Lägg till underhåll  5: Visa underhållshistorik  6: Lägg till reservdel");
            Console.WriteLine("7: Visa statistik  8: Tillbaka");
            var choice = Console.ReadLine();

            if (choice == "8") break;

            switch (choice)
            {
                case "1": await personal.VisaLagerAsync(); break;
                case "2": await personal.UppdateraBilStatusAsync(); break;
                case "3": await personal.VisaFörsäljningshistorikAsync(); break;
                case "4": await personal.LäggTillUnderhållAsync(); break;
                case "5": await personal.VisaUnderhållshistorikAsync(); break;
                case "6": await personal.LäggTillReservdelAsync(); break;
                case "7": await personal.VisaStatistikAsync(); break;
                default: Console.WriteLine("Fel val, försök igen."); break;
            }
        }
    }
}
