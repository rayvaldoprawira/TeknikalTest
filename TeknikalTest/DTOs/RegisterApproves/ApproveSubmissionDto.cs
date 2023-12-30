using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.registerApprove;

public class ApproveSubmissionDto
{
    [Required]
    public Guid Guid { get; set; }

    [Required]
    public IFormFile CompanyImage { get; set; }

}

