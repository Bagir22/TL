using Domain.Interfaces.Services.RandomService;
using Domain.Interfaces.Types;

namespace Domain.Models.Fighters;

public class Secutor( string name, IRace race, IRandomService random ) : BaseFighter( name, race, random )
{
    protected override int GetClassDamage() => 4;
    protected override int GetClassArmor() => 7;
    protected override int GetClassHealth() => 8;
    protected override int GetClassInitiative() => 5;
}