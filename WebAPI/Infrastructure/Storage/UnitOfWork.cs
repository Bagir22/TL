using Domain;

namespace Infrastructure.Storage;

public class UnitOfWork : IUnitOfWork
{
    private readonly WebAPIDbContext _dbContext;

    public UnitOfWork( WebAPIDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync()
    {
        _ = await _dbContext.SaveChangesAsync();
    }
}