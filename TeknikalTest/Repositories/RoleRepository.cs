using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.Models;
using TeknikalTest.Repositories;

namespace API.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }
    public Role? GetByName(string name)
    {
        return _context.Set<Role>().FirstOrDefault(r => r.Name == name);
    }
}
