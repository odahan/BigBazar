#nullable disable
using BigBazar.Services;
using BigBazar.ViewModels;

namespace BigBazar.Views;

public partial class CategoryPage : BasePage
{
	public CategoryPage()
	{
        InitializeComponent();
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        BindingContext = null;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModelFactory = BaseViewModel.GetServiceHelper<IViewModelFactory>();
        BindingContext = viewModelFactory.Create<CategoryPageViewModel>();
    }
}