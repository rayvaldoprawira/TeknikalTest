using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.Roles
{
    public class GetRoleDto
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
