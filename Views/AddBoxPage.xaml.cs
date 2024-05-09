using BigBazar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBazar.Services;

namespace BigBazar.Views;

public partial class AddBoxPage : BasePage
{
    public AddBoxPage(IViewModelFactory viewModelFactory)
    {
        InitializeComponent();
        BindingContext = viewModelFactory.Create<AddBoxPageViewModel>();
    }
    

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as AddBoxPageViewModel)?.InitData();
    }

  
}