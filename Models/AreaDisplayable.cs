#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;

namespace BigBazar.Models;

public class AreaDisplayable : ObservableObject
{
    private int id;
    private string name;

    public int Id
    {
        get => id;
        set => SetProperty(ref id, value);
    }

    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    public AreaDisplayable(Area area)
    {
        Id = area.Id;
        Name = area.Name;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        AreaDisplayable other = (AreaDisplayable)obj;
        return Id == other.Id && Name == other.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

    public Area ToDataArea()
    {
        return new Area
        {
            Id = Id,
            Name = Name
        };
    }

}
