using Fighters.Models.Fighters;
using Fighters.Utils;

namespace Fighters.Commands;

public class PrintFightersCommand( List<IFighter> fighters ) : ICommand
{
    public void Execute()
    {
        if ( fighters.Count == 0 )
        {
            Console.WriteLine( "Нет добавленных бойцов" );
            
            return;
        }

        Console.WriteLine( "Добавленные бойцы:" );
        foreach ( IFighter fighter in fighters )
        {
            Console.WriteLine( $"Боец: {fighter.Name}\n" +
                               $" -> Раса: {EnumPrinter.GetEnumValueDescription( fighter.GetRace().RaceType )}\n" +
                               $" -> Броня: {EnumPrinter.GetEnumValueDescription( fighter.GetArmor().ArmorType )}\n" +
                               $" -> Оружие: {EnumPrinter.GetEnumValueDescription( fighter.GetWeapon().WeaponType )}\n" +
                               $" -> Урон: {fighter.CalculateDamage()}\n" +
                               $" -> Защита: {fighter.CalculateDefense()}\n" +
                               $" -> Инициатива: {fighter.Initiative}\n" );
        }

        Console.WriteLine();
    }
}