using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Races;

public class Elf : IRace
{
    public RaceType RaceType => RaceType.Elf;
    public int Damage => 5;
    public int Health => 30;
    public int Vitality => 3;
    public int Initiative => 5;
}