using TeknikalTest.Models;

namespace TeknikalTest.DTOs.Projects
{
    public class CreateProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid VendorGuid { get; set; }

        public static implicit operator Project(CreateProjectDto projectDtoCreate)
        {
            return new()
            {
                Name = projectDtoCreate.Name,
                Description = projectDtoCreate.Description,
                VendorGuid = projectDtoCreate.VendorGuid,
            };
        }

        public static explicit operator CreateProjectDto(Project project)
        {
            return new()
            {
                Name = project.Name,
                Description = project.Description,
                VendorGuid = project.VendorGuid,
            };
        }
    }
}

