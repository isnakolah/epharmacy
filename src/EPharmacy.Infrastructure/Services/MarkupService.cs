namespace EPharmacy.Infrastructure.Services;

internal sealed class MarkupService : IMarkupService
{
    private const double NON_PHARMACEUTICAL_PERCENTAGE_MARKUP = 0.2;
    private const double PHARMACEUTICAL_PERCENTAGE_MARKUP = 0.15;

    public double CalculateNonPharmaceuticalQuotationItemMarkup(in double total) => total * NON_PHARMACEUTICAL_PERCENTAGE_MARKUP;

    public double CalculatePharmaceuticalQuotationItemMarkup(in double total) => total * PHARMACEUTICAL_PERCENTAGE_MARKUP;
}