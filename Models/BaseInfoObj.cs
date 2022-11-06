namespace CaptrsCardGamePrototype.Models; 

public class BaseInfoObj : ICloneable, IEquatable<BaseInfoObj> {
    
    public bool isValid { get; set; } 
    public string? id { get; set; } = Guid.NewGuid().ToString();
    public string? name { get; set; }

    private string? _displayName;
    public string? displayName {
        get => _displayName ?? name;
        set => _displayName = value;
    }
    public string? url { get; set; }


    public virtual object Clone()
    {
        return new BaseInfoObj()
        {
            isValid = isValid,
            id = id,
            name = name,
            _displayName = _displayName,
            displayName = displayName,
            url = url
        };
    }

    public bool Equals(BaseInfoObj other)
    {
        return _displayName == other._displayName && isValid == other.isValid && id == other.id && name == other.name && url == other.url;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((BaseInfoObj)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_displayName, isValid, id, name, url);
    }
}