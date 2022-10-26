namespace CaptrsCardGamePrototype.Helpers; 

public class TextSectionLookUps {


    private string _lookUpDictName = string.Empty;
    private Dictionary<string, LookUpDict> _lookUpDicts = new();
    public Dictionary<string, string> keyValueStore { get; set; } = new();
    public string lookUpDictName => _lookUpDictName;

    public string GetValue(string key) {
        if (string.IsNullOrWhiteSpace(key) || !keyValueStore.ContainsKey(key)) {
            return$"[MISSING VALUE (KEY: {key.Replace("#",string.Empty)})]";
        }
        return keyValueStore[key];
    }

    public void AddLookUpDict(LookUpDict lookUpDict) {
        _lookUpDicts.TryAdd(lookUpDict.name, lookUpDict);
    }

    public void SetLookUpDict(string dictName) {
        if (!_lookUpDicts.TryGetValue(dictName, out var dict)) return;
        
        keyValueStore = dict.keyValueStore;
        _lookUpDictName = dictName;
    }
}