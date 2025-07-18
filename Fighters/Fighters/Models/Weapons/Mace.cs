namespace Fighters.Models.Weapons;

public class Mace : IWeapon
{
    public WeaponType WeaponType => WeaponType.Mace;
    public int Damage => 20;
}