

using System.Net.Http.Json;

namespace CaptrsCardGamePrototype.Models;

public class PathogenDbState : DbState
{
    public List<Pathogen>? loadedPathogens { get; set; } = new();
    public List<Pathogen> dirtyPathogens { get; set; } = new();
    public List<Pathogen> selectedPathogens { get; set; } = new();

    public override async Task GetAllFromDb()
    {
        isLoading = true;
        isLoaded = false;
        loadedPathogens = await http.GetFromJsonAsync<List<Pathogen>>(connectionInfo.getAll);

        isLoading = false;
        isLoaded = true;
        if (loadedPathogens is null)
        {
            isLoaded = false;
        }
        
    }
    
        

    public PathogenDbState()
    {
        connectionInfo = new ConnectionInfo()
        {
            getAll =
                "https://pathogensapi.azurewebsites.net/api/pathogen?code=cdtNGBg2z0nHTUzTzEAxHqVuIsCyOnmZEULRaguOPvfYAzFus8dMCQ=="

        };
    }
}