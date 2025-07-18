using Fighters.Models.Armors;

namespace Fighters.Factories;

public static class ArmorFactory
{
    private static readonly Dictionary<ArmorType, Func<IArmor>> _armors = new()
    {
        { ArmorType.NoArmor, () => new NoArmor() },
        { ArmorType.Leather, () => new LeatherArmor() },
        { ArmorType.Iron, () => new IronArmor() }
    };
    
    public static bool HasImplementation(ArmorType armor) => _armors.ContainsKey(armor);

    public static IArmor Create( ArmorType armor )
    {
        return _armors[ armor ]();
    }
}