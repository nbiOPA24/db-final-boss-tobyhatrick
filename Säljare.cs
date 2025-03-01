using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Säljare : Användare
{
    public Säljare(string namn, DatabaseRepository dbRepository) 
        : base(namn, "Säljare", dbRepository)
    {
    }

    public async Task VisaLagerAsync()
    {
        var bilar = await _dbRepository.HämtaAllaBilarAsync();

        // Filtrera bort sålda bilar
        var tillgängligaBilar = bilar.Where(b => b.Status != "Såld");

        Console.WriteLine("Tillgängliga bilar:");
        foreach (var bil in tillgängligaBilar)
        {
            Console.WriteLine($"ID: {bil.Id} -- Märke: {bil.Märke} -- Modell: {bil.Modell} -- Årsmodell: {bil.Årsmodell} -- Pris: {bil.Pris:C} -- Status: {bil.Status}");                              
        }

        // Lägg till en paus
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

    public async Task LäggTillBilAsync()
    {
        try
        {
            Console.WriteLine("Lägg till en ny bil till försäljning!");

            Console.Write("Märke: ");
            string märke = Console.ReadLine() ?? "";

            Console.Write("Modell: ");
            string modell = Console.ReadLine() ?? "";

            Console.Write("Årsmodell: ");
            if (!int.TryParse(Console.ReadLine(), out int årsmodell))
            {
                Console.WriteLine("Felaktig inmatning! Årsmodell måste vara ett nummer.");
                return;
            }

            Console.Write("Miltal: ");
            if (!int.TryParse(Console.ReadLine(), out int miltal))
            {
                Console.WriteLine("Felaktig inmatning! Miltal måste vara ett nummer.");
                return;
            }

            Console.Write("Växellåda (Manuell/Automat): ");
            string växellåda = Console.ReadLine() ?? "";

            Console.Write("Pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal pris))
            {
                Console.WriteLine("Felaktig inmatning! Pris måste vara ett nummer.");
                return;
            }

            // Skapa ett nytt bilobjekt
            var nyBil = new Bil(märke, modell, årsmodell, miltal, växellåda, pris);

            // Bekräfta med användaren
            Console.WriteLine($"Är du säker på att du vill lägga till en {märke} {modell} för säljning? (ja/nej)");
            string bekräftelse = Console.ReadLine();
            if (bekräftelse.ToLower() != "ja")
            {
                Console.WriteLine("Bilen har inte lagts till i systemet.");
                return;
            }

            // Lägg till bilen i databasen
            await _dbRepository.LäggTillBilAsync(nyBil);
            Console.WriteLine($"Bilen {märke} {modell} har lagts till i systemet!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod vid tillägg av bil: {ex.Message}");
            Console.WriteLine("Försök igen eller kontakta support.");
        }
    }
}