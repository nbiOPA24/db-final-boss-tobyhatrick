public class Underhåll
{
    public int Id { get; set; }
    public int BilId { get; set; }
    public DateTime Datum { get; set; }
    public string Beskrivning { get; set; }

    public Underhåll(int id, int bilId, DateTime datum, string beskrivning)
    {
        Id = id;
        BilId = bilId;
        Datum = datum;
        Beskrivning = beskrivning;
    }
}