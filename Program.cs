using System.Runtime.CompilerServices;


class Program
{
    static List<Bil> lager = new List<Bil>
        {
            new Bil(1,"Volvo", "XC60,", 2020, 25000, "Automat", null, "Tillgänglig"),
            new Bil(2, "Toyota", "Corolla", 2018, 45000, "Manuell", null, "Tillgänglig"),
            new Bil(3, "Ford", "Mustang", 2022, 5000, "Automat", null, "Tillgänglig")
        };
     
    static void Main()
    {
        bool avslutaProgrammet = false;


        while(!avslutaProgrammet)
        {
            Console.Clear();
            Console.WriteLine("Välkommen till våran Bilfirma!");
            Console.WriteLine("Välj din roll / Avsluta ");
            Console.WriteLine("'1' Köpare  '2' Säljare  '3' Personal  '4' Avsluta");
           
            var choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    KöpareMeny();
                    break;
                case "2":
                    SäljareMeny();
                    break;
                case "3":
                    PersonalMeny();
                    break;
                case "4":
                    Console.WriteLine("Avslutar programmet...");
                    avslutaProgrammet = true;
                    break;
                default:
                    Console.WriteLine("Error, försök igen.");
                    break;
            }
        }
    }


    static void KöpareMeny()
    {
        Köpare köpare = new Köpare("Köpare");
        bool tillbakaTillHuvudmeny = false;


        while(!tillbakaTillHuvudmeny)
        {
            Console.Clear();
            Console.WriteLine("Meny för köpare:");
            Console.WriteLine("1. Visa lagerstatus");
            Console.WriteLine("2. Köp din dröm bil!");
            Console.WriteLine("3. Tilbaka till huvudmenyn");


            var choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    köpare.VisaLagerStatus(lager);
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;


                case "2":
                    köpare.KöpBil(lager);
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;
               
                case "3":
                    tillbakaTillHuvudmeny = true;
                    break;
               
                default:
                    Console.WriteLine("Error, försök igen.");
                    break;
            }
        }
    }






    static void SäljareMeny()
    {
        Säljare säljare = new Säljare("Säljare");
        bool tillbakaTillHuvudmeny = false;


        while (!tillbakaTillHuvudmeny)
        {
            Console.Clear();
            Console.WriteLine("Meny för säljare:");
            Console.WriteLine("1. Visa lagerstatus");
            Console.WriteLine("2. Sälj din bil");
            Console.WriteLine("3. Tilbaka till huudmenyn");


            var choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    säljare.VisaLagerStatus(lager);
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;
               
                case "2":
                    säljare.Säljbil(lager);
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;
               
                case "3":
                    tillbakaTillHuvudmeny = true;
                    break;
               
                default:
                    Console.WriteLine("Error, försök igen.");
                    break;
            }
        }
    }


    static void PersonalMeny()
    {
        Personal personal = new Personal("Admin");
        bool tillbakaTillHuvudmeny = false;


        while (!tillbakaTillHuvudmeny)
        {
            Console.Clear();
            Console.WriteLine("Meny för personal:");
            Console.WriteLine("1. Visa lagerstatus");
            Console.WriteLine("2. Uppdatera bilstatus (sätt pris och status)");
            Console.WriteLine("3. Visa försäljningshistorik");
            Console.WriteLine("4. Gå tillbaka till huvudmenyn");


            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    personal.VisaLagerStatus(Program.lager);
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;


                case "2":
                    personal.UppdateraBilStatus(Program.lager);
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;


                case "3":
                    personal.VisaFörsäljningshistorik();
                    Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;


                case "4":
                    tillbakaTillHuvudmeny = true;
                    break;


                default:
                    Console.WriteLine("Error, försök igen.");
                    break;
        }
    }
}
       
   
}
