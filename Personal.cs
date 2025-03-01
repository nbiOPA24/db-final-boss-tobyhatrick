public class Personal : Användare
{
    private readonly DatabaseRepository _dbRepository;

    public Personal(string namn, DatabaseRepository dbRepository) 
    : base(namn, "Personal", dbRepository)
    {
        _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
    }

    public async Task VisaLagerAsync()
    {
        var bilar = await _dbRepository.HämtaAllaBilarAsync();
        Console.WriteLine("Alla bilar:");
        foreach (var bil in bilar)
        {
            Console.WriteLine($"ID: {bil.Id} -- Märke: {bil.Märke} -- Modell: {bil.Modell} -- Årsmodell: {bil.Årsmodell} -- Pris: {bil.Pris:C} -- Status: {bil.Status}");
        }    
        // Lägg till en paus
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
    

    public async Task UppdateraBilStatusAsync()
    {
        // Visa alla bilar (inklusive sålda och reserverade)
        var bilar = await _dbRepository.HämtaAllaBilarAsync();

        Console.WriteLine("Alla bilar:");
        foreach (var bil in bilar)
        {
            Console.WriteLine($"ID: {bil.Id}, Märke: {bil.Märke}, Modell: {bil.Modell}, Årsmodell: {bil.Årsmodell}, Status: {bil.Status}");
        }

        Console.WriteLine("Ange ID på bilen du vill uppdatera:");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Ogiltigt ID.");
            return;
        }

        // Kontrollera att bilen finns
        var valdBil = await _dbRepository.HämtaBilMedIdAsync(id);
        if (valdBil == null)
        {
            Console.WriteLine("Ingen bil med det angivna ID:et hittades.");
            return;
        }

        Console.WriteLine("Ange ny status (Tillgänglig/Reserverad/Såld):");
        string nyStatus = Console.ReadLine();

        // Validera status
        if (nyStatus != "Tillgänglig" && nyStatus != "Reserverad" && nyStatus != "Såld")
        {
            Console.WriteLine("Ogiltig status. Ange 'Tillgänglig', 'Reserverad' eller 'Såld'.");
            return;
        }

        await _dbRepository.UppdateraBilStatusAsync(id, nyStatus);
        Console.WriteLine("Bilens status har uppdaterats!");
    }

    public async Task VisaFörsäljningshistorikAsync()
    {
        var såldaBilar = await _dbRepository.HämtaSåldaBilarAsync();

        if (såldaBilar == null || !såldaBilar.Any())
        {
            Console.WriteLine("Inga bilar har sålts ännu.");
        }
        else
        {
            Console.WriteLine("Försäljningshistorik:");
            foreach (var bil in såldaBilar)
            {
                Console.WriteLine($"ID: {bil.Id}, Märke: {bil.Märke}, Modell: {bil.Modell}, Årsmodell: {bil.Årsmodell}, Status: {bil.Status}");
            }
        }

        // Lägg till en paus
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

    
    public async Task LäggTillUnderhållAsync()
    {
        var bilar = await _dbRepository.HämtaAllaBilarAsync();
        Console.WriteLine("Vilken bil behöver underhåll?:");
        foreach (var bil in bilar)
        {
            Console.WriteLine($"ID: {bil.Id}, Märke: {bil.Märke}, Modell: {bil.Modell}, Årsmodell: {bil.Årsmodell}, Status: {bil.Status}");
        }
        Console.WriteLine();
        Console.Write("Ange bil ID: ");
        if (!int.TryParse(Console.ReadLine(), out int bilId))
        {
            Console.WriteLine("Ogiltigt bil ID.");
            return;
        }

        Console.Write("Beskrivning: ");
        string beskrivning = Console.ReadLine();

        Console.Write("Kostnad: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal kostnad))
        {
            Console.WriteLine("Ogiltig kostnad.");
            return;
        }

        // Lägg till underhållet i databasen
        await _dbRepository.LäggTillUnderhållAsync(bilId, beskrivning, kostnad);

        // Bekräfta att underhållet har lagts till
        Console.WriteLine($"Underhållet '{beskrivning}' har lagts till för bilen med ID {bilId}.");

        // Lägg till en paus
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

    public async Task VisaUnderhållshistorikAsync()
    {
        // Hämta alla bilar
        var bilar = await _dbRepository.HämtaAllaBilarAsync();

        // Visa bilar och låt användaren välja en bil
        Console.WriteLine("Ange ID för bilen du vill se underhållshistorik:");
        foreach (var bil in bilar)
        {
            Console.WriteLine($"ID: {bil.Id}, Märke: {bil.Märke}, Modell: {bil.Modell}, Årsmodell: {bil.Årsmodell}, Status: {bil.Status}");
        }
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int bilId))
        {
            Console.WriteLine("Ogiltigt bil ID.");
            return;
        }

        // Hämta underhållshistorik för den valda bilen
        var underhåll = await _dbRepository.HämtaUnderhållshistorikAsync(bilId);

        // Visa underhållshistorik
        Console.WriteLine($"\nUnderhållshistorik för bil ID {bilId}:");
        foreach (var u in underhåll)
        {
            Console.WriteLine($"Beskrivning: {u.Beskrivning}, Kostnad: {u.Kostnad:C}, Datum: {u.Datum}");
        }

        // Lägg till en paus
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }


    public async Task LäggTillReservdelAsync()
    {
        // Hämta alla bilar
        var bilar = await _dbRepository.HämtaAllaBilarAsync();

        // Visa bilar och låt användaren välja en bil
        Console.WriteLine("Tillgängliga bilar:");
        foreach (var bil in bilar)
        {
            Console.WriteLine($"ID: {bil.Id}, Märke: {bil.Märke}, Modell: {bil.Modell}, Årsmodell: {bil.Årsmodell}, Status: {bil.Status}");
        }

        // Be användaren ange ID för bilen
        Console.Write("\nAnge ID för bilen du vill lägga till reservdel: ");
        if (!int.TryParse(Console.ReadLine(), out int bilId))
        {
            Console.WriteLine("Ogiltigt ID.");
            return;
        }

        // Be användaren ange information om reservdelen
        Console.Write("Namn på reservdel: ");
        string namn = Console.ReadLine();

        Console.Write("Pris: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal pris))
        {
            Console.WriteLine("Ogiltigt pris.");
            return;
        }

        // Lägg till reservdelen i databasen
        await _dbRepository.LäggTillReservdelAsync(bilId, namn, pris);

        // Bekräfta att reservdelen har lagts till
        Console.WriteLine($"Reservdelen '{namn}' har lagts till för bilen med ID {bilId}.");

        // Lägg till en paus
        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

    public async Task VisaStatistikAsync()
    {
        try
        {
            // Visa statistik för försäljningar
            var genomsnittligtPris = await _dbRepository.HämtaGenomsnittligtPrisAsync();
            var totalFörsäljning = await _dbRepository.HämtaTotalFörsäljningAsync();
            Console.WriteLine();
            Console.WriteLine($"Genomsnittligt pris på sålda bilar: {genomsnittligtPris:C}");
            Console.WriteLine($"Total försäljning: {totalFörsäljning:C}");

            // Visa historik över reservdelar
            var reservdelar = await _dbRepository.HämtaAllaReservdelarAsync();
            if (reservdelar == null || !reservdelar.Any())
            {
                Console.WriteLine("\nInga reservdelar har lagts till ännu.");
            }
            else
            {
                Console.WriteLine("\nReservdelshistorik:");
                foreach (var reservdel in reservdelar)
                {
                    Console.WriteLine($"Bil ID: {reservdel.BilId}, Reservdel: {reservdel.Namn}, Pris: {reservdel.Pris:C}");
                }

                // Beräkna totalt pris för alla reservdelar
                var totaltPrisReservdelar = reservdelar.Sum(r => r.Pris);
                Console.WriteLine($"\nTotalt pris för alla reservdelar: {totaltPrisReservdelar:C}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod: {ex.Message}");
        }
        finally
        {
            // Lägg till en paus
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}