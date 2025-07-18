namespace Fighters.Models.Armors;

public class LeatherArmor : IArmor
{
    public ArmorType ArmorType => ArmorType.Leather;
    public int Armor => 10;
}