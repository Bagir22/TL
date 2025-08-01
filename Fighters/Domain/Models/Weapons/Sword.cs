using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Weapons;

public class Sword : IWeapon
{
    public WeaponType WeaponType => WeaponType.Sword;
    public int Damage => 3;
}