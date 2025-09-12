using Domain.Interfaces.Services.RandomService;
using Domain.Interfaces.Types;

namespace Domain.Models.Fighters;

public class Knight( string name, IRace race, IRandomService random ) : BaseFighter( name, race, random )
{
    protected override int GetClassDamage() => 2;
    protected override int GetClassArmor() => 4;
    protected override int GetClassHealth() => 15;
    protected override int GetClassInitiative() => 3;
}