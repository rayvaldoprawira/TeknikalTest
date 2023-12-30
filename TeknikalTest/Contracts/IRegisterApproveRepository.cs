using TeknikalTest.Models;
using TeknikalTest.Utilities.Enums;

namespace TeknikalTest.Contracts
{
    public interface IRegisterApproveRepository : IGeneralRepository<RegisterApprove>
    {
        public bool UpdateCompanyImage(Guid guid, string imageUrl);

        public bool ChangeStatusRegisterPayment(Guid registerPaymentGuid, StatusApprove statusApprove);
    }
}
