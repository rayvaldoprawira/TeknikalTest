using System.Security.Claims;
using TeknikalTest.Contracts;
using TeknikalTest.Data;
using TeknikalTest.DTOs.Auths;
using TeknikalTest.Models;
using TeknikalTest.Utilities.Enums;
using TeknikalTest.Utilities.Handlers;

namespace TeknikalTest.Services;

public class AuthService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IRegisterApproveRepository _registerApproveRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ISysAdminRepository _sysAdminRepository;
    private readonly ITokenHandler _tokenHandler;
    private readonly AppDbContext _twizDbContext;
    private readonly IEmailHandler _emailHandler;
    private object _registerPaymentRepository;

    public AuthService(IAccountRepository accountRepository,
        ICompanyRepository companyRepository,
        IRegisterApproveRepository registerApproveRepository,
        IAccountRoleRepository accountRoleRepository,
        IRoleRepository roleRepository,
        ITokenHandler tokenHandler,
        AppDbContext twizDbContext,
        IEmailHandler emailHandler,
        ISysAdminRepository sysAdminRepository)
    {
        _accountRepository = accountRepository;
        _companyRepository = companyRepository;
        _registerApproveRepository = registerApproveRepository;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;
        _tokenHandler = tokenHandler;
        _twizDbContext = twizDbContext;
        _emailHandler = emailHandler;
        _sysAdminRepository = sysAdminRepository;
    }

    public RegisterDto? Register(RegisterDto registerDto)
    {
        using var transaction = _twizDbContext.Database.BeginTransaction();
        try
        {
            Account account = new Account
            {
                Guid = new Guid(),
                Email = registerDto.Email,
                Password = HashingHandler.HashPassword(registerDto.Password),
                IsActive = false,
                Token = null,
                TokenIsUsed = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                TokenExpiration = null,
                ConfirmPassword = registerDto.ConfirmPassword
            };

            var createdAccount = _accountRepository.Create(account);
            if (createdAccount is null)
            {
                return null;
            }

            Company company = new Company
            {
                Guid = new Guid(),
                AccountGuid = createdAccount.Guid,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Address = registerDto.Address,
                PhoneNumber = registerDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdCompany = _companyRepository.Create(company);
            if (createdCompany is null)
            {
                return null;
            }

            var getRoleUser = _roleRepository.GetByName(nameof(RoleLevel.Company));
            if (getRoleUser is null)
            {
                return null;
            }
            var accountRole = _accountRoleRepository.Create(new AccountRole
            {
                AccountGuid = account.Guid,
                RoleGuid = getRoleUser.Guid
            });

            if (accountRole is null)
            {
                return null;
            }

          

            RegisterApprove registerPayment = new RegisterApprove
            {
                CompanyGuid = createdCompany.Guid,
                CompanyImage = "",
                IsValid = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                StatusApprove = StatusApprove.Pending,
            };

            var createdRegisterPayment = _registerApproveRepository.Create(registerPayment);
            if (createdRegisterPayment is null)
            {
                return null;
            }

            var toDto = new RegisterDto
            {
                Name = createdCompany.Name,
                Email = createdAccount.Email,
                PhoneNumber = createdCompany.PhoneNumber,
                Password = createdAccount.Password,
                Address = createdCompany.Address,
                ConfirmPassword = createdAccount.Password
            };

            transaction.Commit();
            return toDto;
        }
        catch (Exception)
        {
            transaction.Rollback();
            return null;
        }
    }

    public string Login(LoginDto loginDto)
    {

        var account = _accountRepository.GetByEmail(loginDto.Email);
        if (account is null)
        {
            return "0"; // account not found
        }

        var isPasswordValid = HashingHandler.ValidatePassword(loginDto.Password, account.Password);
        if (!isPasswordValid)
        {
            return "-1"; // wrong password
        }

        var claims = new List<Claim>()
        {
            new Claim("Guid", account.Guid.ToString()),
            new Claim("IsActive", account.IsActive.ToString()),
            new Claim("Email", loginDto.Email)
        };

        var company = _companyRepository.GetAll().FirstOrDefault(c => c.AccountGuid == account.Guid);
        var sysAdmin = _sysAdminRepository.GetAll().FirstOrDefault(sa => sa.AccountGuid == account.Guid);

        if (company is not null)
        {
            claims.Add(new Claim(ClaimTypes.Name, company.Name));
            claims.Add(new Claim("companyGuid", company.Guid.ToString()));

            var approve = _registerApproveRepository.GetAll().FirstOrDefault(c => c.CompanyGuid == company.Guid);

            if (approve == null || approve.StatusApprove != StatusApprove.Approve)
            {
                return "-3"; // register approve not paid
            }
            else
            {
                if (!account.IsActive)
                {
                    return "-4"; // account deactivate (is_active = 0)
                }
            }
        }
        else if (sysAdmin is not null)
        {
            claims.Add(new Claim(ClaimTypes.Name, sysAdmin.Name));
        }
        else
        {
            return "0";
        }

        var getAccountRole = _accountRoleRepository.GetByGuidCompany(account.Guid);

        var getRoleNameByAccountRole = from ar in getAccountRole
                                       join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                       select r.Name;

        claims.AddRange(getRoleNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

        try
        {
            var token = _tokenHandler.GenerateToken(claims);
            return token;
        }
        catch
        {
            return "-2"; // generate token is failed
        }
    }

    public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var isExist = _accountRepository.CheckEmail(changePasswordDto.Email);
        if (isExist is null)
        {
            return -1; //Account Not Found
        }

        var getAccount = _accountRepository.GetByGuid(isExist.Guid);
        if (getAccount.Token != changePasswordDto.Token)
        {
            return 0;
        }

        if (getAccount.TokenIsUsed == true)
        {
            return 1;
        }
        if (getAccount.TokenExpiration < DateTime.Now)
        {
            return 2;
        }

        var account = new Account
        {
            Guid = getAccount.Guid,
            Email = getAccount.Email,
            IsActive = getAccount.IsActive,
            ModifiedDate = getAccount.ModifiedDate,
            CreatedDate = getAccount.CreatedDate,
            Token = getAccount.Token,
            TokenExpiration = getAccount.TokenExpiration,
            TokenIsUsed = getAccount.TokenIsUsed,
            Password = HashingHandler.HashPassword(changePasswordDto.NewPassword)
        };

        var isUpdate = _accountRepository.Update(account);
        if (!isUpdate)
        {
            return 0; // Account not updated
        }

        return 3;
    }

    public ForgotPasswordDto ForgotPassword(string email)
    {
        var account = _accountRepository.GetByEmail(email);
        var accountRole = _accountRoleRepository.GetAll();
        var role = _roleRepository.GetAll();

        if (account is null)
        {
            return null!;
        }

        var data = from ar in accountRole
                   join r in role on ar.RoleGuid equals r.Guid
                   where ar.AccountGuid == account.Guid
                   select new
                   {
                       AccountRole = ar,
                       Role = r
                   };

        var matchedData = data.FirstOrDefault();

        if (matchedData is null)
        {
            return null!;
        }

        var toDto = new ForgotPasswordDto
        {
            Role = matchedData.Role.Name,
            Email = account.Email,
            Token = GenerateHandler.GenerateToken(),
            TokenExpiration = DateTime.Now.AddMinutes(5)
        };

        var relatedAccount = _accountRepository.GetByGuid(account.Guid);

        var update = new Account
        {
            Guid = relatedAccount!.Guid,
            Email = relatedAccount.Email,
            Password = relatedAccount.Password,
            Token = toDto.Token,
            IsActive = relatedAccount.IsActive,
            TokenIsUsed = relatedAccount.TokenIsUsed,
            TokenExpiration = DateTime.Now.AddMinutes(5)

        };

        var updateResult = _accountRepository.Update(update);

        if (!updateResult)
        {
            return null!;
        }

        _emailHandler.SendEmail(toDto.Email,
                       "Register Payment",
                       $"Your Virtual Account Number is {toDto.Token}, <a href='https://localhost:7153/?Token={toDto.Token}'>Klik Disini</a>");

        return toDto;
    }
}
