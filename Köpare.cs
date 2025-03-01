using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Köpare : Användare
{
    public Köpare(string namn, DatabaseRepository dbRepository) 
        : base(namn, "Köpare", dbRepository)
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

    public async Task KöpBilAsync()
    {
        Console.WriteLine("Kul att du vill köpa en bil!");
        while (true)
        {
            // Visa tillgängliga bilar
            var bilar = await _dbRepository.HämtaAllaBilarAsync();
            var tillgängligaBilar = bilar.Where(b => b.Status != "Såld");

            Console.Clear(); // Rensa skärmen innan menyn visas
            Console.WriteLine("Tillgängliga bilar:");
            foreach (var bil in tillgängligaBilar)
            {
                Console.WriteLine($"ID: {bil.Id}, Märke: {bil.Märke}, Modell: {bil.Modell}, Årsmodell: {bil.Årsmodell}, Pris: {bil.Pris:C}, Status: {bil.Status}");
            }
            Console.WriteLine();

            // Be användaren ange ID på bilen de vill köpa
            Console.WriteLine("Ange ID på bilen du vill köpa (eller 'q' för att avbryta): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                Console.WriteLine("Köp avbrutet.");
                break;
            }

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Error - Ange ett giltigt ID nummer");
                continue;
            }

            // Hämta bilen med det angivna ID:et
            var valdBil = await _dbRepository.HämtaBilMedIdAsync(id);
            if (valdBil == null)
            {
                Console.WriteLine("Ingen bil med det angivna ID:et hittades.");
                continue;
            }

            if (valdBil.Status != "Tillgänglig")
            {
                Console.WriteLine("Tyvärr, denna bil är reserverad/såld. Välj en annan bil.");
                continue;
            }

            // Uppdatera bilens status till "Såld"
            await _dbRepository.UppdateraBilStatusAsync(id, "Såld");

            // Lägg till försäljningen i Försäljningar-tabellen
            await _dbRepository.LäggTillFörsäljningAsync(id, valdBil.Pris);

            // Visa bekräftelse och lägg till en paus
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"Grattis, du har köpt en {valdBil.Märke} {valdBil.Modell} för {valdBil.Pris:C}!");
            Console.Write("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            break; // Avsluta loopen efter ett lyckat köp
        }
    }

}
