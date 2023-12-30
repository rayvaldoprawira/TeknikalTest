using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.registerApprove;

public class GetRegisterApproveDto
{
    [Required]
    public Guid Guid { get; set; }

    public string CompanyName { get; set; }

    public string CompanyEmail { get; set; }

    public string? CompanyImage { get; set; }

    [Required]
    public string ValidationStatus { get; set; }

    [Required]
    public string StatusApprove { get; set; }

}
