using System.ComponentModel.DataAnnotations.Schema;
using TeknikalTest.Utilities.Enums;

namespace TeknikalTest.Models;

[Table("tb_m_register_approves")]
public class RegisterApprove : BaseEntity
{
    [Column("company_guid", TypeName ="CHAR(36)")]
    public Guid CompanyGuid { get; set; }

    [Column("is_valid")]    
    public bool IsValid { get; set; }

    [Column("approve_image", TypeName = "nvarchar(255)")]
    public string? CompanyImage { get; set; }

    [Column("status_Approve")]
    public StatusApprove StatusApprove{ get; set; }


    //Cardinality
    public Company? Company { get; set; }

}
