using BigBazar.Services;
using BigBazar.ViewModels;

namespace BigBazar.Views;

public partial class LogPage : BasePage
{
	public LogPage(IViewModelFactory viewModelFactory)
	{
        InitializeComponent();
		BindingContext = viewModelFactory.Create<LogPageViewModel>(); ;
	}
    
}