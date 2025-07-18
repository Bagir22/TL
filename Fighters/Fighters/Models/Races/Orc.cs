namespace Fighters.Models.Races;

public class Orc : IRace
{
    public RaceType RaceType => RaceType.Orc;
    public int Damage => 20;
    public int Health => 50;
    public int Vitality => 10;
    public int Initiative => 7;
}