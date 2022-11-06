

using System.Net.Http.Json;

namespace CaptrsCardGamePrototype.Models;

public class PathogenDbState : DbState
{
    public List<Pathogen>? loadedPathogens { get; set; } = new();
    public List<Pathogen> dirtyPathogens { get; set; } = new();
    public List<Pathogen> selectedPathogens { get; set; } = new();
    
    public Pathogen? pathogenSetForEditing { get; set; }
    
    public PathogenDbState()
    {
        connectionStringBase = "https://pathogensapi.azurewebsites.net/api/pathogen";
        connectionStringKey = "?code=captrs";
        // "https://pathogensapi.azurewebsites.net/api/pathogen?code=cdtNGBg2z0nHTUzTzEAxHqVuIsCyOnmZEULRaguOPvfYAzFus8dMCQ=="
    }

    protected override async Task<bool> GetAllDbSpecificItems()
    {
        loadedPathogens = await http.GetFromJsonAsync<List<Pathogen>>(connectionString);
        return loadedPathogens is not null;
    }

    public override Task BuildNewAndSetToEditor()
    {
        pathogenSetForEditing = new()
        {
            isValid = true,
            acuteSymptoms = new List<string>(),
            reservoirDetails = new List<string>()
        };
        return Task.CompletedTask;
    }

    public override async Task<bool> SubmitEditsToDbAsync()
    {
        if (pathogenSetForEditing is null)
            return false;

        var editedId = pathogenSetForEditing.id;
        if (loadedPathogens is not null && loadedPathogens.Any(x => x.id == editedId))
        {
            var putResponse = 
                await http.PutAsJsonAsync(PutByIdConnectionString(editedId), pathogenSetForEditing);
            return putResponse.IsSuccessStatusCode;
        }
        
        var postResponse = await http.PostAsJsonAsync(connectionString, pathogenSetForEditing);
        return postResponse.IsSuccessStatusCode;
    }


    
}