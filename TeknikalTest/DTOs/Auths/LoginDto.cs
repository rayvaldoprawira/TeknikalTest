using API.Utilities.Validations;
using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.Auths
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPolicy]
        public string Password { get; set; }
    }
}
