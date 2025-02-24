public class Reservdel
{
    public int Id { get; set; }
    public string Namn { get; set; }
    public int LagerAntal { get; set; }
    public decimal Pris { get; set; }

    public Reservdel(int id, string namn, int lagerAntal, decimal pris)
    {
        Id = id;
        Namn = namn;
        LagerAntal = lagerAntal;
        Pris = pris;
    }
}