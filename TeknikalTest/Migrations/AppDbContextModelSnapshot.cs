﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeknikalTest.Data;

#nullable disable

namespace TeknikalTest.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TeknikalTest.Models.Account", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.Property<int?>("Token")
                        .HasColumnType("int")
                        .HasColumnName("token");

                    b.Property<DateTime?>("TokenExpiration")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("token_expiration");

                    b.Property<bool?>("TokenIsUsed")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("token_is_used");

                    b.HasKey("Guid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("tb_m_accounts");
                });

            modelBuilder.Entity("TeknikalTest.Models.AccountRole", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("account_guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RoleGuid")
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("role_guid");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.HasIndex("RoleGuid");

                    b.ToTable("tb_m_account_roles");
                });

            modelBuilder.Entity("TeknikalTest.Models.Company", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("account_guid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("address");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("company_email");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone_number");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid")
                        .IsUnique();

                    b.HasIndex("Email", "PhoneNumber", "AccountGuid")
                        .IsUnique();

                    b.ToTable("tb_m_company");
                });

            modelBuilder.Entity("TeknikalTest.Models.Project", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("description");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<Guid>("VendorGuid")
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("vendor_guid");

                    b.HasKey("Guid");

                    b.HasIndex("VendorGuid");

                    b.ToTable("tb_m_projects");
                });

            modelBuilder.Entity("TeknikalTest.Models.RegisterApprove", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<Guid>("CompanyGuid")
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("company_guid");

                    b.Property<string>("CompanyImage")
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("approve_image");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsValid")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_valid");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<int>("StatusApprove")
                        .HasColumnType("int")
                        .HasColumnName("status_Approve");

                    b.HasKey("Guid");

                    b.HasIndex("CompanyGuid")
                        .IsUnique();

                    b.ToTable("tb_m_register_approves");
                });

            modelBuilder.Entity("TeknikalTest.Models.Role", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_roles");
                });

            modelBuilder.Entity("TeknikalTest.Models.SysAdmin", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("account_guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid")
                        .IsUnique();

                    b.ToTable("tb_m_sys_admin");
                });

            modelBuilder.Entity("TeknikalTest.Models.Vendor", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("guid");

                    b.Property<Guid>("CompanyGuid")
                        .HasColumnType("CHAR(36")
                        .HasColumnName("company_guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<bool>("IsApprove")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_approve");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("vendor_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone_number");

                    b.Property<string>("PhotoProfile")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("photo_profile");

                    b.Property<string>("Sector")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("sector");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("type");

                    b.HasKey("Guid");

                    b.HasIndex("CompanyGuid")
                        .IsUnique();

                    b.HasIndex("Email", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("tb_m_vendors");
                });

            modelBuilder.Entity("TeknikalTest.Models.AccountRole", b =>
                {
                    b.HasOne("TeknikalTest.Models.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeknikalTest.Models.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TeknikalTest.Models.Company", b =>
                {
                    b.HasOne("TeknikalTest.Models.Account", "Account")
                        .WithOne("Company")
                        .HasForeignKey("TeknikalTest.Models.Company", "AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TeknikalTest.Models.Project", b =>
                {
                    b.HasOne("TeknikalTest.Models.Vendor", "Vendor")
                        .WithMany("Projects")
                        .HasForeignKey("VendorGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("TeknikalTest.Models.RegisterApprove", b =>
                {
                    b.HasOne("TeknikalTest.Models.Company", "Company")
                        .WithOne("RegisterApprove")
                        .HasForeignKey("TeknikalTest.Models.RegisterApprove", "CompanyGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TeknikalTest.Models.SysAdmin", b =>
                {
                    b.HasOne("TeknikalTest.Models.Account", "Account")
                        .WithOne("SysAdmin")
                        .HasForeignKey("TeknikalTest.Models.SysAdmin", "AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TeknikalTest.Models.Vendor", b =>
                {
                    b.HasOne("TeknikalTest.Models.Company", "Company")
                        .WithOne("Vendor")
                        .HasForeignKey("TeknikalTest.Models.Vendor", "CompanyGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TeknikalTest.Models.Account", b =>
                {
                    b.Navigation("AccountRoles");

                    b.Navigation("Company");

                    b.Navigation("SysAdmin");
                });

            modelBuilder.Entity("TeknikalTest.Models.Company", b =>
                {
                    b.Navigation("RegisterApprove");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("TeknikalTest.Models.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("TeknikalTest.Models.Vendor", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
