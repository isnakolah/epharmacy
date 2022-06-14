namespace EPharmacy.Application.Common.Interfaces;

public interface IMarkupService
{
    double CalculatePharmaceuticalQuotationItemMarkup(in double total);

    double CalculateNonPharmaceuticalQuotationItemMarkup(in double total);
}