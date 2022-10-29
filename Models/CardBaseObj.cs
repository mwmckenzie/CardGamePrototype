using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace CaptrsCardGamePrototype.Models; 

public class CardBaseObj : BaseInfoObj {
    
    [Display(Name="Icon")] 
    public string icon { get; set; } = Icons.Material.Outlined.Info;

    [Display(Name = "Card Type")] 
    public string cardType { get; set; } = string.Empty;
    
    [Display(Name="Card Title")]
    public string cardTitle { get; set; } = string.Empty;

    [Display(Name = "Text Content Sections")]
    public List<string> textContentSections { get; set; } = new();
    
    public bool isSpecialText { get; set; }

    public override string ToString() {
        var typeText = string.IsNullOrWhiteSpace(cardType) ? "Unknown Type" : cardType;
        var titleText = string.IsNullOrWhiteSpace(cardTitle) ? "Unknown Title" : cardTitle;
        return$"{typeText}: {titleText}";
    }
}