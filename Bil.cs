public class Bil
{
    public int Id { get; set; }
    public string Märke { get; set; }
    public string Modell { get; set; }
    public int Årsmodell { get; set; }
    public int Miltal { get; set; }
    public string Växellåda { get; set; }
    public decimal Pris { get; set; }
    public string Status { get; set; } = "Tillgänglig";
     

    // Parameterlös konstruktor (krävs av Dapper)
    public Bil() { }

    
    public Bil(string märke, string modell, int årsmodell, int miltal, string växellåda, decimal pris, string status = "Tillgänglig")
    {
        Märke = märke;
        Modell = modell;
        Årsmodell = årsmodell;
        Miltal = miltal;
        Växellåda = växellåda;
        Pris = pris;
        Status = status;
    }

    
}