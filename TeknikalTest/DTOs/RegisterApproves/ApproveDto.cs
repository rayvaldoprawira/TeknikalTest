using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.registerApprove
{
    public class ApproveDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
