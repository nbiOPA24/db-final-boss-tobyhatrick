public class Underhåll
{
    public int Id { get; set; }
    public string Beskrivning { get; set; }
    public DateTime Datum { get; set; }
    public decimal Kostnad { get; set; }

    // Parameterlös konstruktor (krävs av Dapper)
    public Underhåll() { }

    // Konstruktor för att skapa ett nytt Underhåll-objekt
    public Underhåll(string beskrivning, DateTime datum, decimal kostnad)
    {
        Id = Id;
        Beskrivning = beskrivning;
        Datum = datum;
        Kostnad = kostnad;
    }
}