#nullable disable
using BigBazar.Messages;
using BigBazar.Models;
using BigBazar.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BigBazar.ViewModels;

public partial class CatSelectionPageViewModel : BaseViewModel
{
    public CatSelectionPageViewModel(IDataService data, int forBoxId)
    {
        Title = "Select Category(ies)";
        dataService = data; 
        this.forBoxId = forBoxId;
        LoadDataAsync();
    }

    private readonly IDataService dataService;
    private readonly int forBoxId;

    [ObservableProperty]
    private List<CategoryForSelection> categoriesToSelect = [];

    private async void LoadDataAsync()
    {
        try
        {
            IsBusy = true;
            try
            {
                var list= (await dataService.GetCatsForBoxId(forBoxId)).Select(b=> b.IdCat).ToList();
                var cats = await dataService.GetCategoriesAsync();
                CategoriesToSelect = cats
                    .Select(c => new CategoryForSelection()
                    { Id = c.Id, Name = c.Name, IsSelected = list.Contains(c.Id), IsEnabled = !list.Contains(c.Id) })
                    .OrderBy(c => c.Name)
                    .ToList();
            }
            finally { IsBusy = false; }
        }catch (Exception ex)
        {
            await Logger.LogErrorAsync(AppLogger.LogLevel.Error, nameof(LoadDataAsync) + " " + ex.Message);
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Select()
    {
        // créer la liste des éléments sélectionnés
        var selectedCats = CategoriesToSelect
            .Where(c => c.IsSelected && c.IsEnabled)
            .Select(c => new Category() { Id = c.Id, Name = c.Name })
            .ToList();

        WeakReferenceMessenger.Default.Send(new CatSelectedMessage(selectedCats));

        await Shell.Current.GoToAsync("..");
    }
}
