using TeknikalTest.Models;

namespace TeknikalTest.Contracts;

public interface IAccountRepository : IGeneralRepository<Account>
{
    public Account? GetByEmail(string email);
    public Account? CheckEmail(string email);
    public Account? ActivateDeactivate(Guid guid);

   
}
