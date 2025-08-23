using Domain.Entities;

namespace Domain.Repositories;

public interface IGuestRepository
{
    Task<Guest> CreateAsync( Guest guest );
    Task<Guest?> GetByPhoneNumberAsync( string phoneNumber );
}