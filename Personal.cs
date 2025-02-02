public class Personal : Användare
{
    private List<Bil> SaldaBilar = new List<Bil>(); // Försäljningshistorik


    public Personal(string namn) : base(namn, "Personal") { }


    // Visa lagerstatus
    public void VisaLagerStatus(List<Bil> lager)
    {
        Console.WriteLine("Status på alla bilar i lager:");
        foreach (var bil in lager)
        {
            bil.DisplayInfo();
        }
    }


    // Uppdatera bilstatus (sätta pris och status)
    public void UppdateraBilStatus(List<Bil> lager)
    {
        Console.WriteLine("Här kan du uppdatera bilens status och sätta pris:");


        while (true)
        {
            Console.Clear();
            foreach (var bil in lager)
            {
                bil.DisplayInfo();
                Console.WriteLine();
            }


            Console.WriteLine("Ange ID på bilen du vill uppdatera (eller 'q' för att avsluta):");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
            {
                Console.WriteLine("Åtgärden avbröts.");
                break;
            }


            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Error - Ange ett giltigt ID-nummer.");
                continue;
            }


            var valdbil = lager.FirstOrDefault(b => b.Id == id);
            if (valdbil == null)
            {
                Console.WriteLine("Ingen bil med det angivna ID:et hittades.");
                continue;
            }


            Console.WriteLine("Ange det nya priset för bilen:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal nyttPris) || nyttPris < 0)
            {
                Console.WriteLine("Error - Ange ett giltigt pris.");
                continue;
            }


            valdbil.Pris = nyttPris;
            Console.WriteLine($"Priset för bilen {valdbil.Märke} {valdbil.Modell} har uppdaterats till {nyttPris:C}.");


            Console.WriteLine("Vill du ändra bilens status? (Tillgänglig/Såld/Reserverad)");
            string nyStatus = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nyStatus))
            {
                valdbil.Status = nyStatus;
                Console.WriteLine($"Bilens status har uppdaterats till: {nyStatus}");
            }


            break;
        }
    }


    // Visa försäljningshistorik
    public void VisaFörsäljningshistorik()
    {
        Console.WriteLine("De senaste sålda bilarna:");
        if (SaldaBilar.Count == 0)
        {
            Console.WriteLine("Ingen bil har sålts än.");
        }
        else
        {
            foreach (var bil in SaldaBilar)
            {
                bil.DisplayInfo();
            }
        }
    }


    // Lägg till en bil i försäljningshistorik
    public void LäggTillFörsäljningshistorik(Bil bil)
    {
        SaldaBilar.Insert(0, bil);
        if (SaldaBilar.Count > 5)
        {
            SaldaBilar.RemoveAt(SaldaBilar.Count - 1); // Begränsa historiken till de senaste fem sålda bilarna
        }
    }
}
