#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBazar.Models
{
    public partial class BoxCatForDisplay : ObservableObject
    {
        private int idBoxCat;
        public int IdBoxCat
        {
            get => idBoxCat;
            set => SetProperty(ref idBoxCat, value);
        }

        private int idCat;
        public int IdCat
        {
            get => idCat;
            set => SetProperty(ref idCat, value);
        }

        private int idBox;
        public int IdBox
        {
            get => idBox;
            set => SetProperty(ref idBox, value);
        }

        private string catName;
        public string CatName
        {
            get => catName;
            set => SetProperty(ref catName, value);
        }

        public BoxCatForDisplay(int idBoxCat, int idCat, int idBox, string catName)
        {
            IdBoxCat = idBoxCat;
            IdCat = idCat;
            IdBox = idBox;
            CatName = catName;
        }

    }
}
