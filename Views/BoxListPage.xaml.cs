#nullable disable
using BigBazar.Messages;
using BigBazar.Services;
using BigBazar.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;

namespace BigBazar.Views;

public partial class BoxListPage : BasePage
{
    BoxListPageViewModel viewModel;
    public BoxListPage(IViewModelFactory viewModelFactory)
    {
        InitializeComponent();
        this.viewModel = viewModelFactory.Create<BoxListPageViewModel>();
        BindingContext = viewModel;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.InitPage();
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        searchBar.Text = "";
    }
}