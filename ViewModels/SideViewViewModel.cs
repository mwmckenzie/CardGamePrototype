using MudBlazor;

namespace CaptrsCardGamePrototype.ViewModels; 

public class SideViewViewModel {
    
    public bool open = false;
    public Anchor anchor = Anchor.Right;
    public string width = "40%";
    public string height = "100%";

    public void DisplaySideView() {
        open = true;
    }
}