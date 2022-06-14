using Microsoft.EntityFrameworkCore.Migrations;

namespace EPharmacy.Infrastructure.Persistence.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ConciergeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "DeviceCodes",
            columns: table => new
            {
                UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
            });

        migrationBuilder.CreateTable(
            name: "Drugs",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Drugs", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "Formulations",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Formulations", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "Patients",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                ConciergeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Patients", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "PersistedGrants",
            columns: table => new
            {
                Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersistedGrants", x => x.Key);
            });

        migrationBuilder.CreateTable(
            name: "Pharmacies",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ConciergeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                Approved = table.Column<bool>(type: "bit", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pharmacies", x => x.ID);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Prescriptions",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DeliveryLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NoOfItems = table.Column<int>(type: "int", nullable: false),
                PatientID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Prescriptions", x => x.ID);
                table.ForeignKey(
                    name: "FK_Prescriptions_Patients_PatientID",
                    column: x => x.PatientID,
                    principalTable: "Patients",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "PharmacyUsers",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PharmacyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PharmacyUsers", x => x.ID);
                table.ForeignKey(
                    name: "FK_PharmacyUsers_Pharmacies_PharmacyID",
                    column: x => x.PharmacyID,
                    principalTable: "Pharmacies",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "NonPharmaceuticalItems",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PrescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NonPharmaceuticalItems", x => x.ID);
                table.ForeignKey(
                    name: "FK_NonPharmaceuticalItems_Categories_CategoryID",
                    column: x => x.CategoryID,
                    principalTable: "Categories",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_NonPharmaceuticalItems_Prescriptions_PrescriptionID",
                    column: x => x.PrescriptionID,
                    principalTable: "Prescriptions",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "PharmaceuticalItems",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DrugID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Dosage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Frequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                FormulationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PrescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PharmaceuticalItems", x => x.ID);
                table.ForeignKey(
                    name: "FK_PharmaceuticalItems_Categories_CategoryID",
                    column: x => x.CategoryID,
                    principalTable: "Categories",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PharmaceuticalItems_Drugs_DrugID",
                    column: x => x.DrugID,
                    principalTable: "Drugs",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PharmaceuticalItems_Formulations_FormulationID",
                    column: x => x.FormulationID,
                    principalTable: "Formulations",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PharmaceuticalItems_Prescriptions_PrescriptionID",
                    column: x => x.PrescriptionID,
                    principalTable: "Prescriptions",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "PharmacyPrescriptions",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Expiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                PrescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PharmacyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PharmacyPrescriptions", x => x.ID);
                table.ForeignKey(
                    name: "FK_PharmacyPrescriptions_Pharmacies_PharmacyID",
                    column: x => x.PharmacyID,
                    principalTable: "Pharmacies",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PharmacyPrescriptions_Prescriptions_PrescriptionID",
                    column: x => x.PrescriptionID,
                    principalTable: "Prescriptions",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Quotations",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NoQuoted = table.Column<int>(type: "int", nullable: false),
                ToQuote = table.Column<int>(type: "int", nullable: false),
                DeliveryFee = table.Column<double>(type: "float", nullable: false),
                Markup = table.Column<double>(type: "float", nullable: false),
                Total = table.Column<double>(type: "float", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                PharmacyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Quotations", x => x.ID);
                table.ForeignKey(
                    name: "FK_Quotations_Pharmacies_PharmacyID",
                    column: x => x.PharmacyID,
                    principalTable: "Pharmacies",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Quotations_PharmacyPrescriptions_ID",
                    column: x => x.ID,
                    principalTable: "PharmacyPrescriptions",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "NonPharmaceuticalQuotationItems",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                UnitPrice = table.Column<double>(type: "float", nullable: false),
                Markup = table.Column<double>(type: "float", nullable: false),
                Total = table.Column<double>(type: "float", nullable: false, computedColumnSql: "([UnitPrice] * [Quantity]) + [Markup]"),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NonPharmaceuticalItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                QuotationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NonPharmaceuticalQuotationItems", x => x.ID);
                table.ForeignKey(
                    name: "FK_NonPharmaceuticalQuotationItems_NonPharmaceuticalItems_NonPharmaceuticalItemID",
                    column: x => x.NonPharmaceuticalItemID,
                    principalTable: "NonPharmaceuticalItems",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_NonPharmaceuticalQuotationItems_Quotations_QuotationID",
                    column: x => x.QuotationID,
                    principalTable: "Quotations",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "PharmaceuticalQuotationItems",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Stocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                GenericDrug = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                UnitPrice = table.Column<double>(type: "float", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Markup = table.Column<double>(type: "float", nullable: false),
                Total = table.Column<double>(type: "float", nullable: false, computedColumnSql: "([UnitPrice] * [Quantity]) + [Markup]"),
                PharmaceuticalItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                QuotationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PharmaceuticalQuotationItems", x => x.ID);
                table.ForeignKey(
                    name: "FK_PharmaceuticalQuotationItems_PharmaceuticalItems_PharmaceuticalItemID",
                    column: x => x.PharmaceuticalItemID,
                    principalTable: "PharmaceuticalItems",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PharmaceuticalQuotationItems_Quotations_QuotationID",
                    column: x => x.QuotationID,
                    principalTable: "Quotations",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "WorkOrders",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ConciergeAppointmentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkOrders", x => x.ID);
                table.ForeignKey(
                    name: "FK_WorkOrders_Quotations_ID",
                    column: x => x.ID,
                    principalTable: "Quotations",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AspNetUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "AspNetUserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_DeviceCodes_DeviceCode",
            table: "DeviceCodes",
            column: "DeviceCode",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_DeviceCodes_Expiration",
            table: "DeviceCodes",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_NonPharmaceuticalItems_CategoryID",
            table: "NonPharmaceuticalItems",
            column: "CategoryID");

        migrationBuilder.CreateIndex(
            name: "IX_NonPharmaceuticalItems_PrescriptionID",
            table: "NonPharmaceuticalItems",
            column: "PrescriptionID");

        migrationBuilder.CreateIndex(
            name: "IX_NonPharmaceuticalQuotationItems_NonPharmaceuticalItemID",
            table: "NonPharmaceuticalQuotationItems",
            column: "NonPharmaceuticalItemID");

        migrationBuilder.CreateIndex(
            name: "IX_NonPharmaceuticalQuotationItems_QuotationID",
            table: "NonPharmaceuticalQuotationItems",
            column: "QuotationID");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_Expiration",
            table: "PersistedGrants",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_SubjectId_ClientId_Type",
            table: "PersistedGrants",
            columns: new[] { "SubjectId", "ClientId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_PersistedGrants_SubjectId_SessionId_Type",
            table: "PersistedGrants",
            columns: new[] { "SubjectId", "SessionId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_PharmaceuticalItems_CategoryID",
            table: "PharmaceuticalItems",
            column: "CategoryID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmaceuticalItems_DrugID",
            table: "PharmaceuticalItems",
            column: "DrugID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmaceuticalItems_FormulationID",
            table: "PharmaceuticalItems",
            column: "FormulationID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmaceuticalItems_PrescriptionID",
            table: "PharmaceuticalItems",
            column: "PrescriptionID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmaceuticalQuotationItems_PharmaceuticalItemID",
            table: "PharmaceuticalQuotationItems",
            column: "PharmaceuticalItemID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmaceuticalQuotationItems_QuotationID",
            table: "PharmaceuticalQuotationItems",
            column: "QuotationID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmacyPrescriptions_PharmacyID",
            table: "PharmacyPrescriptions",
            column: "PharmacyID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmacyPrescriptions_PrescriptionID",
            table: "PharmacyPrescriptions",
            column: "PrescriptionID");

        migrationBuilder.CreateIndex(
            name: "IX_PharmacyUsers_PharmacyID",
            table: "PharmacyUsers",
            column: "PharmacyID");

        migrationBuilder.CreateIndex(
            name: "IX_Prescriptions_PatientID",
            table: "Prescriptions",
            column: "PatientID");

        migrationBuilder.CreateIndex(
            name: "IX_Quotations_PharmacyID",
            table: "Quotations",
            column: "PharmacyID");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AspNetRoleClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens");

        migrationBuilder.DropTable(
            name: "DeviceCodes");

        migrationBuilder.DropTable(
            name: "NonPharmaceuticalQuotationItems");

        migrationBuilder.DropTable(
            name: "PersistedGrants");

        migrationBuilder.DropTable(
            name: "PharmaceuticalQuotationItems");

        migrationBuilder.DropTable(
            name: "PharmacyUsers");

        migrationBuilder.DropTable(
            name: "WorkOrders");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.DropTable(
            name: "AspNetUsers");

        migrationBuilder.DropTable(
            name: "NonPharmaceuticalItems");

        migrationBuilder.DropTable(
            name: "PharmaceuticalItems");

        migrationBuilder.DropTable(
            name: "Quotations");

        migrationBuilder.DropTable(
            name: "Categories");

        migrationBuilder.DropTable(
            name: "Drugs");

        migrationBuilder.DropTable(
            name: "Formulations");

        migrationBuilder.DropTable(
            name: "PharmacyPrescriptions");

        migrationBuilder.DropTable(
            name: "Pharmacies");

        migrationBuilder.DropTable(
            name: "Prescriptions");

        migrationBuilder.DropTable(
            name: "Patients");
    }
}