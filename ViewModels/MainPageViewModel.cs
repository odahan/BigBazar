#nullable disable
using BigBazar.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BigBazar.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly IDataService dataService;
    private readonly IDialogService dialogService;

    // constructor
    public MainPageViewModel(IDataService data, IDialogService dialog)
    {
        dataService = data;
        dialogService = dialog;
        Title = "Big Bazar";
        ShowSettings = false;
    }

    [ObservableProperty]
    private bool showSettings = false;

    [RelayCommand]
    private async Task About()
    {
        await Shell.Current.GoToAsync("//About");
    }

    [RelayCommand]
    private async Task AddBox()
    {
        await Shell.Current.GoToAsync("//NewBox"); 
    }

    [RelayCommand]
    private async Task BoxList()
    {
        await Shell.Current.GoToAsync("//BoxList"); 
    }

    [RelayCommand]
    private async Task Cats()
    {
        await Shell.Current.GoToAsync("//Categories");
    }

    [RelayCommand]
    private async Task ShowGallery()
    {
        await Shell.Current.GoToAsync("//Gallery");
    }


    [RelayCommand]
    private async Task NewTestDb()
    {
        await dataService.ResetDatabaseAndImagesAsync();
        await dataService.PopulateDatabaseAndImagesWithTestDataAsync();
    }

    [RelayCommand]
    private async Task ResetDb()
    {
        await dataService.ResetDatabaseAndImagesAsync();
    }

    [RelayCommand]
    private void ShowHideSettings()
    {
        ShowSettings = !ShowSettings;
    }

    [RelayCommand]
    private async Task SaveDatabase()
    {
        await dataService.BackupDatabaseAsync();
    }

    [RelayCommand]
    private async Task RestoreDatabase()
    {
        await dataService.RestoreDatabaseAsync();
    }

    [RelayCommand]
    private async Task SavePhotos()
    {
        await dataService.ExportAndSharePhotosAsync();
    }

    [RelayCommand]
    private async Task RestorePhotos()
    {
        await dataService.RestorePhotosAsync();
    }

    [RelayCommand]
    private async Task ShowPhotosStats()
    {
        var (photoCount, totalSize) = await dataService.GetPhotosStatsKbAsync();
        await dialogService.ShowMessage("Photos Stats", $"Photos count: {photoCount}\nTotal size: {totalSize / 1024.0:F2} Megabytes", "OK");
    }

    [RelayCommand]
    private async Task ExportTextData()
    {
        await dataService.ExportDataAsync();
    }
}