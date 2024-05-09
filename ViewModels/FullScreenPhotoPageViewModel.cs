#nullable disable
using BigBazar.Messages;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Reflection.Metadata.Ecma335;

namespace BigBazar.ViewModels;

public partial class FullScreenPhotoPageViewModel : BaseViewModel
{

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PhotoName))] 
    private string photoPath = string.Empty;

    public string PhotoName
    {
        get
        {
           var (nx, ny) = Utils.ImagePathParser.ExtractNumbersAsString(PhotoPath);
            return $"Box n° {nx} ";
        }
        set { }
    }

    [RelayCommand]
    private async void SharePhoto()
    {
        await Share.RequestAsync(new ShareFileRequest
        {
            Title = PhotoName,
            File = new ShareFile(PhotoPath)
        });
    }
}