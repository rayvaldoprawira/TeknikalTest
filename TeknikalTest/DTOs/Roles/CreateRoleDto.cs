using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.Roles
{
    public class CreateRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
