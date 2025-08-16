using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly WebAPIDbContext _context;

    public CurrencyRepository( WebAPIDbContext context )
    {
        _context = context;
    }

    public async Task<Currency?> GetCurrencyByTypeAsync( string type )
    {
        return await _context.Currency.FirstOrDefaultAsync( c => c.Type == type );
    }
}