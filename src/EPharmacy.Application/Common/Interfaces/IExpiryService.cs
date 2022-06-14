namespace EPharmacy.Application.Common.Interfaces;

/// <summary>
/// IExpiryService interface
/// </summary>
public interface IExpiryService
{
    /// <summary>
    /// Get the preset quotation expiry time
    /// </summary>
    DateTime GetQuotationExpiry { get; }
}