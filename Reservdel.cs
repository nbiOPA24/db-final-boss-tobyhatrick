public class Reservdel
{
    public int Id { get; set; }
    public string Namn { get; set; }
    public decimal Pris { get; set; }
    public int BilId { get; set; }

    // Parameterlös konstruktor
    public Reservdel()
    {
        Namn = string.Empty; // Sätt ett standardvärde för Namn
    }

    public Reservdel(int id, string namn, decimal pris, int bilId)
    {
        Id = id;
        Namn = namn;
        Pris = pris;
        BilId = bilId;
    }
}