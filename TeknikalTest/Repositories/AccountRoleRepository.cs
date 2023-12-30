
using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.Models;
using TeknikalTest.Repositories;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(AppDbContext context) : base(context)
    {

    }

    public IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid)
    {
        return _context.Set<AccountRole>().Where(ar => ar.AccountGuid == guid);
    }

    public IEnumerable<AccountRole> GetByGuidCompany(Guid companyGuid)
    {
        return _context.Set<AccountRole>().Where(ar => ar.AccountGuid == companyGuid);
    }
}
