using System.Data.SqlTypes;

namespace CaptrsCardGamePrototype.Models;

public abstract class DbState : BaseInfoObj
{
    public HttpClient http { get; set; }
    public bool isLoaded { get; set; }
    public bool isLoading { get; set; }
    
    public string connectionStringBase { get; set; }
    public string connectionStringKey { get; set; }

    public string connectionString => $"{connectionStringBase}{connectionStringKey}";

    public DateTime lastLoaded { get; set; }
    
    private TimeSpan TimeSinceLastLoaded() => lastLoaded.Subtract(DateTime.Now);
    public int SecondsSinceLastLoaded() => TimeSinceLastLoaded().Seconds;
    public int MinutesSinceLastLoaded() => TimeSinceLastLoaded().Minutes;

    protected string PutByIdConnectionString(string putId)
    {
        return $"{connectionStringBase}/{putId}{connectionStringKey}";
    }
    
    protected string DeleteByIdConnectionString(string deleteId)
    {
        return $"{connectionStringBase}/{deleteId}{connectionStringKey}";
    }

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

    public abstract Task BuildNewAndSetToEditor();
    
    public abstract Task<bool> SubmitEditsToDbAsync();
}

