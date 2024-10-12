using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models;

[Table("ms_storage_location")]
public class StorageLocation
{
    [Key]
    [Column("location_id", TypeName = "varchar(10)")]
    public string LocationId { get; set; }

    [Column("location_name", TypeName = "varchar(100)")]
    public string LocationName { get; set; }
}
