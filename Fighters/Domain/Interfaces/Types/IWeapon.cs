using Domain.Types;

namespace Domain.Interfaces.Types;

public interface IWeapon
{
    public int Damage { get; }
    public WeaponType WeaponType { get; }
}