public class Användare
{
    protected readonly DatabaseRepository _dbRepository; // Skyddad så att underklasser kan använda den
    public string Namn { get; set; }
    public string Roll { get; set; }

    public Användare(string namn, string roll, DatabaseRepository dbRepository)
    {
        Namn = namn;
        Roll = roll;
        _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
    }
}