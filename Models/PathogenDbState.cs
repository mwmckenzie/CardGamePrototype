

using System.Net.Http.Json;

namespace CaptrsCardGamePrototype.Models;

public class PathogenDbState : DbState
{
    public List<Pathogen>? loadedPathogens { get; set; } = new();
    public List<Pathogen> dirtyPathogens { get; set; } = new();
    public List<Pathogen> selectedPathogens { get; set; } = new();

    protected override async Task<bool> GetAllDbSpecificItems()
    {
        loadedPathogens = await http.GetFromJsonAsync<List<Pathogen>>(connectionInfo.getAll);
        return loadedPathogens is not null;
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