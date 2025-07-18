using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Secutor( string name, IRace race ) : BaseFighter( name, race )
{
    protected override int GetClassDamage() => 4;
    protected override int GetClassArmor() => 7;
    protected override int GetClassHealth() => 8;
    protected override int GetClassInitiative() => 5;
}