using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikalTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    email = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    token = table.Column<int>(type: "int", nullable: true),
                    token_is_used = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    token_expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.guid);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_company",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    company_email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    account_guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_company", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_company_tb_m_accounts_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_accounts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_sys_admin",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    account_guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_sys_admin", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_sys_admin_tb_m_accounts_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_accounts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_account_roles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    account_guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    role_guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account_roles", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_account_roles_tb_m_accounts_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_accounts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_account_roles_tb_m_roles_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_roles",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_register_approves",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    company_guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    is_valid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    approve_image = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status_Approve = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_register_approves", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_register_approves_tb_m_company_company_guid",
                        column: x => x.company_guid,
                        principalTable: "tb_m_company",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_vendors",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    vendor_name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    photo_profile = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    sector = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    is_approve = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    company_guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_vendors", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_vendors_tb_m_company_company_guid",
                        column: x => x.company_guid,
                        principalTable: "tb_m_company",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_m_projects",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    vendor_guid = table.Column<Guid>(type: "CHAR(36)", nullable: false, collation: "ascii_general_ci"),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_projects", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_projects_tb_m_vendors_vendor_guid",
                        column: x => x.vendor_guid,
                        principalTable: "tb_m_vendors",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_account_guid",
                table: "tb_m_account_roles",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_role_guid",
                table: "tb_m_account_roles",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accounts_email",
                table: "tb_m_accounts",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_company_account_guid",
                table: "tb_m_company",
                column: "account_guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_company_company_email_phone_number_account_guid",
                table: "tb_m_company",
                columns: new[] { "company_email", "phone_number", "account_guid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_projects_vendor_guid",
                table: "tb_m_projects",
                column: "vendor_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_register_approves_company_guid",
                table: "tb_m_register_approves",
                column: "company_guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_sys_admin_account_guid",
                table: "tb_m_sys_admin",
                column: "account_guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_vendors_company_guid",
                table: "tb_m_vendors",
                column: "company_guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_vendors_email_phone_number",
                table: "tb_m_vendors",
                columns: new[] { "email", "phone_number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_account_roles");

            migrationBuilder.DropTable(
                name: "tb_m_projects");

            migrationBuilder.DropTable(
                name: "tb_m_register_approves");

            migrationBuilder.DropTable(
                name: "tb_m_sys_admin");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_vendors");

            migrationBuilder.DropTable(
                name: "tb_m_company");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");
        }
    }
}
