using TeknikalTest.Contracts;
using TeknikalTest.DTOs.Projects;

namespace TeknikalTest.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<GetProjectDto> Get()
        {
            var projects = _projectRepository.GetAll().ToList();
            if (!projects.Any()) return Enumerable.Empty<GetProjectDto>();
            List<GetProjectDto> projectDtoGets = new List<GetProjectDto>();
            foreach (var project in projects)
            {
                projectDtoGets.Add((GetProjectDto)project);
            }
            return projectDtoGets;
        }

        public GetProjectDto? Get(Guid guid)
        {
            var project = _projectRepository.GetByGuid(guid);
            if (project is null) return null;
            return (GetProjectDto)project;
        }

        public CreateProjectDto? Create(CreateProjectDto projectDtoCreate)
        {
            var projectCreated = _projectRepository.Create(projectDtoCreate);
            if (projectCreated is null) return null;
            return (CreateProjectDto)projectCreated;
        }

        public int Update(UpdateProjectDto projectDtoUpdate)
        {
            var project = _projectRepository.GetByGuid(projectDtoUpdate.Guid);
            if (project is null) return -1;
            var projectUpdated = _projectRepository.Update(projectDtoUpdate);
            return projectUpdated ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var project = _projectRepository.GetByGuid(guid);
            if (project is null) return -1;
            var projectDeleted = _projectRepository.Delete(project);
            return projectDeleted ? 1 : 0;
        }
    }
}
