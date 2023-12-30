using System.ComponentModel.DataAnnotations.Schema;

namespace TeknikalTest.Models;


[Table("tb_m_accounts")]
public class Account : BaseEntity
{
    [Column("email", TypeName = "varchar(100)")]
    public string Email { get; set; }

    [Column("password", TypeName = "nvarchar(255)")]
    public string Password { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("token")]
    public int? Token { get; set; }
    [Column("token_is_used")]
    public bool? TokenIsUsed { get; set; }

    [Column("token_expiration")]
    public DateTime? TokenExpiration { get; set; }
    [NotMapped]
    public string ConfirmPassword { get; set; }

    // Cardinality

    public ICollection<AccountRole>? AccountRoles { get; set; }
    public Company? Company { get; set; }
    public SysAdmin? SysAdmin { get; set; }
}
