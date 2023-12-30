using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.Models;

namespace TeknikalTest.Repositories
{
    public class VendorRepository : GeneralRepository<Vendor>, IVendorRepository
    {
        protected readonly AppDbContext Context;
        public VendorRepository(AppDbContext context) : base(context)
        {
            Context = context;
        }

        public Vendor? GetVendorByEmail(string email)
        {
            return Context.Set<Vendor>().FirstOrDefault(v => v.Email == email);
        }
    }
}
