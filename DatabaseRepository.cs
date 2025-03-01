using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

public class DatabaseRepository
{
    private readonly string _connectionString;

    public DatabaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection SkapaAnslutning() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Bil>> HämtaSåldaBilarAsync()
    {
        using var db = SkapaAnslutning();
        var sql = @"
            SELECT b.*, f.Försäljningspris, f.FörsäljningsDatum AS FörsäljningsDatum
            FROM Bilar b
            INNER JOIN Försäljningar f ON b.Id = f.BilId
            WHERE b.Status = 'Såld'";
        return await db.QueryAsync<Bil>(sql);
    }

    public async Task<Bil> HämtaBilMedIdAsync(int id)
    {
        using var db = SkapaAnslutning();
        return await db.QueryFirstOrDefaultAsync<Bil>(
            "SELECT * FROM Bilar WHERE Id = @Id",
            new { Id = id }
        );
    }

    public async Task<IEnumerable<Bil>> HämtaAllaBilarAsync()
    {
        using var db = SkapaAnslutning();
        var sql = @"
            SELECT b.*, 
                r.Namn AS Reservdel, r.Pris AS ReservdelPris,
                u.Beskrivning AS Underhåll, u.Kostnad AS UnderhållKostnad
            FROM Bilar b
            LEFT JOIN Reservdelar r ON b.Id = r.BilId
            LEFT JOIN Underhåll u ON b.Id = u.BilId";
        return await db.QueryAsync<Bil>(sql);
    }

    public async Task LäggTillBilAsync(Bil bil)
    {
        using var db = SkapaAnslutning();
        await db.ExecuteAsync(
            "INSERT INTO Bilar (Märke, Modell, Årsmodell, Miltal, Växellåda, Pris, Status) " +
            "VALUES (@Märke, @Modell, @Årsmodell, @Miltal, @Växellåda, @Pris, @Status)",
            new
            {
                bil.Märke,
                bil.Modell,
                bil.Årsmodell,
                bil.Miltal,
                bil.Växellåda,
                bil.Pris,
                Status = "Tillgänglig" // Standardstatus
            }
        );
    }

    public async Task LäggTillFörsäljningAsync(int bilId, decimal försäljningspris)
    {
        using var db = SkapaAnslutning();
        var sql = "INSERT INTO Försäljningar (BilId, Försäljningspris) VALUES (@BilId, @Försäljningspris)";
        await db.ExecuteAsync(sql, new { BilId = bilId, Försäljningspris = försäljningspris });
    }

    public async Task UppdateraBilStatusAsync(int id, string status)
    {
        using var db = SkapaAnslutning();
        await db.ExecuteAsync(
            "UPDATE Bilar SET Status = @Status WHERE Id = @Id",
            new { Id = id, Status = status }
        );
    }

    public async Task<decimal> HämtaTotalFörsäljningAsync()
    {
        using var db = SkapaAnslutning();
        var sql = "SELECT COALESCE(SUM(Försäljningspris), 0) FROM Försäljningar";
        return await db.QueryFirstOrDefaultAsync<decimal>(sql);
    }
    // COALESCE i SQL-frågan för att returnera 0 istället för NULL om det inte finns några försäljningar.
    public async Task<decimal> HämtaGenomsnittligtPrisAsync()
    {
        using var db = SkapaAnslutning();
        var sql = "SELECT COALESCE(AVG(Försäljningspris), 0) FROM Försäljningar";
        return await db.QueryFirstOrDefaultAsync<decimal>(sql);
    }

    public async Task LäggTillReservdelAsync(int bilId, string namn, decimal pris)
    {
        using var db = SkapaAnslutning();
        await db.ExecuteAsync(
            "INSERT INTO Reservdelar (BilId, Namn, Pris) VALUES (@BilId, @Namn, @Pris)",
            new { BilId = bilId, Namn = namn, Pris = pris }
        );
    }

    public async Task<List<Reservdel>> HämtaAllaReservdelarAsync()
    {
        using var db = SkapaAnslutning();
        var sql = "SELECT Id, Namn, Pris, BilId FROM Reservdelar";
        return (await db.QueryAsync<Reservdel>(sql)).ToList();
    }
    
    public async Task LäggTillUnderhållAsync(int bilId, string beskrivning, decimal kostnad)
    {
        using var db = SkapaAnslutning();
        var sql = "INSERT INTO Underhåll (BilId, Beskrivning, Kostnad) VALUES (@BilId, @Beskrivning, @Kostnad)";
        await db.ExecuteAsync(sql, new { BilId = bilId, Beskrivning = beskrivning, Kostnad = kostnad });
    }

    public async Task<List<Underhåll>> HämtaUnderhållshistorikAsync(int bilId)
    {
        using var db = SkapaAnslutning();
        var sql = @"
            SELECT u.*, b.Märke, b.Modell
            FROM Underhåll u
            INNER JOIN Bilar b ON u.BilId = b.Id
            WHERE u.BilId = @BilId";
        return (await db.QueryAsync<Underhåll>(sql, new { BilId = bilId })).ToList();
    }

    

    
}
