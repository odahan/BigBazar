#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBazar.Models;
using BigBazar.Services;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BigBazar.ViewModels
{
    public partial class CategoryPageViewModel : BaseViewModel
    {
        private readonly IDataService dataService;
        private readonly IDialogService dialogService;
        public CategoryPageViewModel(IDataService data, IDialogService dialog)
        {
            Title = "Categoryies";
            dataService = data;
            dialogService = dialog;
            LoadDataAsync();
        }

        [ObservableProperty]
        private ObservableCollection<Category> categories = [];

        [ObservableProperty]
        private Category selectedCategory;


        private async void LoadDataAsync()
        {
            IsBusy = true;
            try
            {
                Categories = new ObservableCollection<Category>(await dataService.GetCategoriesAsync());
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async void RefreshList()
        {
            LoadDataAsync();
        }

        [RelayCommand]
        private async Task ValidateChange()
        {
            if (SelectedCategory == null) return;
            await dataService.SaveCategoryAsync(SelectedCategory);
        }

        [RelayCommand]
        private async Task AddCategory()
        {
            var newCategory = new Category();
            var s = await dialogService.InputTextDialogAsync("Add", "New category", "category name", "");
            if (string.IsNullOrEmpty(s)) return;
            newCategory.Name = s;
            var i = await dataService.SaveCategoryAsync(newCategory);
            if (i < 0) return;
            Categories.Add(newCategory);
            SelectedCategory = newCategory;
        }

        [RelayCommand]
        private async Task DeleteCategory()
        {
            if (SelectedCategory == null) return;
            var number = await dataService.HowMuchTimesCategoryIsUsed(SelectedCategory.Id);
            var sm = number > 0 ? "really " : "";
            if (number > 0)
            {
                var r1 = await dialogService.ShowMessageWithBooleanResponse("In Use!", $"This category is used in boxes {number} time(s). Do you still want to delete it ?.", "Yes", "No"); ;
                if (!r1) return;
            }

            var r = await dialogService.ShowMessageWithBooleanResponse("Delete", $"Are you {sm}sure you want to delete this category?", "Yes", "No");
            if (r)
            {
                await dataService.DeleteCategoryAsync(SelectedCategory.Id);
                Categories.Remove(SelectedCategory);
            }
        }

        
        [RelayCommand]
        private async Task TappedItem(Category category)
        {
            if (category == null) return;
            if (SelectedCategory==null || category.Id != SelectedCategory.Id)
            {
                SelectedCategory = category;
                return;
            }
            SelectedCategory = category;
            var s = await dialogService.InputTextDialogAsync("Edit", "Modify category", "", SelectedCategory.Name);
            if (string.IsNullOrEmpty(s)) return;
            SelectedCategory.Name = s;
            await dataService.SaveCategoryAsync(SelectedCategory);
        }
    }
}





