using System.ComponentModel.DataAnnotations.Schema;

namespace TeknikalTest.Models;


[Table("tb_m_sys_admin")]
public class SysAdmin : BaseEntity
{
    [Column("name",TypeName ="nvarchar(255)")]
    public string Name { get; set; }

    [Column("account_guid", TypeName = "CHAR(36)")]
    public Guid AccountGuid { get; set; }

    // Cardinality
    public Account? Account { get; set; }
}
