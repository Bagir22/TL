using Domain.Entities;

namespace Domain.Services;

public interface ICurrencyRepository
{
    Task<Currency?> GetCurrencyByTypeAsync( string type );
}