public class Försäljning
{
    public int Id { get; set; }
    public int BilId { get; set; }
    public int KöpareId { get; set; }
    public decimal Försäljningspris { get; set; }
    public DateTime Försäljningsdatum { get; set; }
}