using EPharmacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.Application.Common.Interfaces;

/// <summary>
/// IApplicationDbContext interface
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// The Patients table
    /// </summary>
    DbSet<Patient> Patients { get; }

    /// <summary>
    /// The Pharmaceutical Items Table
    /// </summary>
    DbSet<PharmaceuticalItem> PharmaceuticalItems { get; }

    /// <summary>
    /// The NonPharmaceutical Items Table
    /// </summary>
    DbSet<NonPharmaceuticalItem> NonPharmaceuticalItems { get; }

    /// <summary>
    /// The Prescription Table
    /// </summary>
    DbSet<Prescription> Prescriptions { get; }

    /// <summary>
    /// The Pharmacy Table
    /// </summary>
    DbSet<Pharmacy> Pharmacies { get; }

    /// <summary>
    /// The PharmacyPrescriptions table
    /// </summary>
    DbSet<PharmacyPrescription> PharmacyPrescriptions { get; }

    /// <summary>
    /// The Pharmacy Quotatoin Table
    /// </summary>
    DbSet<Quotation> Quotations { get; }

    /// <summary>
    /// The Pharmacy Quotation Item Table
    /// </summary>
    DbSet<PharmaceuticalQuotationItem> PharmaceuticalQuotationItems { get; }

    /// <summary>
    /// The Pharmacy Quotation Item Table
    /// </summary>
    DbSet<NonPharmaceuticalQuotationItem> NonPharmaceuticalQuotationItems { get; }

    /// <summary>
    /// WorkOrders table for the pharmacy users
    /// </summary>
    DbSet<WorkOrder> WorkOrders { get; }

    /// <summary>
    /// Pharmacy Users table for the pharmacy users
    /// </summary>
    DbSet<PharmacyUser> PharmacyUsers { get; }

    /// <summary>
    /// Categories table for the categories
    /// </summary>
    DbSet<Category> Categories { get; }

    /// <summary>
    /// Drugs table for the categories
    /// </summary>
    DbSet<Drug> Drugs { get; }

    /// <summary>
    /// Drugs table for the categories
    /// </summary>
    DbSet<Formulation> Formulations { get; }

    /// <summary>
    /// Persisting the changes
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An integer</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}