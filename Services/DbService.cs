using CaptrsCardGamePrototype.Models;

namespace CaptrsCardGamePrototype.Services;

public enum Databases
{
    AppCache,
    Pathogens,
    
}

public class DbService
{
    
    private List<DbState> _databases = new();

    public HttpClient http { get; set; }
    public PathogenDbState pathogenDb { get; set; } = new();

    public DbService(HttpClient httpClient)
    {
        http = httpClient;
        _databases.Add(pathogenDb);

        foreach (var database in _databases)
        {
            database.http = http;
        }
    }

    public async Task LoadDbAsync(Databases database, int minRefreshTimeLimit = 60)
    {
        switch (database)
        {
            case Databases.AppCache:
                break;
            case Databases.Pathogens:
                await TryLoadDbAsync(pathogenDb, minRefreshTimeLimit);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(database), database, null);
        }
    }

    private async Task TryLoadDbAsync(DbState dbState, int refreshTimeLimit)
    {
        if (dbState.isLoading || (dbState.isLoaded && dbState.SecondsSinceLastLoaded() < refreshTimeLimit))
        {
            return;
        }
        await  dbState.GetAllFromDb();
    }
}