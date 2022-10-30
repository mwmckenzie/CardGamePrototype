using System.Net.Http.Json;
using CaptrsCardGamePrototype.Models;

namespace CaptrsCardGamePrototype.Services; 

public class SeirDataService {
    
    private HttpClient? _http;
    private bool _isInitialized;
    public bool loadComplete => _isInitialized;

    public string seirParamsFilepath { get; set; } = "data/SeirModelParams_PSA Game_Test01_2022-09-19-1836.json";

    public SeirParams seirParams { get; set; } = new();
    public List<LabeledDoubles> labeledSeirParams { get; set; } = new();

    public async Task Init(HttpClient http) {
        _http = http;
        if (_isInitialized) return;

        await LoadSeirParamsAsync();
        OnInitializationComplete();
    }

    private void OnInitializationComplete() {
        _isInitialized = true;
    }

    private async Task LoadSeirParamsAsync() {
        if (_http is null) {
            return;
        }
        var loadedParams = await _http.GetFromJsonAsync<SeirParams>(seirParamsFilepath);
        if (loadedParams is null) {
            return;
        }
        
        seirParams = loadedParams;
        foreach (var (key, value) in seirParams.labelsAndValues()) {
            labeledSeirParams.Add(new LabeledDoubles(){ label = key, value = value });
        }
    }

    
}