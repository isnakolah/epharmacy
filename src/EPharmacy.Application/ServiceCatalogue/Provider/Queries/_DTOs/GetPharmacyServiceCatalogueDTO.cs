namespace EPharmacy.Application.ServiceCatalogue.Provider.Queries.DTOs;

public record class GetPharmacyServiceCatalogueDTO
{
    public long ID { get; init; }

    public string Code { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public double MinFee { get; init; }

    public double MaxFee { get; init; }

    public double Charges { get; init; }

    public double Rake { get; init; }

    public bool Stocked { get; init; }
}
