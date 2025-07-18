namespace Fighters.Models.Races;

public class Human : IRace
{
    public RaceType RaceType => RaceType.Human;
    public int Damage => 1;
    public int Health => 20;
    public int Vitality => 0;
    public int Initiative => 1;
}