namespace Fighters.Models.Weapons;

public class Fists : IWeapon
{
    public WeaponType WeaponType => WeaponType.Fists;
    public int Damage => 1;
}