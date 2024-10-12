using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

[Table("ms_user")]
public class UserModel
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
