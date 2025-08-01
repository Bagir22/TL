using System.Text.Json;
using Application.DTOs;
using Domain.Interfaces.Types;
using Domain.Types;
using Infrastructure.Factories;
using Domain.Interfaces.Services.RandomService;

namespace Infrastructure.Loaders.FightersLoader;

public static class FighterLoader
{
    public static List<IFighter> LoadFightersFromJson( string? filePath, IRandomService random )
    {
        string json = File.ReadAllText( filePath );
        List<FighterDto>? fightersData = JsonSerializer.Deserialize<List<FighterDto>>( json );

        List<IFighter> fighters = new();

        if ( fightersData != null )
        {
            foreach ( FighterDto dto in fightersData )
            {
                IRace race = RaceFactory.Create( Enum.Parse<RaceType>( dto.Race ) );
                IFighter fighter = FighterFactory.Create( Enum.Parse<FighterType>( dto.Type ), dto.Name, race, random );

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