using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace CaptrsCardGamePrototype.Models; 

public class CardBaseObj : BaseInfoObj {

    public string icon { get; set; } = Icons.Material.Outlined.Info;
    public string cardType { get; set; }
    
}