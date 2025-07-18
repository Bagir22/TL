using Fighters.Models.Weapons;

namespace Fighters.Factories;

public static class WeaponFactory
{
    private static readonly Dictionary<WeaponType, Func<IWeapon>> _weapons = new()
    {
        { WeaponType.Fists, () => new Fists() },
        { WeaponType.Sword, () => new Sword() },
        { WeaponType.Axe, () => new Axe() },
        { WeaponType.Mace, () => new Mace() },
    };
    
    public static bool HasImplementation(WeaponType weapon) => _weapons.ContainsKey(weapon);

    public static IWeapon Create( WeaponType weapon )
    {
        return _weapons[ weapon ]();
    }
}