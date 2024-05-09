#nullable disable
using BigBazar.Messages;
using BigBazar.Models;
using BigBazar.Services;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using BigBazar.Controls;
using BigBazar.Views;

namespace BigBazar.ViewModels;

public partial class BoxPageViewModel : BaseViewModel, IQueryAttributable
{

    public BoxPageViewModel(IDataService service, IDialogService dialogService)
    {
        Guard.IsNotNull(service);
        isCtorTime = true;
        Title = "Box";
        dataService = service;
        this.dialogService = dialogService;
        WeakReferenceMessenger.Default.Register<PastilleMessage>(this, OnPastilleMessageReceived);
        WeakReferenceMessenger.Default.Register<CatSelectedMessage>(this, OnCatSelectedMessageReceived);
        initStorage();
    }

    [RelayCommand]
    private async void ShellBackButton()
    {
        DisposeResources();
        WeakReferenceMessenger.Default.Send(new BoxPageCallerMessage { ViewModelName = caller });
        await Shell.Current.GoToAsync("..", true);
    }

    protected override void DisposeResources()
    {
        base.DisposeResources();
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    private async void initStorage()
    {
        StorageAreas = new ObservableCollection<Area>(await dataService.GetAreasAsync());
    }

    private List<int> getSelectedCatId()
    {
        // create a list of the selected cat ids
        var selectedCats = boxCats.Select(c => c.IdCat).ToList();
        return selectedCats;
    }

    [ObservableProperty]
    private Area? selectedArea;

    [ObservableProperty]
    private ObservableCollection<Area> storageAreas = [];

    private readonly IDialogService dialogService;
    private bool isCtorTime = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor("Description")]
    private Box currentBox;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private bool isModified;

    private readonly IDataService dataService;

    private List<BoxCatForDisplay> boxCats;

    partial void OnSelectedAreaChanged(Area oldValue, Area newValue)
    {
        if (newValue == null)
        {
            CurrentBox.AreaCode = null;
        }
        else
        {
            CurrentBox.AreaCode = newValue.Id;
        }
        IsModified = true;
    }

    [RelayCommand]
    private void EraseArea()
    {
        SelectedArea = null;
    }

    private string caller = string.Empty;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            if (!isCtorTime) return;
            isCtorTime = false;
            Guard.IsNotNull(query);
            caller = query["caller"].ToString();
            var id = Convert.ToInt32(query["boxid"]);
            CurrentBox = await dataService.GetBoxByIdAsync(id);
            boxCats = (await dataService.GetCatsForBoxId(id));
            Description = CurrentBox.Description;
            SelectedArea = StorageAreas.FirstOrDefault(a => a.Id == CurrentBox.AreaCode);
            IsModified = false;
            WeakReferenceMessenger.Default.Send(new BoxPageRebuildCatListMessage() { Tag = boxCats });
        }
        catch (Exception ex)
        {
            await Logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error applying query attributes: {ex.Message}");
        }
    }

    partial void OnDescriptionChanged(string value)
    {
        IsModified = true;
        CurrentBox.Description = value;
    }

    private bool working = false;
    private async void OnPastilleMessageReceived(object recipient, PastilleMessage message)
    {
        try
        {
            if ((message == null)
                 ||
                 (message.Kind == PayloadKind.None)
                 ||
                 (message.Payload == null) && (message.Kind == PayloadKind.Delete)
                ) return;
            if (working) return;
            working = true;
            try
            {
                var payload = message.Payload;
                var payloadIconKind = message.Kind;
                switch (payloadIconKind)
                {
                    case PayloadKind.Add:
                        // Add a new BoxCat to the list
                        // call to the catselection page. 
                        var catsel = new CatSelectionPage(CurrentBox.Id);
                        var nav = App.Current.MainPage.Navigation;
                        await nav.PushModalAsync(catsel);
                        //await Shell.Current.GoToAsync("CatSelection");
                        break;
                    case PayloadKind.Delete:
                        var modified = false;
                        for (var i = boxCats.Count - 1; i >= 0; i--)
                        {
                            if (payload != null && boxCats[i].IdBoxCat == (int)payload)
                            {
                                var boxCatId = (int)payload;
                                boxCats.RemoveAt(i);
                                await dataService.DeleteBoxCatAsync(boxCatId);
                                modified = true;
                                break;
                            }
                        }
                        if (modified)
                        {
                            boxCats = (await dataService.GetCatsForBoxId(CurrentBox.Id));
                            WeakReferenceMessenger.Default.Send(new BoxPageRebuildCatListMessage() { Tag = boxCats });
                        }
                        break;
                    default:
                        await Logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Unknown payload kind: {payloadIconKind}");
                        break;
                }
            }
            finally
            {
                working = false;
                message.Sender = null;
                message.Kind = PayloadKind.None;
                message.Payload = null;
                message.Dispose();
                message = null;
            }
        }
        catch (Exception ex)
        {
            await Logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error processing pastille message: {ex.Message}");
        }
    }

    [RelayCommand]
    public void Save()
    {
        IsBusy = true;
        try
        {
            try
            {
                Guard.IsNotNull(dataService);
                dataService.SaveBoxAsync(CurrentBox);
                WeakReferenceMessenger.Default.Send(new BoxListDataModifiedMessage { ViewModelName = nameof(BoxListPageViewModel), MessageData = new Tuple<Box, int>(CurrentBox, currentBox.Id) });
            }
            finally
            {
                IsBusy = false;
            }
            IsModified = false;
        }
        catch (Exception ex)
        {
            Logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error saving Box entity: {ex.Message}");
            IsModified = false;
        }
    }

    [RelayCommand]
    private async void DisplayBoxPhotos()
    {
        await Shell.Current.GoToAsync($"detail/Gallery?PhotoNumber={CurrentBox.Number}", true);
    }

    [RelayCommand]
    private async void DeleteCurrentBox()
    {
        var r =
            await dialogService.ShowMessageWithBooleanResponse("Delete Box",
                                                              "Are you sure you want to delete this box?",
                                                             "Yes", "No");
        if (!r) return;
        IsBusy = true;
        try
        {
            var result = await dataService.DeleteBoxByBoxNumber(CurrentBox.Number);
            if (result)
            {
                WeakReferenceMessenger.Default.Send(new BoxListDataModifiedMessage { ViewModelName = nameof(BoxListPageViewModel), MessageData = new Tuple<Box, int>(CurrentBox, CurrentBox.Id) });
                CurrentBox = new Box() { Description = "THIS BOX HAS BEEN DELETED - Select a new one" };
                await Shell.Current.GoToAsync("//BoxList");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddPhoto()
    {
        try
        {
            var photoName = await dataService.AddPhotoForBoxNumberAsync(CurrentBox.Number);
            await Logger.LogErrorAsync(AppLogger.LogLevel.Information, $"Photo added for Box {CurrentBox.Number} with name {photoName}");
        }
        catch (Exception ex)
        {
            await Logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error adding photo for Box {CurrentBox.Number}: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task PickPhoto()
    {
        var photoName = await dataService.AddPickPhotoForBoxNumberAsync(CurrentBox.Id);
        await Logger.LogErrorAsync(AppLogger.LogLevel.Information, $"Photo added for Box {CurrentBox.Number} with name {photoName}");
    }

    private async void OnCatSelectedMessageReceived(object recipient, CatSelectedMessage message)
    {
        Guard.IsNotNull(message);
        IsBusy = true;
        try
        {
            var selectedCats = message.SelectedCategories;
            foreach (var cat in selectedCats)
            {
                var b = boxCats.Any(c => c.IdBox == currentBox.Id && c.IdCat == cat.Id);
                if (b) continue;
                var bxc = new BoxCat() { CatId = cat.Id, BoxId = CurrentBox.Id };
                var id = await dataService.SaveBoxCatAsync(bxc);
                var bcf = new BoxCatForDisplay(id, cat.Id, CurrentBox.Id, cat.Name);

                boxCats.Add(bcf);
            }
        }
        finally
        {
            IsBusy = false;
        }
        // send a message to rebuild the visual list
        // sort boxcats by catname   
        boxCats = boxCats.OrderBy(c => c.CatName).ToList();

        WeakReferenceMessenger.Default.Send(new BoxPageRebuildCatListMessage() { Tag = boxCats });
    }
}