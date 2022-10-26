namespace CaptrsCardGamePrototype.Models; 

public class BaseInfoObj {
    
    public bool isValid { get; set; }
    public string? id { get; set; }
    public string? name { get; set; }

    private string? _displayName;
    public string? displayName {
        get => _displayName ?? name;
        set => _displayName = value;
    }
    public string? url { get; set; }
    
    
}