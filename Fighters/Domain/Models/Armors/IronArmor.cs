using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Armors;

public class IronArmor : IArmor
{
    public ArmorType ArmorType => ArmorType.Iron;
    public int Armor => 30;
}