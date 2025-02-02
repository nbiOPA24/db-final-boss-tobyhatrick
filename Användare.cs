public class Användare  // Basklass för att hantera grundläggande som namn och roll för varje användartyp
{
    public string Namn { get; set; }
    public string Roll { get; set; } // "Köpare, "Säljare, "Personal"


    public Användare(string namn, string roll)
    {
        Namn = namn;
        Roll = roll;
    }


}
