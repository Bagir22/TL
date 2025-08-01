using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Weapons;

public class Mace : IWeapon
{
    public WeaponType WeaponType => WeaponType.Mace;
    public int Damage => 20;
}