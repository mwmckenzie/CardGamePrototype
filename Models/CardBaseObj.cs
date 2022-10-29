using MudBlazor;
using System.ComponentModel.DataAnnotations;
using CaptrsCardGamePrototype.Interfaces;

namespace CaptrsCardGamePrototype.Models; 

public class CardBaseObj : BaseInfoObj, ICardDisplay {

    [Display(Name="ID")]
    public string? id { get; set; }
    
    [Display(Name="Icon")] 
    public string icon { get; set; } = Icons.Material.Outlined.Info;
    
    [Display(Name="Card Type")]
    public string cardType { get; set; }
    
    [Display(Name="Card Title")]
    public string cardTitle { get; set; }
    
    [Display(Name="Text Content Sections")]
    public List<string> textContentSections { get; set; }
    

}