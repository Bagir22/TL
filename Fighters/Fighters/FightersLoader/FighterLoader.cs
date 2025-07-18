using System.Text.Json;
using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.FightersLoader;

public static class FighterLoader
{
    public static List<IFighter> LoadFightersFromJson( string? filePath )
    {
        string json = File.ReadAllText( filePath );
        List<FighterDto>? fightersData = JsonSerializer.Deserialize<List<FighterDto>>( json );

        List<IFighter> fighters = new List<IFighter>();

        if ( fightersData != null )
        {
            foreach ( FighterDto dto in fightersData )
            {
                IRace race = RaceFactory.Create( Enum.Parse<RaceType>( dto.Race ) );
                IFighter fighter = FighterFactory.Create( Enum.Parse<FighterType>( dto.Type ), dto.Name, race );

                IArmor armor = ArmorFactory.Create( Enum.Parse<ArmorType>( dto.Armor ) );

                fighter.SetArmor( armor );

                IWeapon weapon = WeaponFactory.Create( Enum.Parse<WeaponType>( dto.Weapon ) );

                fighter.SetWeapon( weapon );

                fighters.Add( fighter );
            }
        }

        return fighters;
    }
}