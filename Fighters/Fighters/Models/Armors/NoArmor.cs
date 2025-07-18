namespace Fighters.Models.Armors;

public class NoArmor : IArmor
{
    public ArmorType ArmorType => ArmorType.NoArmor;
    public int Armor => 0;
}