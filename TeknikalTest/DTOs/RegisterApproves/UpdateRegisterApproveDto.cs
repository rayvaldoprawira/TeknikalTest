using System.ComponentModel.DataAnnotations;
using TeknikalTest.Utilities.Enums;

namespace TeknikalTest.DTOs.registerApprove;

public class UpdateRegisterApproveDto
{
    [Required]
    public Guid Guid { get; set; }

    [Required]
    public Guid CompanyGuid { get; set; }

    public string? CompanyImage { get; set; }

    [Required]
    public bool IsValid { get; set; }

    [Required]
    public StatusApprove StatusApprove { get; set; }

}
