﻿using System.ComponentModel.DataAnnotations;

namespace TeknikalTest.DTOs.Companies;

public class UpdateCompanyDto
{
    [Required]
    public Guid Guid { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public Guid AccountGuid { get; set; }
}
