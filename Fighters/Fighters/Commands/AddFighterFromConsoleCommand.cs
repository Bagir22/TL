using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utils;

namespace Fighters.Commands;

public class AddFighterFromConsoleCommand( List<IFighter> fighters ) : ICommand
{
    public void Execute()
    {
        string fighterName = GetFighterNameFromConsole();

        EnumPrinter.PrintAvailableInputs<RaceType>();
        IRace race = RaceFactory.Create( EnumInputHelper.GetValidEnumInput<RaceType>() );

        EnumPrinter.PrintAvailableInputs<FighterType>();
        IFighter fighter = FighterFactory.Create(
            EnumInputHelper.GetValidEnumInput<FighterType>(), fighterName, race );

        EnumPrinter.PrintAvailableInputs<WeaponType>();
        IWeapon weapon = WeaponFactory.Create( EnumInputHelper.GetValidEnumInput<WeaponType>() );

        fighter.SetWeapon( weapon );

        EnumPrinter.PrintAvailableInputs<ArmorType>();
        IArmor armor = ArmorFactory.Create( EnumInputHelper.GetValidEnumInput<ArmorType>() );

        fighter.SetArmor( armor );

        fighters.Add( fighter );

        Console.WriteLine( $"\nБоец {fighter.Name} добавлен" );
    }

    private string GetFighterNameFromConsole()
    {
        while ( true )
        {
            Console.Write( "Введите имя бойца: " );
            string name = Console.ReadLine() ?? "";

            if ( string.IsNullOrWhiteSpace( name ) )
            {
                Console.WriteLine( "Имя не может быть пустым" );
                continue;
            }

            if ( fighters.Any( f => f.Name.ToLower() == name.ToLower() ) )
            {
                Console.WriteLine( $"Боец с именем {name} уже существует. " +
                                   $"Введите другое имя" );
                continue;
            }

            return name;
        }
    }
}