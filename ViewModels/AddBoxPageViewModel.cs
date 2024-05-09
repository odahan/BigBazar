using BigBazar.Messages;
using BigBazar.Models;
using BigBazar.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BigBazar.ViewModels;

public partial class AddBoxPageViewModel : BaseViewModel
{
    private readonly IDataService dataService;
    private readonly IDialogService dialogService;

    // constructor with dataservice injection
    public AddBoxPageViewModel(IDataService dataService, IDialogService dialogService)
    {
        this.dataService = dataService;
        this.dialogService = dialogService;
        Title = "New Box";
    }

    [ObservableProperty]
    private int firstMissingBoxNumber;

    [ObservableProperty]
    private int greatestUsedBoxNumber;

    [ObservableProperty]
    private int chosenBoxNumber;

    [ObservableProperty]
    private bool firstMissingIsOk;

    public async void InitData()
    {
        IsBusy = true;
        FirstMissingIsOk = true;
        try
        {
            FirstMissingBoxNumber = await dataService.GetFirstMissingBoxNumbersAsync();
            GreatestUsedBoxNumber = await dataService.GetLastBoxNumberAsync();
            if (FirstMissingBoxNumber < 1) FirstMissingIsOk = false;
            ChosenBoxNumber = GreatestUsedBoxNumber + 1;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async void ReInitData()
    {
        InitData();
    }

    [RelayCommand]
    public void UseFirstMissingBoxNumber()
    {
        if (IsBusy) return;
        ChosenBoxNumber = FirstMissingBoxNumber;
    }

    [RelayCommand]
    public void UseGreatestUsedBoxNumber()
    {
        if (IsBusy) return;
        ChosenBoxNumber = GreatestUsedBoxNumber + 1;
    }

    [RelayCommand]
    public async Task Cancel()
    {
        if (IsBusy) return;
        // go back to main page
        await Shell.Current.GoToAsync("//MainPage");
    }


    [RelayCommand]
    public async Task ValidateNewBox()
    {
        if (IsBusy) return;
        var b = new Box
        {
            Number = ChosenBoxNumber
        };
        var boxId = await dataService.SaveBoxAsync(b);
        if (boxId < 1)
        {
            await dialogService.ShowMessage("Error", "Box number already exists or can't be created (see logs).", "Ok");
            return;
        }
        try
        {
            WeakReferenceMessenger.Default.Register<BoxPageCallerMessage>(this, (r, m) =>
            {
                if (m.ViewModelName == GetType().Name)
                {
                    InitData();
                    WeakReferenceMessenger.Default.Unregister<BoxPageCallerMessage>(this);
                }
            });
            await Shell.Current.GoToAsync($"BoxDetail?boxid={ChosenBoxNumber}&caller={GetType().Name}");
        }
        catch (Exception ex)
        {
            await Logger.LogErrorAsync(AppLogger.LogLevel.Critical, $"Error navigating to BoxDetail {ChosenBoxNumber} " + ex.Message);
            await dialogService.ShowMessage("Error", ex.Message, "Ok");
        }
    }
}
