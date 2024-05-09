#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBazar.Controls
{
    public class SmartSearchBar : SearchBar
    {
        public SmartSearchBar()
        {
            var bnd = new Binding(nameof(this.Text), source: this);
            this.SetBinding(SmartSearchBar.SearchCommandParameterProperty, bnd);
        }

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            if (this.SearchCommand?.CanExecute(newValue) == true)
                this.SearchCommand?.Execute(newValue);
            base.OnTextChanged(oldValue, newValue);
        }
    }
}
