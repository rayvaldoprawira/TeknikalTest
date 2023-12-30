using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.Roles
{
    public class UpdateRoleDto
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
