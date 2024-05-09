using BigBazar.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBazar.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BigBazar.ViewModels
{
    public partial class AreaPageViewModel : BaseViewModel
    {
        private readonly IDataService dataService;
        private readonly IDialogService dialogService;
        public AreaPageViewModel(IDataService data, IDialogService dialog)
        {
            Title = "Storage Areas";
            dataService = data;
            dialogService = dialog;
            InitData();
        }

        [ObservableProperty]
        private ObservableCollection<AreaDisplayable> areas = [];

        private async void InitData()
        {
            IsBusy = true;
            try
            {
                Areas.Clear();
                SelectedArea = null;
                var data = await dataService.GetAreasAsync();
                foreach (var item in data)
                {
                    Areas.Add(new AreaDisplayable(item));
                }
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(AppLogger.LogLevel.Error, ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ObservableProperty]
        private AreaDisplayable? selectedArea;


        [RelayCommand]
        private async void DeleteSelectedArea()
        {
            if (SelectedArea == null) return;
            try
            {
                var usage = await dataService.GetCountUsageForAreaId(SelectedArea.Id);
                if (usage > 0)
                {
                    await dialogService.ShowMessage("Error", 
                        $"Cannot delete the area because it is used in {usage} box(es)", "Ok");
                    return;
                }
                await dataService.DeleteAreaAsync(SelectedArea.Id);
            } catch (Exception ex) 
            {
                await Logger.LogErrorAsync(AppLogger.LogLevel.Error, ex.Message);
                await dialogService.ShowMessage("Error", "Could not delete the area","Ok");
                return;
            }
            Areas.Remove(SelectedArea);
            SelectedArea = null;
        }

        [RelayCommand]
        private async void AddArea()
        {
            var s = await dialogService.InputTextDialogAsync("Add Area", "Enter the name of the new area","new area name");
            if (string.IsNullOrWhiteSpace(s)) return;
            s = s.Trim();
            var newArea = new Area {Name=s };
            try
            {
                newArea.Id = await dataService.AddAreaAsync(newArea);

                var added = false;
                for (var i = 0; i < Areas.Count; i++)
                {
                    if (String.CompareOrdinal(Areas[i].Name, newArea.Name) > 0)
                    {
                        Areas.Insert(i, new AreaDisplayable(newArea));
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    Areas.Add(new AreaDisplayable(newArea));
                }
                SelectedArea = new AreaDisplayable(newArea);


            } catch (Exception ex)
            {
                await Logger.LogErrorAsync(AppLogger.LogLevel.Error, ex.Message);
                await dialogService.ShowMessage("Error", "Could not add the area", "Ok");
            }
        }

        [RelayCommand]
        private async void ModifySelectedArea()
        {
            if (SelectedArea == null) return;
            var s = await dialogService.InputTextDialogAsync("Modify Area", "Enter area new name", "New area name",SelectedArea.Name);
            if (string.IsNullOrWhiteSpace(s)) return;
            s = s.Trim();
            SelectedArea.Name = s;
            try
            {
                await dataService.SaveAreaAsync(SelectedArea.ToDataArea());
            } catch (Exception ex)
            {
                await Logger.LogErrorAsync(AppLogger.LogLevel.Error, ex.Message);
                await dialogService.ShowMessage("Error", "Could not save the area", "Ok");
            }
        }

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        private void RefreshList()
        {
            InitData();
            IsRefreshing = false;
        }

    }
}
