namespace CaptrsCardGamePrototype.Models; 

public class Pathogen: BaseInfoObj {
    
    public string? typeName { get; set; }
    public double? onset { get; set; }
    public double? duration { get; set; }
    
    public List<string>? acuteSymptoms { get; set; }
    public List<string>? reservoirDetails { get; set; }
}