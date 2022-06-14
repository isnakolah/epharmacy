namespace EPharmacy.Application.Common.Interfaces;

public interface ICurrentUserPharmacy
{
    Task<Guid> GetIDAsync();
}
