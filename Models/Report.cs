namespace CaptrsCardGamePrototype.Models; 

public class Report : CardBaseObj {

    public List<string> textSections = new();

    public Report() {
        cardType = "Report";
    }
}