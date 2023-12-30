using Microsoft.EntityFrameworkCore;
using TeknikalTest.Models;

namespace TeknikalTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<RegisterApprove> RegisterApproves { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SysAdmin> SysAdmins { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasIndex(c => new
                {
                    c.Email,
                    c.PhoneNumber,
                    c.AccountGuid
                }).IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => new
                {
                    a.Email
                }).IsUnique();

            modelBuilder.Entity<RegisterApprove>()
                .HasIndex(r => new
                {
                    r.CompanyGuid
                }).IsUnique();

            modelBuilder.Entity<Vendor>()
                .HasIndex(v => new
                {
                    v.Email,
                    v.PhoneNumber
                }).IsUnique();


            //Account - AccountRole (One to Many)
            modelBuilder.Entity<Account>()
                .HasMany(account => account.AccountRoles)
                .WithOne(accountRole => accountRole.Account)
                .HasForeignKey(accountRole => accountRole.AccountGuid);

            //Account - Company (One to One)
            modelBuilder.Entity<Account>()
                .HasOne(account => account.Company)
                .WithOne(company => company.Account);

            //Account - SysAdmin (One to One)
            modelBuilder.Entity<SysAdmin>()
                .HasOne(account => account.Account)
                .WithOne(sysadmin => sysadmin.SysAdmin);

            //AccountRole - Role (One to Many)
            modelBuilder.Entity<AccountRole>()
                .HasOne(accountrole => accountrole.Role)
                .WithMany(roles => roles.AccountRoles)
                .HasForeignKey(accountrole => accountrole.RoleGuid);

            // Company - RegisterApprove (One to One)
            modelBuilder.Entity<Company>()
                .HasOne(company => company.RegisterApprove)
                .WithOne(registerapprove => registerapprove.Company);

            // Company - Vendor (One to One)
            modelBuilder.Entity<Company>()
                .HasOne(company => company.Vendor)
                .WithOne(vendor => vendor.Company);

            // Vendor - Projects (One to Many)
            modelBuilder.Entity<Vendor>()
                .HasMany(vendor=>vendor.Projects)
                .WithOne(projects=>projects.Vendor)
                .HasForeignKey(project=>project.VendorGuid);
        }

    }
}
