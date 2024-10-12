using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models;

[Table("ms_user")]
public class User
{
    [Key]
    [Column("user_id", TypeName = "bigint")]
    public long UserId { get; set; }

    [Column("user_name", TypeName = "varchar(20)")]
    public string UserName { get; set; }

    [Column("password", TypeName = "varchar(50)")]
    public string Password { get; set; }

    [Column("is_active", TypeName = "bit")]
    public bool IsActive { get; set; }
}
