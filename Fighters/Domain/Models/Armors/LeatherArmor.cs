using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Armors;

public class LeatherArmor : IArmor
{
    public ArmorType ArmorType => ArmorType.Leather;
    public int Armor => 10;
}