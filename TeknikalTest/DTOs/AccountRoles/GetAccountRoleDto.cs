using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.AccountRoles
{
    public class GetAccountRoleDto
    {
     
            [Required]
            public Guid Guid { get; set; }

            [Required]
            public Guid AccountGuid { get; set; }

            [Required]
            public Guid RoleGuid { get; set; }

    }
}
