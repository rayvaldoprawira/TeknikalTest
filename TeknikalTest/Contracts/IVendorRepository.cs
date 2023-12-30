using TeknikalTest.Models;

namespace TeknikalTest.Contracts
{
    public interface IVendorRepository : IGeneralRepository<Vendor>
    {
        Vendor? GetVendorByEmail(string email);
    }
}
