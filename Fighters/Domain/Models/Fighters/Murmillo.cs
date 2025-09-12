using Domain.Interfaces.Services.RandomService;
using Domain.Interfaces.Types;

namespace Domain.Models.Fighters;

public class Murmillo( string name, IRace race, IRandomService random ) : BaseFighter( name, race, random )
{
    protected override int GetClassDamage() => 2;
    protected override int GetClassArmor() => 3;
    protected override int GetClassHealth() => 20;
    protected override int GetClassInitiative() => 4;
}