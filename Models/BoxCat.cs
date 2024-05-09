#nullable disable

namespace BigBazar.Models
{
    using SQLite;

    [Table("BoxCats")]
    public class BoxCat
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int BoxId { get; set; }

        public int CatId { get; set; }
    }

}
