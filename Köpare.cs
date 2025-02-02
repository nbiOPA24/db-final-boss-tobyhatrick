using System.ComponentModel;


public class Köpare : Användare
{
    public Köpare(string namn) : base(namn, "Köpare") { }


    public void VisaLagerStatus(List<Bil> lager)
    {
        Console.WriteLine("Status på våra bilar i lager just nu:");
        foreach (var Bil in lager)
        {
            Bil.DisplayInfo();
            Console.WriteLine();
        }
    }






    public void KöpBil(List<Bil> lager)
    {
        Console.WriteLine("Kul att du vill köpa en bil!");
       
        while(true)
        {
            Console.Clear();
            foreach (var Bil in lager)
            {
                Bil.DisplayInfo();
                Console.WriteLine();
            }
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
                continue; // starta om om input inte är giltigt
            }


            Bil valdbil = null;
            foreach(var bil in lager)
            {
                if (bil.Id == id)
                {
                    valdbil = bil;
                    break;
                }
            }


            if (valdbil == null)
            {
                Console.WriteLine("Ingen bil med det angina ID:et har hittats.");
                continue; // starta om om ingen bil hittats
            }


            if (valdbil.Status != "Tillgänglig")
            {
                Console.WriteLine("Tyvärr, denna bil är reserverad/såld. Välj en annan bil.");
                continue; // starta om bilen inte är tillgänlig
           
            }


            valdbil.Status = "Såld";
            Console.WriteLine($"Grattis, du har köpt en; {valdbil.Märke} {valdbil.Modell}");
            break;
        }
    }
}