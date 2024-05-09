using BigBazar.Services;
using BigBazar.ViewModels;

namespace BigBazar.Views
{
    public partial class MainPage : BasePage
    {
        public MainPage(IViewModelFactory viewModelFactory)
        {
            InitializeComponent();
            BindingContext = viewModelFactory.Create<MainPageViewModel>(); ;
        }
        


    }

}
