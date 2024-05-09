using BigBazar.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BigBazar.ViewModels
{
    public partial class AboutPageViewModel : BaseViewModel
    {
        public AboutPageViewModel(IVersionService version)
        {
            Title = version.Title;
            Version =  version.Version;
            Description = version.Description;
            Company = version.Company;
            Product = version.Product;
            Copyright = version.Copyright;
        }

        [ObservableProperty]
        private string version;

        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private string company;
        [ObservableProperty]
        private string product;
        [ObservableProperty]
        private string copyright;

        public ICommand OpenWebsiteCommand { get; } = new RelayCommand<string>(OpenWebsite);

        private static async void OpenWebsite(string? url)
        {
            _ = await Browser.OpenAsync(url ?? "https://www.e-naxos.com/blog");

        }


    }
}
