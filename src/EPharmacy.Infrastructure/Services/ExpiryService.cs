namespace EPharmacy.Infrastructure.Services;

internal sealed class ExpiryService : IExpiryService
{
    private const int TIME_TILL_EXPIRY = 15;

    private readonly IDateTime _dateTime;

    public ExpiryService(IDateTime dateTime) 
    {
        _dateTime = dateTime;
    }

    public DateTime GetQuotationExpiry => _dateTime.Now.AddMinutes(TIME_TILL_EXPIRY);
}