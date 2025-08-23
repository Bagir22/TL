using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class GuestRepository : IGuestRepository
{
    private readonly WebAPIDbContext _context;

    public GuestRepository( WebAPIDbContext context )
    {
        _context = context;
    }

    public async Task<Guest> CreateAsync( Guest guest )
    {
        _context.Guest.Add( guest );

        return guest;
    }

    public async Task<Guest?> GetByPhoneNumberAsync( string phoneNumber )
    {
        return await _context.Guest.FirstOrDefaultAsync( g => g.PhoneNumber == phoneNumber );
    }
}