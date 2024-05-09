#nullable disable
using BigBazar.Services;
using BigBazar.ViewModels;

namespace BigBazar.Views;

public partial class CatSelectionPage : BasePage
{
	public CatSelectionPage(int forBoxId)
	{
        InitializeComponent();
        var vm = new CatSelectionPageViewModel(BaseViewModel.GetServiceHelper<IDataService>(), forBoxId);
        BindingContext = vm;
    }
    
}