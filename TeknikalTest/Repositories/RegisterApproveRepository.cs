
using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.Models;
using TeknikalTest.Utilities.Enums;

namespace TeknikalTest.Repositories;

public class RegisterApproveRepository : GeneralRepository<RegisterApprove>, IRegisterApproveRepository
{
    public RegisterApproveRepository(AppDbContext context) : base(context)
    {
    }

    public bool UpdateCompanyImage(Guid guid, string approveImageUrl = "")
    {
        try
        {
            var registerPaymentByGuid = _context.Set<RegisterApprove>().Find(guid);

            if (registerPaymentByGuid is null)
            {
                return false;
            }

            if (approveImageUrl != "")
            {
                registerPaymentByGuid.CompanyImage = approveImageUrl;
                _context.SaveChanges();
                return true;
            }

            return false;


        }
        catch { return false; }

    }

    public bool ChangeStatusRegisterPayment(Guid registerPaymentGuid, StatusApprove statusApprove)
    {
        try
        {
            var registerPaymentByGuid = _context.Set<RegisterApprove>().Find(registerPaymentGuid);

            if (registerPaymentByGuid is null)
            {
                return false;
            }

            registerPaymentByGuid.StatusApprove = statusApprove;
            _context.SaveChanges();

            return true;
        }
        catch { return false; }

    }
}
