#nullable disable
using SQLite;

namespace BigBazar.Models;

[Table("Boxes")]
public class Box
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public int Number { get; set; }

    public string Description { get; set; }

    public int? AreaCode { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.Now;

    
}