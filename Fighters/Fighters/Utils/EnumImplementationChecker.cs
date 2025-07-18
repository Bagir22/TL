using Fighters.Commands;
using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Utils;

public static class EnumImplementationChecker
{
    private static HashSet<Command> _implementedCommands = new();
    
    private static bool HasImplementation(RaceType race) =>
        RaceFactory.HasImplementation(race);

    private static bool HasImplementation(WeaponType weapon) =>
        WeaponFactory.HasImplementation(weapon);

    private static bool HasImplementation(FighterType fighter) =>
        FighterFactory.HasImplementation(fighter);

    private static bool HasImplementation(ArmorType armor) =>
        ArmorFactory.HasImplementation(armor);
    
    public static void SetImplementedCommands(IEnumerable<Command> commands)
    {
        _implementedCommands = new HashSet<Command>(commands);
    }

    private static bool HasImplementation(Command command) =>
        _implementedCommands.Contains(command);

    public static bool HasImplementation<TEnum>(TEnum value) where TEnum : Enum
    {
        return value switch
        {
            RaceType race => HasImplementation(race),
            WeaponType weapon => HasImplementation(weapon),
            FighterType fighter => HasImplementation(fighter),
            ArmorType armor => HasImplementation(armor),
            Command command => HasImplementation(command),
            _ => false
        };
    }
}

