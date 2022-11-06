using System.Data.SqlTypes;

namespace CaptrsCardGamePrototype.Models;

public struct ConnectionInfo
{
    public string getAll;
    public string get;
    public string post;
    public string put;
    public string delete;
}

public abstract class DbState : BaseInfoObj
{
    public HttpClient http { get; set; }
    public bool isLoaded { get; set; }
    public bool isLoading { get; set; }
    
    public ConnectionInfo connectionInfo { get; set; }
    
    public DateTime lastLoaded { get; set; }
    
    private TimeSpan TimeSinceLastLoaded() => lastLoaded.Subtract(DateTime.Now);
    public int SecondsSinceLastLoaded() => TimeSinceLastLoaded().Seconds;
    public int MinutesSinceLastLoaded() => TimeSinceLastLoaded().Minutes;

    public async Task GetAllFromDb()
    {
        isLoading = true;
        isLoaded = false;

        if (await GetAllDbSpecificItems())
        {
            isLoading = false;
            isLoaded = true;
            return;
        }
        
        isLoading = false;
        isLoaded = false;
    }

    protected abstract Task<bool> GetAllDbSpecificItems();
}

