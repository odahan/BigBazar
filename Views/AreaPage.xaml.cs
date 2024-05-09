using BigBazar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBazar.Services;


namespace BigBazar.Views;

public partial class AreaPage : BasePage
{
    public AreaPage(IViewModelFactory viewModelFactory)
    {
        InitializeComponent();
        BindingContext = viewModelFactory.Create<AreaPageViewModel>();
    }
    
}