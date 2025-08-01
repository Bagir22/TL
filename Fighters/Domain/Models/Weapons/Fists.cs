using Domain.Interfaces.Types;
using Domain.Types;

namespace Domain.Models.Weapons;

public class Fists : IWeapon
{
    public WeaponType WeaponType => WeaponType.Fists;
    public int Damage => 1;
}