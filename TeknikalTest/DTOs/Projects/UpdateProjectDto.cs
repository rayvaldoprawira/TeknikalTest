using TeknikalTest.Models;

namespace TeknikalTest.DTOs.Projects
{
    public class UpdateProjectDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid VendorGuid { get; set; }

        public static implicit operator Project(UpdateProjectDto projectDtoUpdate)
        {
            return new()
            {
                Guid = projectDtoUpdate.Guid,
                Name = projectDtoUpdate.Name,
                Description = projectDtoUpdate.Description,
                VendorGuid = projectDtoUpdate.VendorGuid,
            };
        }

        public static explicit operator UpdateProjectDto(Project project)
        {
            return new()
            {
                Guid = project.Guid,
                Name = project.Name,
                Description = project.Description,
                VendorGuid = project.VendorGuid,
            };
        }
    }
}

