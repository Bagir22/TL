using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Weapons;

public class Axe : IWeapon
{
    public WeaponType WeaponType => WeaponType.Axe;
    public int Damage => 5;
}