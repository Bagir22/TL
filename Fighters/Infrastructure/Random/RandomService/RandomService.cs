namespace Infrastructure.Random.RandomService;

public class RandomService : IRandomService, Domain.Interfaces.Services.RandomService.IRandomService
{
    private readonly System.Random _random = new();

    public double NextDouble()
    {
        return _random.NextDouble();
    }
}