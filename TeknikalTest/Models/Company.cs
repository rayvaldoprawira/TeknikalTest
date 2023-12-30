using System.ComponentModel.DataAnnotations.Schema;

namespace TeknikalTest.Models
{
    [Table("tb_m_company")]
    public class Company : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        [Column("company_email", TypeName = "nvarchar(255)")]
        public string Email { get; set; }

        [Column("address", TypeName = "nvarchar(100)")]
        public string Address { get; set; }

        [Column("account_guid", TypeName = "CHAR(36)")]
        public Guid AccountGuid { get; set; }

        // Cardinality
        public RegisterApprove? RegisterApprove { get; set; }
        public Account? Account { get; set; }
        public Vendor? Vendor { get; set; }


    }
}
