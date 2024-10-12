using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

[Table("ms_storage_location")]
public class StorageLocationModel
{
    [Key]
    [Column("location_id", TypeName = "varchar(10)")]
    public string LocationId { get; set; }

    [Column("location_name", TypeName = "varchar(100)")]
    public string LocationName { get; set; }
}
