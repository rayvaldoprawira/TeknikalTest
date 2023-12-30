using System.ComponentModel.DataAnnotations.Schema;

namespace TeknikalTest.Models
{
    [Table("tb_m_vendors")]
    public class Vendor : BaseEntity
    {
        [Column("vendor_name", TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }
        [Column("photo_profile", TypeName = "nvarchar(255)")]
        public string PhotoProfile { get; set; }
        [Column("sector", TypeName = "nvarchar(100)")]
        public string Sector { get; set; }
        [Column("type", TypeName = "nvarchar(100)")]
        public string Type { get; set; }
        [Column("is_approve")]
        public bool IsApprove { get; set; }

        [Column("company_guid", TypeName="CHAR(36")]
        public Guid CompanyGuid { get; set; }

        // Cardinality

        public Company? Company { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}
