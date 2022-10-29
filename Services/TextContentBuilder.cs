using System.Diagnostics;
using System.Text.Encodings.Web;
using CaptrsCardGamePrototype.Helpers;

namespace CaptrsCardGamePrototype.Services; 

public class TextContentBuilder {

    public TextSectionLookUps textLookUps { get; set; }

    public string BuildTextContent(string text) {
        
        while (true) {
            var first = text.IndexOf("#", StringComparison.OrdinalIgnoreCase);
            var last = text.IndexOf("#", first + 1, StringComparison.OrdinalIgnoreCase);
            
            if (first < 0 || last < 0 || last <= first) return text;
            
            var key = text[first..(last + 1)];
            var value = textLookUps.GetValue(key);

            text = text.Replace(key, WrapTextInDiv(value));
        }
    }

    private string WrapTextInMud(string text) {
        return
            $"<MudElement Color=\"Color.Secondary\" Class=\"ma-0\" Style=\"font-weight:bold;\">{text}</MudElement>";
    }
    
    private string WrapTextInDiv(string text) {
        return
            $"<span style=\"color: rebeccapurple;font-weight: bold\">{text}</span>";
    }
}