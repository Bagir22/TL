using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Knight( string name, IRace race ) : BaseFighter( name, race )
{
    protected override int GetClassDamage() => 2;
    protected override int GetClassArmor() => 4;
    protected override int GetClassHealth() => 15;
    protected override int GetClassInitiative() => 3;
}