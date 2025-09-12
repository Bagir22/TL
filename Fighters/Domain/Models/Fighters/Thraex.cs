using Domain.Interfaces.Services.RandomService;
using Domain.Interfaces.Types;

namespace Domain.Models.Fighters;

public class Thraex( string name, IRace race, IRandomService random ) : BaseFighter( name, race, random )
{
    protected override int GetClassDamage() => 3;
    protected override int GetClassArmor() => 7;
    protected override int GetClassHealth() => 18;
    protected override int GetClassInitiative() => 6;
}