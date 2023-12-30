using TeknikalTest.Models;

namespace TeknikalTest.Contracts;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Role? GetByName(string name);
}
