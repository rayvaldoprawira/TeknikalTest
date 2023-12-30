

using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.Models;
using TeknikalTest.Repositories;

namespace API.Repositories;

public class CompanyRepository : GeneralRepository<Company>, ICompanyRepository
{
    public CompanyRepository(AppDbContext context) : base(context)
    {
    }

    public Company? GetByName(string name)
    {
        return _context.Set<Company>().FirstOrDefault(company => company.Name == name);
    }
}
