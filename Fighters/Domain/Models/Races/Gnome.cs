using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Races;

public class Gnome : IRace
{
    public RaceType RaceType => RaceType.Gnome;
    public int Damage => 3;
    public int Health => 35;
    public int Vitality => 2;
    public int Initiative => 3;
}