namespace Fighters.Models.Armors;

public class IronArmor : IArmor
{
    public ArmorType ArmorType => ArmorType.Iron;
    public int Armor => 30;
}