using TeknikalTest.Models;

namespace TeknikalTest.Contracts;

public interface ICompanyRepository : IGeneralRepository<Company>
{
    public Company? GetByName(string name);
}
