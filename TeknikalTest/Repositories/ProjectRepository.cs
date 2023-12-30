using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.Models;

namespace TeknikalTest.Repositories
{
    public class ProjectRepository : GeneralRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
