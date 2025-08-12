namespace Domain;

public interface IUnitOfWork
{
    Task CommitAsync();
}