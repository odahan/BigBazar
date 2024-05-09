using System.ComponentModel;
using System.Windows.Input;
using BigBazar.Services;
using BigBazar.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BigBazar.Views;

public partial class AboutPage : BasePage
{

    public AboutPage(IViewModelFactory viewModelFactory)
    {
        InitializeComponent();
        BindingContext = viewModelFactory.Create<AboutPageViewModel>(); ;
    }
}
