namespace EPharmacy.Application.Common.Models;

/// <summary>
/// Record type for immutable query params
/// </summary>
public record struct QueryParam(string QueryKey, string QueryValue);
