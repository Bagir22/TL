using Application.Commands;
using Domain.Types;
using Infrastructure.Console.Commands;
using Infrastructure.Factories;

namespace Infrastructure.Console.Helpers;

public static class EnumImplementationChecker
{
    private static HashSet<Command> _implementedCommands = new();

    public static void SetImplementedCommands( IEnumerable<Command> commands )
    {
        _implementedCommands = new HashSet<Command>( commands );
    }

    private static bool HasImplementation( Command command ) =>
        _implementedCommands.Contains( command );

    public static bool HasImplementation<TEnum>( TEnum value ) where TEnum : Enum
    {
        return value switch
        {
            RaceType race => RaceFactory.HasImplementation( race ),
            WeaponType weapon => WeaponFactory.HasImplementation( weapon ),
            FighterType fighter => FighterFactory.HasImplementation( fighter ),
            ArmorType armor => ArmorFactory.HasImplementation( armor ),
            Command command => HasImplementation( command ),
            _ => false
        };
    }
}