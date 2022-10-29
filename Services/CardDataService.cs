using System.Net.Http.Json;
using CaptrsCardGamePrototype.Helpers;
using CaptrsCardGamePrototype.Models;

namespace CaptrsCardGamePrototype.Services; 

public class CardDataService {
    
    private HttpClient? _http;
    private bool _isInitialized;
    public bool loadComplete => _isInitialized;

    public TextSectionLookUps textSectionLookUps { get; set; } = new();
    public List<string> loadedTextSections { get; set; } = new();

    public List<CardBaseObj> cards { get; set; } = new();
    public CardBaseObj? editorCard { get; set; }

    public async Task Init(HttpClient http) {
        _http = http;
        if (_isInitialized) return;

        await LoadLookUpsAsync();

        OnInitializationComplete();
    }

    private void OnInitializationComplete() {
        _isInitialized = true;
    }

    private async Task LoadLookUpsAsync() {
        for (var i = 1; i < 5; i++) {
            var dict = await BuildLookUpDict($"data/lookups/reportCard01_{i:00}.json");
            textSectionLookUps.AddLookUpDict(new LookUpDict() {
                name = $"reportCard01_{i:00}", 
                keyValueStore = dict,
            });
        }
        
        await BuildLookUp(loadedTextSections, "data/testTextInput01.json");
    }
    
    private async Task BuildLookUp(List<string> listOut, string filename) {
        var list = await _http.GetFromJsonAsync<List<string>>(filename);
        if (list is null) {
            return;
        }
        listOut.AddRange(list);
    }

    private async Task<Dictionary<string, string>> BuildLookUpDict(string filepath) {
        var dict = new Dictionary<string, string>();
        var loadedDict = await _http.GetFromJsonAsync<Dictionary<string, string>>(filepath);
        if (loadedDict is null) {
            return null;
        }
        foreach (var (key, value) in loadedDict) {
            dict.Add(key, value);
        }
        return dict;
    }
}