#nullable disable
using System.Collections;
using System.ComponentModel;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using BigBazar.Messages;
using BigBazar.Models;
using BigBazar.Services;
using BigBazar.Utils;
using BigBazar.Views;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Xaml.Internals;

namespace BigBazar.ViewModels;

public partial class BoxListPageViewModel : BaseViewModel
{
    readonly IDataService dataService;

    public BoxListPageViewModel(IDataService service)
    {
        Guard.IsNotNull(service);
        Title = "Box List";
        dataService = service;
    }

    public void InitPage()
    {
        setPlaceHolder();
        LoadDataAsync();
        LoadCategoriesAsync();
        LoadAreasAsync();
        CurrentSearchMode = BoxSearchMode.Description;
        WeakReferenceMessenger.Default.Register<BoxListDataModifiedMessage>(this, (recipient, message) =>
        {
            // var tuple = (Tuple<Box, int>)message.MessageData;
            if (message.ViewModelName == nameof(BoxListPageViewModel))
                LoadDataAsync();
        });
    }

    protected override void DisposeResources()
    {
        base.DisposeResources();
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    [ObservableProperty]
    private IEnumerable searchModes = Enum.GetValues(typeof(BoxSearchMode));

    private async void LoadDataAsync()
    {
        SearchResults = await dataService.GetBoxesAsync();
    }

    private async void LoadCategoriesAsync()
    {
        Categories = await dataService.GetCategoriesAsync();
    }

    private async void LoadAreasAsync()
    {
        Areas = await dataService.GetAreasAsync();
    }

    [ObservableProperty]
    private List<Area> areas;

    [ObservableProperty]
    private Area? selectedArea;


    [ObservableProperty]
    private List<Category> categories;

    [ObservableProperty]
    private Category? selectedCategory;


    private CancellationTokenSource cts;

    [RelayCommand]
    private async Task PerformSearch(string text)
    {
        if (cts != null) await cts.CancelAsync();
        cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(500, cts.Token); // Wait for the user to stop typing

            if ((string.IsNullOrWhiteSpace(text) && IsDescriptionSearchMode)
                || (SelectedCategory == null && IsCategorySearchMode)
                || (SelectedArea == null && IsAreaSearchMode))
            {
                SearchResults = await dataService.GetBoxesAsync();
            }
            else
            {
                IsBusy = true;
                try
                {
                    switch (CurrentSearchMode)
                    {
                        case BoxSearchMode.Area:
                            SearchResults = await dataService.SearchBoxesAreaAsync(SelectedArea?.Name);
                            break;
                        case BoxSearchMode.Category:
                            SearchResults = await dataService.SearchBoxesCatAsync(SelectedCategory?.Name);
                            break;
                        case BoxSearchMode.Description:
                            SearchResults = await dataService.SearchBoxesDescAsync(text);
                            break;
                    }
                }

                finally
                { IsBusy = false; }
            }
        }
        catch (TaskCanceledException)
        {
            // Ignore this exception 
        }
    }

    [RelayCommand]
    private async Task ItemTapped(Box box)
    {
        Guard.IsNotNull(box);
        await Shell.Current.GoToAsync($"BoxDetail?boxid={box.Id}&caller={GetType().Name}");
    }

    [ObservableProperty]
    private List<Box> searchResults;

    [ObservableProperty]
    private string searchPlaceHolder;


    [ObservableProperty]
    private BoxSearchMode currentSearchMode;

    partial void OnCurrentSearchModeChanged(BoxSearchMode oldValue, BoxSearchMode newValue)
    {
        if (newValue == BoxSearchMode.None)
        {
            CurrentSearchMode = BoxSearchMode.Description;
            return;
        }
        if (newValue == oldValue) return;
        SelectedCategory = null;
        SelectedArea = null;
        LoadDataAsync();
        setPlaceHolder();
        switch (newValue)
        {
            case BoxSearchMode.Description:
                IsDescriptionSearchMode = true;
                IsCategorySearchMode = false;
                IsAreaSearchMode = false;
                break;
            case BoxSearchMode.Category:
                IsDescriptionSearchMode = false;
                IsCategorySearchMode = true;
                IsAreaSearchMode = false;
                break;
            case BoxSearchMode.Area:
                IsDescriptionSearchMode = false;
                IsCategorySearchMode = false;
                IsAreaSearchMode = true;
                break;
        }

    }

    [ObservableProperty]
    private bool isDescriptionSearchMode;

    [ObservableProperty]
    private bool isAreaSearchMode;

    [ObservableProperty]
    private bool isCategorySearchMode;


    private void setPlaceHolder()
    {
        if (IsCategorySearchMode)
        { SearchPlaceHolder = "Search by Category"; return; }
        if (IsAreaSearchMode)
        { SearchPlaceHolder = "Search by Area"; return; }
        if (IsDescriptionSearchMode)
        { SearchPlaceHolder = "Search by Description"; return; }
    }

    private async Task ShareTextAsync(string text)
    {
        IsBusy = true;
        try
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "BigBazar"
            });
        }
        catch (Exception ex)
        {
            var nomMethode = MethodBase.GetCurrentMethod()?.Name;
            await Logger.LogErrorAsync(AppLogger.LogLevel.Error, nomMethode + " : " + ex.Message);
        }
        finally { IsBusy = false; }
    }


    [RelayCommand]
    private async Task ExportData()
    {
        IsBusy = true;
        try
        {
            var text = await dataService.ExportDataAsync();
            await ShareTextAsync(text);
        }
        catch (Exception ex)
        {
            var nomMethode = MethodBase.GetCurrentMethod()?.Name;
            await Logger.LogErrorAsync(AppLogger.LogLevel.Error, nomMethode + " : " + ex.Message);
        }
        finally { IsBusy = false; }
    }

}