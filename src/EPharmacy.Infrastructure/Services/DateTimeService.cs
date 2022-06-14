namespace EPharmacy.Infrastructure.Services;

internal sealed class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow.AddHours(3);
}