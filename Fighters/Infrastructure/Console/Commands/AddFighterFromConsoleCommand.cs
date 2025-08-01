using Application.Interfaces;
using Domain.Interfaces.Types;
using Domain.Types;
using Infrastructure.Console.Helpers;
using Infrastructure.Factories;
using Infrastructure.Random.RandomService;

namespace Infrastructure.Console.Commands;

public class AddFighterFromConsoleCommand( List<IFighter> fighters, IConsoleService console ) : ICommand
{
    public void Execute()
    {
        string fighterName = GetFighterNameFromConsole();

        EnumPrinter.PrintAvailableInputs<RaceType>();
        IRace race = RaceFactory.Create( EnumInputHelper.GetValidEnumInput<RaceType>() );

        EnumPrinter.PrintAvailableInputs<FighterType>();
        IFighter fighter = FighterFactory.Create(
            EnumInputHelper.GetValidEnumInput<FighterType>(), fighterName, race, new RandomService() );

        EnumPrinter.PrintAvailableInputs<WeaponType>();
        IWeapon weapon = WeaponFactory.Create( EnumInputHelper.GetValidEnumInput<WeaponType>() );

        fighter.SetWeapon( weapon );

        EnumPrinter.PrintAvailableInputs<ArmorType>();
        IArmor armor = ArmorFactory.Create( EnumInputHelper.GetValidEnumInput<ArmorType>() );

        fighter.SetArmor( armor );

        fighters.Add( fighter );

        console.WriteLine( $"\nБоец {fighter.Name} добавлен" );
    }

    private string GetFighterNameFromConsole()
    {
        while ( true )
        {
            console.Write( "Введите имя бойца: " );
            string name = console.ReadLine() ?? "";

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                console.WriteLine( "Имя не может быть пустым" );
                continue;
            }

            if ( fighters.Any( f => f.Name.ToLower() == name.ToLower() ) )
            {
                console.WriteLine( $"Боец с именем {name} уже существует. " +
                                   $"Введите другое имя" );
                continue;
            }

            return name;
        }
    }
}