using System.ComponentModel.DataAnnotations.Schema;

namespace TeknikalTest.Models;

[Table("tb_m_account_roles")]
public class AccountRole : BaseEntity 
{
    [Column("account_guid", TypeName ="CHAR(36)")]
    public Guid AccountGuid { get; set; }

    [Column("role_guid", TypeName ="CHAR(36)")]
    public Guid RoleGuid { get; set; }

    // Cardinality

    public Role? Role { get; set; }

    public Account? Account { get; set; }
}
