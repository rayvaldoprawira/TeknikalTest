
using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.Models;
using TeknikalTest.Repositories;

namespace API.Repositories;

public class SysAdminRepository : GeneralRepository<SysAdmin>, ISysAdminRepository
{
    public SysAdminRepository(AppDbContext context) : base(context)
    {
    }
}

