using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Murmillo( string name, IRace race ) : BaseFighter( name, race )
{
    protected override int GetClassDamage() => 2;
    protected override int GetClassArmor() => 3;
    protected override int GetClassHealth() => 20;
    protected override int GetClassInitiative() => 4;
}