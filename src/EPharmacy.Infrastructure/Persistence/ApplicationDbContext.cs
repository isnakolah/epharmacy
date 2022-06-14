using Duende.IdentityServer.EntityFramework.Options;
using EPharmacy.Domain.Common;
using EPharmacy.Domain.Entities;
using EPharmacy.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace EPharmacy.Infrastructure.Persistence;

public sealed class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventService _domainEventService;
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime) : base(options, operationalStoreOptions)
    {
        (_currentUserService, _domainEventService, _dateTime) = (currentUserService, domainEventService, dateTime);
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PharmaceuticalItem> PharmaceuticalItems => Set<PharmaceuticalItem>();
    public DbSet<NonPharmaceuticalItem> NonPharmaceuticalItems => Set<NonPharmaceuticalItem>();
    public DbSet<Pharmacy> Pharmacies => Set<Pharmacy>();
    public DbSet<PharmacyPrescription> PharmacyPrescriptions => Set<PharmacyPrescription>();
    public DbSet<Quotation> Quotations => Set<Quotation>();
    public DbSet<PharmaceuticalQuotationItem> PharmaceuticalQuotationItems => Set<PharmaceuticalQuotationItem>();
    public DbSet<NonPharmaceuticalQuotationItem> NonPharmaceuticalQuotationItems => Set<NonPharmaceuticalQuotationItem>();
    public DbSet<PharmacyUser> PharmacyUsers => Set<PharmacyUser>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Drug> Drugs => Set<Drug>();
    public DbSet<Formulation> Formulations => Set<Formulation>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default!)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
                    entry.Entity.CreatedOn = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents();

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents()
    {
        while (true)
        {
            var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

            if (domainEventEntity is null)
                break;

            domainEventEntity.IsPublished = true;

            await _domainEventService.Publish(domainEventEntity);
        }
    }
}
