using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Factories;

public static class FighterFactory
{
    private static readonly Dictionary<FighterType, Func<string, IRace, IFighter>> _fighters = new()
    {
        { FighterType.Knight, ( name, race ) => new Knight( name, race ) },
        { FighterType.Murmillo, ( name, race ) => new Murmillo( name, race ) },
        { FighterType.Thraex, ( name, race ) => new Thraex( name, race ) },
        { FighterType.Secutor, ( name, race ) => new Secutor( name, race ) }
    };
    
    public static bool HasImplementation(FighterType fighter) => _fighters.ContainsKey(fighter);

    public static IFighter Create( FighterType fighter, string name, IRace race )
    {
        return _fighters[ fighter ]( name, race );
    }
}