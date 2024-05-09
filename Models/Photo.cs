#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;

namespace BigBazar.Models;

public partial class Photo : ObservableObject
{
    public Photo()
    {
    }

    public Photo(string imagePath) : this()
    {
        ImagePath = imagePath;
        DisplayName = ExtractDisplayName(imagePath);
        BoxNumber = ExtractBoxNumber(imagePath);
    }

    [ObservableProperty]
    private string imagePath;

    [ObservableProperty]
    private string displayName;

    [ObservableProperty]
    private int boxNumber;

    public static string ExtractDisplayName(string imagePath)
    {
        var (nx, ny) = Utils.ImagePathParser.ExtractNumbersAsString(imagePath);
        return $"Box {nx} - {ny}";
    }

    public static int ExtractBoxNumber(string imagePath)
    {
        var (nx, ny) = Utils.ImagePathParser.ExtractNumbersAsInt(imagePath);
        return nx;
    }

}