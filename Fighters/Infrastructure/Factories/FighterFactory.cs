using Domain.Interfaces.Types;
using Domain.Models.Fighters;
using Domain.Types;
using Domain.Interfaces.Services.RandomService;

namespace Infrastructure.Factories;

public static class FighterFactory
{
    private static readonly Dictionary<FighterType, Func<string, IRace, IRandomService, IFighter>> _fighters = new()
    {
        { FighterType.Knight, ( name, race, random ) => new Knight( name, race, random ) },
        { FighterType.Murmillo, ( name, race, random ) => new Murmillo( name, race, random ) },
        { FighterType.Thraex, ( name, race, random ) => new Thraex( name, race, random ) },
        { FighterType.Secutor, ( name, race, random ) => new Secutor( name, race, random ) }
    };

    public static bool HasImplementation( FighterType fighter ) => _fighters.ContainsKey( fighter );

    public static IFighter Create( FighterType fighter, string name, IRace race, IRandomService random )
    {
        return _fighters[ fighter ]( name, race, random );
    }
}