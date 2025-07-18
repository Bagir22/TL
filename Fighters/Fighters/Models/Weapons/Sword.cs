namespace Fighters.Models.Weapons;

public class Sword : IWeapon
{
    public WeaponType WeaponType => WeaponType.Sword;
    public int Damage => 3;
}