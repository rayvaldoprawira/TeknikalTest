using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TeknikalTest.Utilities.Enums;

namespace TeknikalTest.DTOs.registerApprove;

public class CreateRegisterApproveDto
{
    [Required]
    public Guid CompanyGuid { get; set; }

    [Required]
    public int VaNumber { get; set; }

    [Required]
    public decimal Price { get; set; }
    public string? CompanyImage { get; set; }

    [DefaultValue(false)]
    public bool IsValid { get; set; }

    [DefaultValue(StatusApprove.Pending)]
    public StatusApprove StatusApprove { get; set; }

}
