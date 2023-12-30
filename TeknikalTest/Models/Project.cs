using System.ComponentModel.DataAnnotations.Schema;

namespace TeknikalTest.Models;

[Table("tb_m_projects")]
public class Project : BaseEntity
{
    [Column("name", TypeName= "nvarchar(255)")]
    public string Name { get; set; }

    [Column("description", TypeName ="nvarchar(255)")]
    public string Description { get; set; }

    [Column("vendor_guid", TypeName ="CHAR(36)")]
    public Guid VendorGuid { get; set; }

    // Cardinality
    public Vendor? Vendor { get; set; }
}
