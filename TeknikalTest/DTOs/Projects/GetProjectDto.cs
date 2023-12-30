using TeknikalTest.Models;

namespace TeknikalTest.DTOs.Projects
{
    public class GetProjectDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid VendorGuid { get; set; }

        public static implicit operator Project(GetProjectDto projectDtoGet)
        {
            return new()
            {
                Guid = projectDtoGet.Guid,
                Name = projectDtoGet.Name,
                Description = projectDtoGet.Description,
                VendorGuid = projectDtoGet.VendorGuid,
            };
        }

        public static explicit operator GetProjectDto(Project project)
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
