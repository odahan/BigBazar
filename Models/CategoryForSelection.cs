#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;

namespace BigBazar.Models
{
        public partial class CategoryForSelection : Category
    {
        [ObservableProperty]
        private bool isSelected = false;

        [ObservableProperty]
        private bool isEnabled = true;
    }
}
