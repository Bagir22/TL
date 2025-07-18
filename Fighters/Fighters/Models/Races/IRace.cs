namespace Fighters.Models.Races;

public interface IRace
{
    public int Damage { get; }
    public int Health { get; }
    public int Vitality { get; }
    public int Initiative { get; }
    public RaceType RaceType { get; }
}