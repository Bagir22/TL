using Domain.Interfaces.Types;
using Domain.Models.Races;
using Domain.Types;

namespace Infrastructure.Factories;

public static class RaceFactory
{
    private static readonly Dictionary<RaceType, Func<IRace>> _races = new()
    {
        { RaceType.Human, () => new Human() },
        { RaceType.Elf, () => new Elf() },
        { RaceType.Orc, () => new Orc() },
        { RaceType.Gnome, () => new Gnome() }
    };

    public static bool HasImplementation( RaceType race ) => _races.ContainsKey( race );

    public static IRace Create( RaceType race )
    {
        return _races[ race ]();
    }
}