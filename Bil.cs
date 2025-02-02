public class Bil
{
    public int Id { get; set; }
    public string Märke { get; set; }
    public string Modell { get; set; }
    public int Årsmodell { get; set; }
    public int Miltal { get; set; }
    public string Växellåda { get; set; }
    public decimal? Pris { get; set; }
    public string Status { get; set; } = "Tillgänglig";


    public Bil(int id, string märke, string modell, int årsmodell, int miltal, string växellåda, decimal? pris, string status)
    {
        Id = id;
        Märke = märke;
        Modell = modell;
        Årsmodell = årsmodell;
        Miltal = miltal;
        Växellåda = växellåda;
        Pris = pris;
        Status = status;
    }


    public void DisplayInfo()
    {
        Console.WriteLine($"(ID: {Id}) Status: {Status} - Pris: {Pris} {Märke} {Modell} - Miltal: {Miltal} Växellåda: {Växellåda} År: {Årsmodell}");
       
    }


}
