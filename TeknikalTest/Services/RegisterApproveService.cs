using System.Security.Claims;
using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.DTOs.registerApprove;
using TeknikalTest.Models;
using TeknikalTest.Services;
using TeknikalTest.Utilities.Enums;
using TeknikalTest.Utilities.Handlers;
using TeknikalTest.Utilities.Validations;

namespace TeknikalTest.Services;

public class RegisterApproveService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IRegisterApproveRepository _registerPaymentRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly AccountService _accountService;
    private readonly AppDbContext _twizDbContext;
    public RegisterApproveService(IRegisterApproveRepository registerApproveRepository, ICompanyRepository companyRepository, IAccountRepository accountRepository, IEmailHandler emailHandler, AccountService accountService, AppDbContext twizDbContext, IHttpContextAccessor httpContextAccessor)
    {
        _registerPaymentRepository = registerApproveRepository;
        _companyRepository = companyRepository;
        _accountRepository = accountRepository;
        _emailHandler = emailHandler;
        _accountService = accountService;
        _twizDbContext = twizDbContext;
        _httpContextAccessor = httpContextAccessor;

    }

    public IEnumerable<GetRegisterApproveDto>? GetregisterApprove()
    {
        var registerApprove = _registerPaymentRepository.GetAll();
        if (registerApprove is null)
        {
            return null; // No RegisterPayment Found
        }

        var companies = _companyRepository.GetAll();
        var accounts = _accountRepository.GetAll();

        var toDto = registerApprove.Select(rp =>
        {
            var company = companies.FirstOrDefault(c => c.Guid == rp.CompanyGuid);
            var accountCompany = accounts.FirstOrDefault(acc => acc.Guid == company.AccountGuid);

            var statusApprove = "";
            if (rp.StatusApprove == StatusApprove.Pending) statusApprove = "pending";
            if (rp.StatusApprove == StatusApprove.Checking) statusApprove = "checking";
            if (rp.StatusApprove == StatusApprove.Approve) statusApprove = "approve";
            if (rp.StatusApprove == StatusApprove.Rejected) statusApprove = "rejected";


            return new GetRegisterApproveDto
            {
                Guid = rp.Guid,
                CompanyEmail = accountCompany?.Email ?? "",
                CompanyName = company?.Name ?? "",
                CompanyImage = rp.CompanyImage,
                StatusApprove = statusApprove,
                ValidationStatus = rp.IsValid == true ? "valid" : "invalid",
 
            };
        });


        return toDto;
    }

    public GetRegisterApproveDto? GetRegisterPayment(Guid guid)
    {
        var claimUser = _httpContextAccessor.HttpContext?.User;

        var userRole = claimUser?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        var accountGuid = claimUser?.Claims?.FirstOrDefault(x => x.Type == "Guid")?.Value;

        if (accountGuid is null)
        {
            return null;
        }

        var registerApprove = _registerPaymentRepository.GetByGuid(guid);
        if (registerApprove is null)
        {
            return null;
        }

        var company = _companyRepository.GetAll().FirstOrDefault(c => c.Guid == registerApprove.CompanyGuid);

        if (company is null)
        {
            return null;
        }

        // jika user bukan sysadmin atau company yang punya register approve ini
        if (userRole != nameof(RoleLevel.SysAdmin) && Guid.Parse(accountGuid) != company.AccountGuid)
        {
            return null;
        }

        var accountCompany = _accountRepository.GetAll().FirstOrDefault(acc => acc.Guid == company.AccountGuid);


        var statusApprove = "";
        if (registerApprove.StatusApprove == StatusApprove.Pending) statusApprove = "pending";
        if (registerApprove.StatusApprove == StatusApprove.Checking) statusApprove = "checking";
        if (registerApprove.StatusApprove == StatusApprove.Approve) statusApprove = "paid";
        if (registerApprove.StatusApprove == StatusApprove.Rejected) statusApprove = "rejected";

        var toDto = new GetRegisterApproveDto
        {
            Guid = registerApprove.Guid,
            CompanyEmail = accountCompany?.Email ?? "",
            CompanyName = company?.Name ?? "",
            CompanyImage = registerApprove.CompanyImage,
            StatusApprove= statusApprove,
            ValidationStatus = registerApprove.IsValid == true ? "valid" : "invalid",
        };

        return toDto;
    }

    public ApproveSummaryDto? GetPaymentSummary(string email)
    {
        var account = _accountRepository.GetByEmail(email);
        if (account is null)
        {
            return null;
        }
        var company = _companyRepository.GetAll().FirstOrDefault(c => c.AccountGuid == account!.Guid);
        if (company is null)
        {
            return null;
        }
        var approve = _registerPaymentRepository.GetAll().FirstOrDefault(c => c.CompanyGuid == company!.Guid);
        if (approve is null)
        {
            return null;
        }

        if (approve.StatusApprove == StatusApprove.Approve)
        {
            var paid = new ApproveSummaryDto
            {

            };
            return paid;
        }

        var toDto = new ApproveSummaryDto
        {
            Guid = approve.Guid,
            
        };
        return toDto;

    }

    public GetRegisterApproveDto? CreateRegisterPayment(CreateRegisterApproveDto newRegisterPaymentDto)
    {
        var registerPayment = new RegisterApprove
        {
            Guid = new Guid(),
            CompanyGuid = newRegisterPaymentDto.CompanyGuid,
            CompanyImage = newRegisterPaymentDto.CompanyImage ?? "",
            IsValid = newRegisterPaymentDto.IsValid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdRegisterPayment = _registerPaymentRepository.Create(registerPayment);
        if (createdRegisterPayment is null)
        {
            return null; // RegisterPayment Not Created
        }

        var toDto = new GetRegisterApproveDto
        {
            Guid = createdRegisterPayment.Guid,
            //CompanyGuid = createdRegisterPayment.CompanyGuid,
            CompanyImage = createdRegisterPayment.CompanyImage,
            //IsValid = createdRegisterPayment.IsValid,
            //BankGuid = createdRegisterPayment.BankGuid,
        };

        return toDto; // RegisterPayment Created
    }

    public int UpdateRegisterPayment(UpdateRegisterApproveDto UpdateRegisterPaymentDto)
    {
        var isExist = _registerPaymentRepository.IsExist(UpdateRegisterPaymentDto.Guid);
        if (!isExist)
        {
            return -1; // RegisterPayment Not Found
        }

        var getRegisterPayment = _registerPaymentRepository.GetByGuid(UpdateRegisterPaymentDto.Guid);

        var registerapprove = new RegisterApprove
        {
            Guid = UpdateRegisterPaymentDto.Guid,
            CompanyGuid = UpdateRegisterPaymentDto.CompanyGuid,
            CompanyImage = UpdateRegisterPaymentDto.CompanyImage ?? "",
            IsValid = UpdateRegisterPaymentDto.IsValid,
            ModifiedDate = DateTime.Now,
            CreatedDate = getRegisterPayment!.CreatedDate
        };

        var isUpdate = _registerPaymentRepository.Update(registerapprove);
        if (!isUpdate)
        {
            return 0; // RegisterPayment Not Updated
        }
        return 1;
    }

    public int DeleteRegisterPayment(Guid guid)
    {
        var isExist = _registerPaymentRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // RegisterPayment Not Found
        }

        var registerapprove = _registerPaymentRepository.GetByGuid(guid);
        var isDelete = _registerPaymentRepository.Delete(registerapprove!);
        if (!isDelete)
        {
            return 0; // RegisterPayment Not Deleted
        }

        return 1; // RegisterPayment Deleted
    }
    
    public async Task<int> UploadPaymentSubmission(ApproveSubmissionDto approveSubmissionDto)
    {
        if(approveSubmissionDto.CompanyImage is null)
        {
            return -1;
        }

        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\register_approves");

        if (!Directory.Exists(folderPath))
        {
            try
            {
                Directory.CreateDirectory(folderPath);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        var size = approveSubmissionDto.CompanyImage.Length;

        // jika ukuran gambar lebih dari 2mb
        if (size > 2000000)
        {
            return -2;
        }

        bool isImage = FileValidation.IsValidImageExtension(approveSubmissionDto.CompanyImage);

        if (!isImage)
        {
            return -3;
        }

        var registerPaymentByGuid = _registerPaymentRepository.GetByGuid(approveSubmissionDto.Guid);
        var oldImageUrl = "";

        if (registerPaymentByGuid == null)
        {
            return -7;
        }

        if (registerPaymentByGuid.CompanyImage != "")
        {
            oldImageUrl = registerPaymentByGuid.CompanyImage;
        }



        var randomName = GenerateHandler.GenerateRandomString();
        var fileName = randomName + approveSubmissionDto.CompanyImage.FileName;
        var imageUrl = $"{fileName}";

        var filePath = $"{folderPath}\\{fileName}";
        using var transaction = _twizDbContext.Database.BeginTransaction();
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await approveSubmissionDto.CompanyImage.CopyToAsync(stream);
            }

            registerPaymentByGuid.CompanyImage = imageUrl;
            registerPaymentByGuid.StatusApprove = StatusApprove.Checking;

            var updatedRegisterPayment = _registerPaymentRepository.Update(registerPaymentByGuid);

            if (updatedRegisterPayment == false)
            {
                FileHandler.DeleteFileIfExist(filePath);
                return -5;
            };


            // hapus foto lama jika ada
            if (oldImageUrl != "")
            {
                var filePathOldImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImageUrl.Replace("/", "\\"));
                FileHandler.DeleteFileIfExist(filePathOldImage);
            }
        }
        catch
        {
            transaction.Rollback();

            FileHandler.DeleteFileIfExist(filePath);
            return -4;
        }

        try
        {
            var company = _companyRepository.GetByGuid(registerPaymentByGuid.CompanyGuid);

            if (company == null)
            {
                FileHandler.DeleteFileIfExist(filePath);
                transaction.Rollback();
                return -7;
            }

            var contentEmail = $"" +
                $"<h1>Register Payment Submission</h1>" +
                $"<p>Company Name : {company.Name}</p>" +
                $"<p>Now you can check it</p>";

            var emailAdmin = _accountService.GetEmailSysAdmin();


            _emailHandler.SendEmail(_accountService.GetEmailSysAdmin(), "Register Payment Submission", contentEmail);
        }
        catch
        {
            FileHandler.DeleteFileIfExist(filePath);
            transaction.Rollback();
            return -6;
        }

        transaction.Commit();


        return 1;
    }

    public int AproveRegisterPayment(AproveRegisterDto aproveRegisterPaymentDto)
    {
        using var transaction = _twizDbContext.Database.BeginTransaction();

        var getRegisterPayment = _registerPaymentRepository.GetByGuid(aproveRegisterPaymentDto.Guid);

        if (getRegisterPayment == null)
        {
            return 0;
        }


        getRegisterPayment.IsValid = true;
        getRegisterPayment.StatusApprove = StatusApprove.Approve;
        getRegisterPayment.ModifiedDate = DateTime.Now;

        var registerPaymentUpdated = _registerPaymentRepository.Update(getRegisterPayment);

        if (registerPaymentUpdated is false)
        {
            transaction.Rollback();
            return 0;
        }

        var company = _companyRepository.GetAll().FirstOrDefault(c => c.Guid == getRegisterPayment.CompanyGuid);

        if (company is null)
        {
            transaction.Rollback();
            return 0;
        }

        var accountCompany = _accountRepository.GetAll().FirstOrDefault(acc => acc.Guid == company.AccountGuid);

        if (accountCompany is null)
        {
            transaction.Rollback();
            return 0;
        }

        // aktivasi account
        accountCompany.IsActive = true;

        var accountUpdated = _accountRepository.Update(accountCompany);

        if (accountUpdated is false)
        {
            transaction.Rollback();
            return 0;
        }


        try
        {
            var contentEmail = $"<h1>Conratulation Your Account Has Been Activated!!</h1>" +
                $"<p>welcome to tWiz. Now you can fully use the services of our tWiz service. Don't hesitate to contact the support center if you have any problems using our tWiz service</p>";

            _emailHandler.SendEmail(accountCompany.Email, "Aproved tWiz Account", contentEmail);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            return 0;
        }

        return 1;
    }

    public int RejectRegisterPayment(AproveRegisterDto aproveRegisterPaymentDto)
    {
        using var transaction = _twizDbContext.Database.BeginTransaction();

        var getRegisterPayment = _registerPaymentRepository.GetByGuid(aproveRegisterPaymentDto.Guid);

        if (getRegisterPayment == null)
        {
            return 0;
        }

        getRegisterPayment.IsValid = false;
        getRegisterPayment.StatusApprove = StatusApprove.Pending;
        getRegisterPayment.ModifiedDate = DateTime.Now;

        var registerPaymentUpdated = _registerPaymentRepository.Update(getRegisterPayment);

        if (registerPaymentUpdated is false)
        {
            transaction.Rollback();
            return 0;
        }

        var company = _companyRepository.GetAll().FirstOrDefault(c => c.Guid == getRegisterPayment.CompanyGuid);

        if (company is null)
        {
            transaction.Rollback();
            return 0;
        }

        var accountCompany = _accountRepository.GetAll().FirstOrDefault(acc => acc.Guid == company.AccountGuid);

        if (accountCompany is null)
        {
            transaction.Rollback();
            return 0;
        }

        // aktivasi account
        accountCompany.IsActive = true;

        var accountUpdated = _accountRepository.Update(accountCompany);

        if (accountUpdated is false)
        {
            transaction.Rollback();
            return 0;
        }

        try
        {
            var contentEmail = $"<h1>Your Submission is Rejected</h1>" +
                $"<p>For customers. Sorry, we still can't activate your tWiz account because the proof of approve that you uploaded is invalid</p>";

            _emailHandler.SendEmail(accountCompany.Email, "Reject activation tWiz Account", contentEmail);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            return 0;
        }

        return 1;
    }

}
