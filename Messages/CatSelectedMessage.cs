#nullable disable
using BigBazar.Models;

namespace BigBazar.Messages
{
   public class CatSelectedMessage(List<Category> selectedCategories)
    {
        public List<Category> SelectedCategories { get; set; } = selectedCategories;
    }
}
